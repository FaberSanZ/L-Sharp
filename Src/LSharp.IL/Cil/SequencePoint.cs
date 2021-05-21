// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;

namespace LSharp.IL.Cil {

	public sealed class SequencePoint {

		internal InstructionOffset offset;
		Document document;

		int start_line;
		int start_column;
		int end_line;
		int end_column;

		public int Offset {
			get { return offset.Offset; }
		}

		public int StartLine {
			get { return start_line; }
			set { start_line = value; }
		}

		public int StartColumn {
			get { return start_column; }
			set { start_column = value; }
		}

		public int EndLine {
			get { return end_line; }
			set { end_line = value; }
		}

		public int EndColumn {
			get { return end_column; }
			set { end_column = value; }
		}

		public bool IsHidden {
			get { return start_line == 0xfeefee && start_line == end_line; }
		}

		public Document Document {
			get { return document; }
			set { document = value; }
		}

		internal SequencePoint (int offset, Document document)
		{
			if (document == null)
				throw new ArgumentNullException ("document");

			this.offset = new InstructionOffset (offset);
			this.document = document;
		}

		public SequencePoint (Instruction instruction, Document document)
		{
			if (document == null)
				throw new ArgumentNullException ("document");

			this.offset = new InstructionOffset (instruction);
			this.document = document;
		}
	}
}
