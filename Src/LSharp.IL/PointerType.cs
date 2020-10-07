// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	PointerType.cs
====================================================================================*/

using System;

using MD = LSharp.IL.Metadata;

namespace LSharp.IL
{

	public sealed class PointerType : TypeSpecification {

		public override string Name {
			get { return base.Name + "*"; }
		}

		public override string FullName {
			get { return base.FullName + "*"; }
		}

		public override bool IsValueType {
			get { return false; }
			set { throw new InvalidOperationException (); }
		}

		public override bool IsPointer {
			get { return true; }
		}

		public PointerType (TypeReference type)
			: base (type)
		{
			Mixin.CheckType (type);
			this.etype = MD.ElementType.Ptr;
		}
	}
}
