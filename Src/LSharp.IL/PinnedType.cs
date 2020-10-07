// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	PinnedType.cs
====================================================================================*/

using System;

using MD = LSharp.IL.Metadata;

namespace LSharp.IL
{

	public sealed class PinnedType : TypeSpecification {

		public override bool IsValueType {
			get { return false; }
			set { throw new InvalidOperationException (); }
		}

		public override bool IsPinned {
			get { return true; }
		}

		public PinnedType (TypeReference type)
			: base (type)
		{
			Mixin.CheckType (type);
			this.etype = MD.ElementType.Pinned;
		}
	}
}
