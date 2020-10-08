// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	AssemblyAttributes.cs
====================================================================================*/

using System;

namespace LSharp.IL
{

    [Flags]
    public enum AssemblyAttributes : uint
    {
        PublicKey = 0x0001,
        SideBySideCompatible = 0x0000,
        Retargetable = 0x0100,
        WindowsRuntime = 0x0200,
        DisableJITCompileOptimizer = 0x4000,
        EnableJITCompileTracking = 0x8000,
    }
}
