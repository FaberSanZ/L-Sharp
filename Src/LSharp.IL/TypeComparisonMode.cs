// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)



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
