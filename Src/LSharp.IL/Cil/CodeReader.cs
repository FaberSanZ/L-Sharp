// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)

/*===================================================================================
	CodeReader.cs
====================================================================================*/


using LSharp.IL.Collections.Generic;
using LSharp.IL.PE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSharp.IL.Cil
{
    internal sealed class CodeReader : BinaryStreamReader
    {

        internal readonly MetadataReader _reader;
        internal int start;
        internal MethodDefinition _method;
        internal MethodBody body;


        public CodeReader(MetadataReader reader) : base(reader.image.Stream.value)
        {
            _reader = reader;
        }

        public int Offset => Position - start;


        public int MoveTo(MethodDefinition method)
        {
            _method = method;

            _reader.context = method;

            int position = Position;

            Position = (int)_reader.image.ResolveVirtualAddress((uint)method.RVA);

            return position;
        }

        public void MoveBackTo(int position)
        {
            _reader.context = null;

            Position = position;
        }

        public MethodBody ReadMethodBody(MethodDefinition method)
        {
            int position = MoveTo(method);

            body = new(method);

            ReadMethodBody();

            MoveBackTo(position);

            return body;
        }

        public int ReadCodeSize(MethodDefinition method)
        {
            int position = MoveTo(method);

            int code_size = ReadCodeSize();

            MoveBackTo(position);

            return code_size;
        }

        private int ReadCodeSize()
        {
            byte flags = ReadByte();

            switch (flags & 0x3)
            {
                case 0x2: // tiny
                    return flags >> 2;

                case 0x3: // fat
                    Advance(-1 + 2 + 2); // go back, 2 bytes flags, 2 bytes stack size
                    return (int)ReadUInt32();

                default:
                    throw new InvalidOperationException();
            }
        }

        private void ReadMethodBody()
        {
            byte flags = ReadByte();

            switch (flags & 0x3)
            {
                case 0x2: // tiny
                    body.code_size = flags >> 2;
                    body.MaxStackSize = 8;
                    ReadCode();
                    break;

                case 0x3: // fat
                    Advance(-1);
                    ReadFatMethod();
                    break;

                default:
                    throw new InvalidOperationException();
            }

            ISymbolReader symbol_reader = _reader.module.symbol_reader;

            if (symbol_reader is not null && _method.debug_info is null)
                _method.debug_info = symbol_reader.Read(_method);

            if (_method.debug_info is not null)
                ReadDebugInfo();
        }

        private void ReadFatMethod()
        {
            ushort flags = ReadUInt16();

            body.max_stack_size = ReadUInt16();
            body.code_size = (int)ReadUInt32();
            body.local_var_token = new(ReadUInt32());
            body.init_locals = (flags & 0x10) != 0;

            if (body.local_var_token.RID is not 0)
                body.variables = ReadVariables(body.local_var_token);

            ReadCode();

            if ((flags & 0x8) is not 0)
                ReadSection();
        }

        public VariableDefinitionCollection ReadVariables(MetadataToken local_var_token)
        {
            int position = _reader.position;

            VariableDefinitionCollection variables = _reader.ReadVariables(local_var_token, _method);

            _reader.position = position;

            return variables;
        }

        private void ReadCode()
        {
            start = Position;
            int code_size = body.code_size;

            if (code_size < 0 || Length <= (uint)(code_size + Position))
                code_size = 0;

            int end = start + code_size;

            // TODO: instructions List -> InstructionCollection
            List<Instruction> instructions = (body.instructions = new InstructionCollection(_method, (code_size + 1) / 2)).ToArray().ToList();

            while (Position < end)
            {
                int offset = Position - start;
                OpCode opcode = ReadOpCode();
                Instruction current = new(offset, opcode);

                if (opcode.OperandType is not OperandType.InlineNone)
                    current.operand = ReadOperand(current);

                instructions.Add(current);
            }

            ResolveBranches(instructions);
        }

        private OpCode ReadOpCode()
        {
            byte il_opcode = ReadByte();

            return il_opcode is not 0xfe ? OpCodes.OneByteOpCode[il_opcode] : OpCodes.TwoBytesOpCode[ReadByte()];
        }

        private object ReadOperand(Instruction instruction)
        {
            switch (instruction.opcode.OperandType)
            {
                case OperandType.InlineSwitch:
                    int length = ReadInt32();
                    int base_offset = Offset + (4 * length);
                    int[] branches = new int[length];

                    for (int i = 0; i < length; i++)
                        branches[i] = base_offset + ReadInt32();

                    return branches;

                case OperandType.ShortInlineBrTarget:
                    return ReadSByte() + Offset;

                case OperandType.InlineBrTarget:
                    return ReadInt32() + Offset;

                case OperandType.ShortInlineI:
                    if (instruction.opcode == OpCodes.Ldc_I4_S)
                        return ReadSByte();

                    return ReadByte();

                case OperandType.InlineI:
                    return ReadInt32();

                case OperandType.ShortInlineR:
                    return ReadSingle();

                case OperandType.InlineR:
                    return ReadDouble();

                case OperandType.InlineI8:
                    return ReadInt64();

                case OperandType.ShortInlineVar:
                    return GetVariable(ReadByte());

                case OperandType.InlineVar:
                    return GetVariable(ReadUInt16());

                case OperandType.ShortInlineArg:
                    return GetParameter(ReadByte());

                case OperandType.InlineArg:
                    return GetParameter(ReadUInt16());

                case OperandType.InlineSig:
                    return GetCallSite(ReadToken());

                case OperandType.InlineString:
                    return GetString(ReadToken());

                case OperandType.InlineTok:
                case OperandType.InlineType:
                case OperandType.InlineMethod:
                case OperandType.InlineField:
                    return _reader.LookupToken(ReadToken());

                default:
                    throw new NotSupportedException();
            }
        }

        public string GetString(MetadataToken token) => _reader.image.UserStringHeap.Read(token.RID);


        public ParameterDefinition GetParameter(int index) => body.GetParameter(index);


        public VariableDefinition GetVariable(int index) => body.GetVariable(index);


        public CallSite GetCallSite(MetadataToken token) => _reader.ReadCallSite(token);
        

        private void ResolveBranches(List<Instruction> instructions)
        {
            int size = instructions.Count;

            for (int i = 0; i < size; i++)
            {
                Instruction instruction = instructions[i];
                switch (instruction.opcode.OperandType)
                {
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.InlineBrTarget:
                        instruction.operand = GetInstruction((int)instruction.operand);
                        break;

                    case OperandType.InlineSwitch:
                        int[] offsets = (int[])instruction.operand;
                        Instruction[] branches = new Instruction[offsets.Length];

                        for (int j = 0; j < offsets.Length; j++)
                            branches[j] = GetInstruction(offsets[j]);

                        instruction.operand = branches;
                        break;
                }
            }
        }

        private Instruction GetInstruction(int offset) => GetInstruction(body.Instructions.ToList(), offset);
        

        private static Instruction GetInstruction(List<Instruction> instructions, int offset)
        {
            int size = instructions.Count;

            if (offset < 0 || offset > instructions[size - 1].offset)
                return null;

            int min = 0;
            int max = size - 1;

            while (min <= max)
            {
                int mid = min + ((max - min) / 2);

                Instruction instruction = instructions[mid];

                int instruction_offset = instruction.offset;

                if (offset == instruction_offset)
                    return instruction;

                if (offset < instruction_offset)
                    max = mid - 1;

                else
                    min = mid + 1;
            }

            return null;
        }

        private void ReadSection()
        {
            Align(4);

            const byte fat_format = 0x40;
            const byte more_sects = 0x80;

            byte flags = ReadByte();


            if ((flags & fat_format) is 0)
                ReadSmallSection();
            else
                ReadFatSection();


            if ((flags & more_sects) is not 0)
                ReadSection();
        }

        private void ReadSmallSection()
        {
            int count = ReadByte() / 12;

            Advance(2);

            ReadExceptionHandlers(count,
                () => ReadUInt16(),
                () => ReadByte());
        }

        private void ReadFatSection()
        {
            Advance(-1);
            int count = (ReadInt32() >> 8) / 24;

            ReadExceptionHandlers(count, ReadInt32, ReadInt32);
        }

        // inline ?
        private void ReadExceptionHandlers(int count, Func<int> read_entry, Func<int> read_length)
        {
            for (int i = 0; i < count; i++)
            {
                ExceptionHandler handler = new((ExceptionHandlerType)(read_entry() & 0x7))
                {
                    TryStart = GetInstruction(read_entry()),

                };

                handler.TryEnd = GetInstruction(handler.TryStart.Offset + read_length());
                handler.HandlerStart = GetInstruction(read_entry());
                handler.HandlerEnd = GetInstruction(handler.HandlerStart.Offset + read_length());

                ReadExceptionHandlerSpecific(handler);

                body.ExceptionHandlers.Add(handler);
            }
        }

        private void ReadExceptionHandlerSpecific(ExceptionHandler handler)
        {
            switch (handler.HandlerType)
            {
                case ExceptionHandlerType.Catch:
                    handler.CatchType = _reader.LookupToken(ReadToken()) as TypeReference;
                    break;

                case ExceptionHandlerType.Filter:
                    handler.FilterStart = GetInstruction(ReadInt32());
                    break;

                default:
                    Advance(4);
                    break;
            }
        }

        public MetadataToken ReadToken() => new MetadataToken(ReadUInt32());
        

        private void ReadDebugInfo()
        {
            if (_method.debug_info.sequence_points is not null)
                ReadSequencePoints();

            if (_method.debug_info.scope is not null)
                ReadScope(_method.debug_info.scope);

            if (_method.custom_infos is not null)
                ReadCustomDebugInformations(_method);
        }

        private void ReadCustomDebugInformations(MethodDefinition method)
        {
            // TODO: Collection -> List
            Collection<CustomDebugInformation> custom_infos = method.custom_infos;

            for (int i = 0; i < custom_infos.Count; i++)
            {
                StateMachineScopeDebugInformation state_machine_scope = custom_infos[i] as StateMachineScopeDebugInformation;

                if (state_machine_scope is not null)
                    ReadStateMachineScope(state_machine_scope);

                AsyncMethodBodyDebugInformation async_method = custom_infos[i] as AsyncMethodBodyDebugInformation;

                if (async_method is not null)
                    ReadAsyncMethodBody(async_method);
            }
        }

        private void ReadAsyncMethodBody(AsyncMethodBodyDebugInformation async_method)
        {
            if (async_method.catch_handler.Offset > -1)
                async_method.catch_handler = new(GetInstruction(async_method.catch_handler.Offset));

            // TODO: Collection -> List
            if (!async_method.yields.IsNullOrEmpty())
                for (int i = 0; i < async_method.yields.Count; i++)
                    async_method.yields[i] = new(GetInstruction(async_method.yields[i].Offset));

            // TODO: Collection -> List
            if (!async_method.resumes.IsNullOrEmpty())
                for (int i = 0; i < async_method.resumes.Count; i++)
                    async_method.resumes[i] = new(GetInstruction(async_method.resumes[i].Offset));
        }

        private void ReadStateMachineScope(StateMachineScopeDebugInformation state_machine_scope)
        {
            // TODO: Collection -> List
            if (state_machine_scope.scopes.IsNullOrEmpty())
                return;

            foreach (StateMachineScope scope in state_machine_scope.scopes)
            {
                scope.start = new(GetInstruction(scope.start.Offset));

                Instruction end_instruction = GetInstruction(scope.end.Offset);

                scope.end = end_instruction is null ? new() : new(end_instruction);
            }
        }

        private void ReadSequencePoints()
        {
            MethodDebugInformation symbol = _method.debug_info;

            for (int i = 0; i < symbol.sequence_points.Count; i++)
            {
                SequencePoint sequence_point = symbol.sequence_points[i];
                Instruction instruction = GetInstruction(sequence_point.Offset);

                if (instruction is not null)
                    sequence_point.offset = new(instruction);
            }
        }

        private void ReadScopes(Collection<ScopeDebugInformation> scopes)
        {
            for (int i = 0; i < scopes.Count; i++)
                ReadScope(scopes[i]);
        }

        private void ReadScope(ScopeDebugInformation scope)
        {
            Instruction start_instruction = GetInstruction(scope.Start.Offset);

            if (start_instruction is not null)
                scope.Start = new(start_instruction);

            Instruction end_instruction = GetInstruction(scope.End.Offset);

            scope.End = end_instruction is not null ? new(end_instruction) : new();

            // TODO: Collection -> List
            if (!scope.variables.IsNullOrEmpty())
            {
                for (int i = 0; i < scope.variables.Count; i++)
                {
                    VariableDebugInformation variable_info = scope.variables[i];
                    VariableDefinition variable = GetVariable(variable_info.Index);

                    if (variable is not null)
                        variable_info.index = new(variable);
                }
            }

            if (!scope.scopes.IsNullOrEmpty())
            {
                ReadScopes(scope.scopes);
            }
        }

        public ByteBuffer PatchRawMethodBody(MethodDefinition method, CodeWriter writer, out int code_size, out MetadataToken local_var_token)
        {
            int position = MoveTo(method);

            ByteBuffer buffer = new();

            byte flags = ReadByte();

            switch (flags & 0x3)
            {
                case 0x2: // tiny
                    buffer.WriteByte(flags);
                    local_var_token = MetadataToken.Zero;
                    code_size = flags >> 2;
                    PatchRawCode(buffer, code_size, writer);
                    break;

                case 0x3: // fat
                    Advance(-1);
                    PatchRawFatMethod(buffer, writer, out code_size, out local_var_token);
                    break;

                default:
                    throw new NotSupportedException();
            }

            MoveBackTo(position);

            return buffer;
        }

        private void PatchRawFatMethod(ByteBuffer buffer, CodeWriter writer, out int code_size, out MetadataToken local_var_token)
        {
            ushort flags = ReadUInt16();
            buffer.WriteUInt16(flags);
            buffer.WriteUInt16(ReadUInt16());
            code_size = ReadInt32();
            buffer.WriteInt32(code_size);
            local_var_token = ReadToken();

            if (local_var_token.RID > 0)
            {
                VariableDefinitionCollection variables = ReadVariables(local_var_token);
                buffer.WriteUInt32(variables is not null ? writer.GetStandAloneSignature(variables).ToUInt32() : 0);
            }
            else
                buffer.WriteUInt32(0);

            PatchRawCode(buffer, code_size, writer);

            if ((flags & 0x8) is not 0)
                PatchRawSection(buffer, writer.metadata);

        }

        private void PatchRawCode(ByteBuffer buffer, int code_size, CodeWriter writer)
        {
            MetadataBuilder metadata = writer.metadata;
            buffer.WriteBytes(ReadBytes(code_size));
            int end = buffer.position;
            buffer.position -= code_size;

            while (buffer.position < end)
            {
                OpCode opcode;
                byte il_opcode = buffer.ReadByte();

                if (il_opcode is not 0xfe)
                    opcode = OpCodes.OneByteOpCode[il_opcode];
                else
                {
                    byte il_opcode2 = buffer.ReadByte();
                    opcode = OpCodes.TwoBytesOpCode[il_opcode2];
                }

                switch (opcode.OperandType)
                {
                    case OperandType.ShortInlineI:
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.ShortInlineVar:
                    case OperandType.ShortInlineArg:
                        buffer.position += 1;
                        break;

                    case OperandType.InlineVar:
                    case OperandType.InlineArg:
                        buffer.position += 2;
                        break;

                    case OperandType.InlineBrTarget:
                    case OperandType.ShortInlineR:
                    case OperandType.InlineI:
                        buffer.position += 4;
                        break;

                    case OperandType.InlineI8:
                    case OperandType.InlineR:
                        buffer.position += 8;
                        break;

                    case OperandType.InlineSwitch:
                        int length = buffer.ReadInt32();
                        buffer.position += length * 4;
                        break;

                    case OperandType.InlineString:
                        string @string = GetString(new(buffer.ReadUInt32()));
                        buffer.position -= 4;
                        buffer.WriteUInt32(new MetadataToken(TokenType.String, metadata.user_string_heap.GetStringIndex(@string)).ToUInt32());
                        break;

                    case OperandType.InlineSig:
                        CallSite call_site = GetCallSite(new MetadataToken(buffer.ReadUInt32()));
                        buffer.position -= 4;
                        buffer.WriteUInt32(writer.GetStandAloneSignature(call_site).ToUInt32());
                        break;

                    case OperandType.InlineTok:
                    case OperandType.InlineType:
                    case OperandType.InlineMethod:
                    case OperandType.InlineField:
                        IMetadataTokenProvider provider = _reader.LookupToken(new(buffer.ReadUInt32()));
                        buffer.position -= 4;
                        buffer.WriteUInt32(metadata.LookupToken(provider).ToUInt32());
                        break;

                }
            }
        }

        private void PatchRawSection(ByteBuffer buffer, MetadataBuilder metadata)
        {
            int position = Position;

            Align(4);

            buffer.WriteBytes(Position - position);

            const byte fat_format = 0x40;
            const byte more_sects = 0x80;

            byte flags = ReadByte();

            if ((flags & fat_format) == 0)
            {
                buffer.WriteByte(flags);
                PatchRawSmallSection(buffer, metadata);
            }
            else
                PatchRawFatSection(buffer, metadata);

            if ((flags & more_sects) is not 0)
                PatchRawSection(buffer, metadata);

        }

        private void PatchRawSmallSection(ByteBuffer buffer, MetadataBuilder metadata)
        {
            byte length = ReadByte();

            buffer.WriteByte(length);

            Advance(2);

            buffer.WriteUInt16(0);

            int count = length / 12;

            PatchRawExceptionHandlers(buffer, metadata, count, false);
        }

        private void PatchRawFatSection(ByteBuffer buffer, MetadataBuilder metadata)
        {
            Advance(-1);

            int length = ReadInt32();

            buffer.WriteInt32(length);

            int count = (length >> 8) / 24;

            PatchRawExceptionHandlers(buffer, metadata, count, true);
        }

        private void PatchRawExceptionHandlers(ByteBuffer buffer, MetadataBuilder metadata, int count, bool fat_entry)
        {
            const int fat_entry_size = 16;
            const int small_entry_size = 6;

            for (int i = 0; i < count; i++)
            {
                ExceptionHandlerType handler_type;

                if (fat_entry)
                {
                    uint type = ReadUInt32();
                    handler_type = (ExceptionHandlerType)(type & 0x7);
                    buffer.WriteUInt32(type);
                }
                else
                {
                    ushort type = ReadUInt16();
                    handler_type = (ExceptionHandlerType)(type & 0x7);
                    buffer.WriteUInt16(type);
                }

                buffer.WriteBytes(ReadBytes(fat_entry ? fat_entry_size : small_entry_size));

                switch (handler_type)
                {
                    case ExceptionHandlerType.Catch:
                        IMetadataTokenProvider exception = _reader.LookupToken(ReadToken());
                        buffer.WriteUInt32(metadata.LookupToken(exception).ToUInt32());
                        break;

                    default:
                        buffer.WriteUInt32(ReadUInt32());
                        break;
                }
            }
        }
    }
}
