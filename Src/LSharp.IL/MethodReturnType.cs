// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)

using System.Threading;

using LSharp.IL.Collections.Generic;

namespace LSharp.IL
{

	public sealed class MethodReturnType : IConstantProvider, ICustomAttributeProvider, IMarshalInfoProvider {

		internal IMethodSignature method;
		internal ParameterDefinition parameter;
		TypeReference return_type;

		public IMethodSignature Method {
			get { return method; }
		}

		public TypeReference ReturnType {
			get { return return_type; }
			set { return_type = value; }
		}

		internal ParameterDefinition Parameter {
			get {
				if (parameter == null)
					Interlocked.CompareExchange (ref parameter, new ParameterDefinition (return_type, method), null);

				return parameter;
			}
		}

		public MetadataToken MetadataToken {
			get { return Parameter.MetadataToken; }
			set { Parameter.MetadataToken = value; }
		}

		public ParameterAttributes Attributes {
			get { return Parameter.Attributes; }
			set { Parameter.Attributes = value; }
		}

		public string Name {
			get { return Parameter.Name; }
			set { Parameter.Name = value; }
		}

		public bool HasCustomAttributes {
			get { return parameter != null && parameter.HasCustomAttributes; }
		}

		public Collection<CustomAttribute> CustomAttributes {
			get { return Parameter.CustomAttributes; }
		}

		public bool HasDefault {
			get { return parameter != null && parameter.HasDefault; }
			set { Parameter.HasDefault = value; }
		}

		public bool HasConstant {
			get { return parameter != null && parameter.HasConstant; }
			set { Parameter.HasConstant = value; }
		}

		public object Constant {
			get { return Parameter.Constant; }
			set { Parameter.Constant = value; }
		}

		public bool HasFieldMarshal {
			get { return parameter != null && parameter.HasFieldMarshal; }
			set { Parameter.HasFieldMarshal = value; }
		}

		public bool HasMarshalInfo {
			get { return parameter != null && parameter.HasMarshalInfo; }
		}

		public MarshalInfo MarshalInfo {
			get { return Parameter.MarshalInfo; }
			set { Parameter.MarshalInfo = value; }
		}

		public MethodReturnType (IMethodSignature method)
		{
			this.method = method;
		}
	}
}
