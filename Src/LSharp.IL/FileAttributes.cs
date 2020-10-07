// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	FileAttributes.cs
====================================================================================*/

namespace LSharp.IL
{
    internal enum FileAttributes : uint
    {
        ContainsMetaData = 0x0000,  // This is not a resource file
        ContainsNoMetaData = 0x0001,    // This is a resource file or other non-metadata-containing file
    }
}
