// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using LSharp.IL.Collections.Generic;
using System;
using System.Text;

namespace LSharp.IL
{

    public sealed class CallSite : IMethodSignature
    {
        private readonly MethodReference signature;

        public bool HasThis
        {
            get { return signature.HasThis; }
            set { signature.HasThis = value; }
        }

        public bool ExplicitThis
        {
            get { return signature.ExplicitThis; }
            set { signature.ExplicitThis = value; }
        }

        public MethodCallingConvention CallingConvention
        {
            get { return signature.CallingConvention; }
            set { signature.CallingConvention = value; }
        }

        public bool HasParameters
        {
            get { return signature.HasParameters; }
        }

        public Collection<ParameterDefinition> Parameters
        {
            get { return signature.Parameters; }
        }

        public TypeReference ReturnType
        {
            get { return signature.MethodReturnType.ReturnType; }
            set { signature.MethodReturnType.ReturnType = value; }
        }

        public MethodReturnType MethodReturnType
        {
            get { return signature.MethodReturnType; }
        }

        public string Name
        {
            get { return string.Empty; }
            set { throw new InvalidOperationException(); }
        }

        public string Namespace
        {
            get { return string.Empty; }
            set { throw new InvalidOperationException(); }
        }

        public ModuleDefinition Module
        {
            get { return ReturnType.Module; }
        }

        public IMetadataScope Scope
        {
            get { return signature.ReturnType.Scope; }
        }

        public MetadataToken MetadataToken
        {
            get { return signature.token; }
            set { signature.token = value; }
        }

        public string FullName
        {
            get
            {
                StringBuilder signature = new StringBuilder();
                signature.Append(ReturnType.FullName);
                this.MethodSignatureFullName(signature);
                return signature.ToString();
            }
        }

        internal CallSite()
        {
            this.signature = new MethodReference
            {
                token = new MetadataToken(TokenType.Signature, 0)
            };
        }

        public CallSite(TypeReference returnType)
            : this()
        {
            if (returnType == null)
            {
                throw new ArgumentNullException("returnType");
            }

            this.signature.ReturnType = returnType;
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
