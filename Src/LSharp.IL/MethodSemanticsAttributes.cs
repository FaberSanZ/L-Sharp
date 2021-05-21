// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)



using System;

namespace LSharp.IL
{

	[Flags]
	public enum MethodSemanticsAttributes : ushort {
		None		= 0x0000,
		Setter		= 0x0001,	// Setter for property
		Getter		= 0x0002,	// Getter for property
		Other		= 0x0004,	// Other method for property or event
		AddOn		= 0x0008,	// AddOn method for event
		RemoveOn	= 0x0010,	// RemoveOn method for event
		Fire		= 0x0020	 // Fire method for event
	}
}
