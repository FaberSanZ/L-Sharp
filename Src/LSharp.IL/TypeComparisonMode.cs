// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	TypeComparisonMode.cs
====================================================================================*/


namespace LSharp.IL
{
    internal enum TypeComparisonMode
    {
        Exact,
        SignatureOnly,

        /// <summary>
        /// Types can be in different assemblies, as long as the module, assembly, and type names match they will be considered equal
        /// </summary>
        SignatureOnlyLoose
    }
}
