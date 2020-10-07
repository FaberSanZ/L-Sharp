// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	ParameterAttributes.cs
====================================================================================*/

using System;

namespace LSharp.IL
{

    [Flags]
    public enum ParameterAttributes : ushort
    {
        None = 0x0000,
        In = 0x0001,    // Param is [In]
        Out = 0x0002,   // Param is [Out]
        Lcid = 0x0004,
        Retval = 0x0008,
        Optional = 0x0010,  // Param is optional
        HasDefault = 0x1000,    // Param has default value
        HasFieldMarshal = 0x2000,   // Param has field marshal
        Unused = 0xcfe0  // Reserved: shall be zero in a conforming implementation
    }
}
