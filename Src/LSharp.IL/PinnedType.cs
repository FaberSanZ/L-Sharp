// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


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
