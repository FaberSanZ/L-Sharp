// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	ManifestResourceAttributes.cs
====================================================================================*/

using System;

namespace LSharp.IL
{

    [Flags]
    public enum ManifestResourceAttributes : uint
    {
        VisibilityMask = 0x0007,
        Public = 0x0001,    // The resource is exported from the Assembly
        Private = 0x0002     // The resource is private to the Assembly
    }
}
