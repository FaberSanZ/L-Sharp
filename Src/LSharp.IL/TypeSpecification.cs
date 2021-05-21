// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;

using LSharp.IL.Metadata;

namespace LSharp.IL
{

	public abstract class TypeSpecification : TypeReference {

		readonly TypeReference element_type;

		public TypeReference ElementType {
			get { return element_type; }
		}

		public override string Name {
			get { return element_type.Name; }
			set { throw new InvalidOperationException (); }
		}

		public override string Namespace {
			get { return element_type.Namespace; }
			set { throw new InvalidOperationException (); }
		}

		public override IMetadataScope Scope {
			get { return element_type.Scope; }
			set { throw new InvalidOperationException (); }
		}

		public override ModuleDefinition Module {
			get { return element_type.Module; }
		}

		public override string FullName {
			get { return element_type.FullName; }
		}

		public override bool ContainsGenericParameter {
			get { return element_type.ContainsGenericParameter; }
		}

		public override MetadataType MetadataType {
			get { return (MetadataType) etype; }
		}

		internal TypeSpecification (TypeReference type)
			: base (null, null)
		{
			this.element_type = type;
			this.token = new MetadataToken (TokenType.TypeSpec);
		}

		public override TypeReference GetElementType ()
		{
			return element_type.GetElementType ();
		}
	}
}
