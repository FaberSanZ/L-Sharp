// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)

namespace LSharp.IL.Cil {

	public sealed class VariableDefinition : VariableReference {

		public bool IsPinned {
			get { return variable_type.IsPinned; }
		}

		public VariableDefinition (TypeReference variableType)
			: base (variableType)
		{
		}

		public override VariableDefinition Resolve ()
		{
			return this;
		}
	}
}
