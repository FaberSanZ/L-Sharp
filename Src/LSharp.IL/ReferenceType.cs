// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	ByReferenceType.cs
====================================================================================*/

using System;

using MD = LSharp.IL.Metadata;

namespace LSharp.IL
{

	public sealed class ByReferenceType : TypeSpecification {

		public override string Name {
			get { return base.Name + "&"; }
		}

		public override string FullName {
			get { return base.FullName + "&"; }
		}

		public override bool IsValueType {
			get { return false; }
			set { throw new InvalidOperationException (); }
		}

		public override bool IsByReference {
			get { return true; }
		}

		public ByReferenceType (TypeReference type)
			: base (type)
		{
			Mixin.CheckType (type);
			this.etype = MD.ElementType.ByRef;
		}
	}
}
