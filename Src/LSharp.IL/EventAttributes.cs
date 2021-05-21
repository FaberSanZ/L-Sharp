// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;

namespace LSharp.IL
{

    [Flags]
    public enum EventAttributes : ushort
    {
        None = 0x0000,
        SpecialName = 0x0200,   // Event is special
        RTSpecialName = 0x0400   // CLI provides 'special' behavior, depending upon the name of the event
    }
}
