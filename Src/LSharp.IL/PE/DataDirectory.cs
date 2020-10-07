// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	DataDirectory.cs
====================================================================================*/

using System;

using RVA = System.UInt32;

namespace LSharp.IL.PE {

	struct DataDirectory {

		public readonly RVA VirtualAddress;
		public readonly uint Size;

		public bool IsZero {
			get { return VirtualAddress == 0 && Size == 0; }
		}

		public DataDirectory (RVA rva, uint size)
		{
			this.VirtualAddress = rva;
			this.Size = size;
		}
	}
}
