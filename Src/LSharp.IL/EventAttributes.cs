// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	EventAttributes.cs
====================================================================================*/

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
