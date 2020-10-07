// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	MethodCallingConvention.cs
====================================================================================*/

namespace LSharp.IL
{

    public enum MethodCallingConvention : byte
    {
        Default = 0x0,
        C = 0x1,
        StdCall = 0x2,
        ThisCall = 0x3,
        FastCall = 0x4,
        VarArg = 0x5,
        Generic = 0x10,
    }
}
