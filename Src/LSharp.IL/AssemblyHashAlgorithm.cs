// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	AssemblyHashAlgorithm.cs
====================================================================================*/

namespace LSharp.IL
{

    public enum AssemblyHashAlgorithm : uint
    {
        None = 0x0000,
        MD5 = 0x8003,
        SHA1 = 0x8004,
        SHA256 = 0x800C,
        SHA384 = 0x800D,
        SHA512 = 0x800E,
        Reserved = 0x8003, // MD5
    }
}
