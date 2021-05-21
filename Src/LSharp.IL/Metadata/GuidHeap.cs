// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;

namespace LSharp.IL.Metadata {

	public sealed class GuidHeap : Heap {

		public GuidHeap (byte [] data)
			: base (data)
		{
		}

		public Guid Read (uint index)
		{
			const int guid_size = 16;

			if (index == 0 || ((index - 1) + guid_size) > data.Length)
				return new Guid ();

			var buffer = new byte [guid_size];

			Buffer.BlockCopy (this.data, (int) ((index - 1) * guid_size), buffer, 0, guid_size);

			return new Guid (buffer);
		}
	}
}
