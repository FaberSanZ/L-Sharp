// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	MetadataScopeType.cs
====================================================================================*/

namespace LSharp.IL
{

    public enum MetadataScopeType
    {
        AssemblyNameReference,
        ModuleReference,
        ModuleDefinition,
    }

    public interface IMetadataScope : IMetadataTokenProvider
    {
        MetadataScopeType MetadataScopeType { get; }
        string Name { get; set; }
    }
}
