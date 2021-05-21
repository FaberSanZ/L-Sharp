// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


namespace LSharp.IL
{
    internal enum FileAttributes : uint
    {
        ContainsMetaData = 0x0000,  // This is not a resource file
        ContainsNoMetaData = 0x0001,    // This is a resource file or other non-metadata-containing file
    }
}
