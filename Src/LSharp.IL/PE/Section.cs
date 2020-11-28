// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	Section.cs
====================================================================================*/

using System;


namespace LSharp.IL.PE {

	public sealed class Section 
	{
		public string Name;
		public uint VirtualAddress;
		public uint VirtualSize;
		public uint SizeOfRawData;
		public uint PointerToRawData;
	}
}
