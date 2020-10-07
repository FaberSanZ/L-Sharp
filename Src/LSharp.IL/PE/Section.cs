// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	Section.cs
====================================================================================*/

using System;

using RVA = System.UInt32;

namespace LSharp.IL.PE {

	sealed class Section {
		public string Name;
		public RVA VirtualAddress;
		public uint VirtualSize;
		public uint SizeOfRawData;
		public uint PointerToRawData;
	}
}
