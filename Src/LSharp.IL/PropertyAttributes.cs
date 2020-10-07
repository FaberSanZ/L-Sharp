// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	PropertyAttributes.cs
====================================================================================*/

using System;

namespace LSharp.IL
{

	[Flags]
	public enum PropertyAttributes : ushort 
	{
		None			= 0x0000,
		SpecialName		= 0x0200,	// Property is special
		RTSpecialName	= 0x0400,	// Runtime(metadata internal APIs) should check name encoding
		HasDefault		= 0x1000,	// Property has default
		Unused			= 0xe9ff	 // Reserved: shall be zero in a conforming implementation
	}
}
