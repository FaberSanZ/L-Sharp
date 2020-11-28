// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	DataDirectory.cs
====================================================================================*/

namespace LSharp.IL.PE
{

    public struct DataDirectory
    {
        public DataDirectory(uint rva, uint size)
        {
            VirtualAddress = rva;
            Size = size;
        }

        public uint VirtualAddress;
        public uint Size;

        public bool IsZero => VirtualAddress == 0 && Size == 0;

    }
}
