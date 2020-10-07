// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	VariableDefinition.cs
====================================================================================*/

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
