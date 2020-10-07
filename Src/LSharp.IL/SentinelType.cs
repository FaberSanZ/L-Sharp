// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	SentinelType.cs
====================================================================================*/

using System;

using MD = LSharp.IL.Metadata;

namespace LSharp.IL
{

	public sealed class SentinelType : TypeSpecification {

		public override bool IsValueType {
			get { return false; }
			set { throw new InvalidOperationException (); }
		}

		public override bool IsSentinel {
			get { return true; }
		}

		public SentinelType (TypeReference type)
			: base (type)
		{
			Mixin.CheckType (type);
			this.etype = MD.ElementType.Sentinel;
		}
	}
}
