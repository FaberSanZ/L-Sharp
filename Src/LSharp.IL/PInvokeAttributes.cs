// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;

namespace LSharp.IL
{

    [Flags]
    public enum PInvokeAttributes : ushort
    {
        NoMangle = 0x0001,  // PInvoke is to use the member name as specified

        // Character set
        CharSetMask = 0x0006,
        CharSetNotSpec = 0x0000,
        CharSetAnsi = 0x0002,
        CharSetUnicode = 0x0004,
        CharSetAuto = 0x0006,

        SupportsLastError = 0x0040, // Information about target function. Not relevant for fields

        // Calling convetion
        CallConvMask = 0x0700,
        CallConvWinapi = 0x0100,
        CallConvCdecl = 0x0200,
        CallConvStdCall = 0x0300,
        CallConvThiscall = 0x0400,
        CallConvFastcall = 0x0500,

        BestFitMask = 0x0030,
        BestFitEnabled = 0x0010,
        BestFitDisabled = 0x0020,

        ThrowOnUnmappableCharMask = 0x3000,
        ThrowOnUnmappableCharEnabled = 0x1000,
        ThrowOnUnmappableCharDisabled = 0x2000,
    }
}
