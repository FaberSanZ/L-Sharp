// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


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
