// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	PropertyReference.cs
====================================================================================*/

using System;

using LSharp.IL.Collections.Generic;

namespace LSharp.IL
{

	public abstract class PropertyReference : MemberReference {

		TypeReference property_type;

		public TypeReference PropertyType {
			get { return property_type; }
			set { property_type = value; }
		}

		public abstract Collection<ParameterDefinition> Parameters {
			get;
		}

		internal PropertyReference (string name, TypeReference propertyType)
			: base (name)
		{
			Mixin.CheckType (propertyType, Mixin.Argument.propertyType);

			property_type = propertyType;
		}

		protected override IMemberDefinition ResolveDefinition ()
		{
			return this.Resolve ();
		}

		public new abstract PropertyDefinition Resolve ();
	}
}
