// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	TypeDefinitionTreatment.cs
====================================================================================*/

using System;

namespace LSharp.IL
{

    [Flags]
    internal enum TypeDefinitionTreatment
    {
        None = 0x0,

        KindMask = 0xf,
        NormalType = 0x1,
        NormalAttribute = 0x2,
        UnmangleWindowsRuntimeName = 0x3,
        PrefixWindowsRuntimeName = 0x4,
        RedirectToClrType = 0x5,
        RedirectToClrAttribute = 0x6,
        RedirectImplementedMethods = 0x7,

        Abstract = 0x10,
        Internal = 0x20,
    }

    internal enum TypeReferenceTreatment
    {
        None = 0x0,
        SystemDelegate = 0x1,
        SystemAttribute = 0x2,
        UseProjectionInfo = 0x3,
    }

    [Flags]
    internal enum MethodDefinitionTreatment
    {
        None = 0x0,
        Abstract = 0x2,
        Private = 0x4,
        Public = 0x8,
        Runtime = 0x10,
        InternalCall = 0x20,
    }

    internal enum FieldDefinitionTreatment
    {
        None = 0x0,
        Public = 0x1,
    }

    internal enum CustomAttributeValueTreatment
    {
        None = 0x0,
        AllowSingle = 0x1,
        AllowMultiple = 0x2,
        VersionAttribute = 0x3,
        DeprecatedAttribute = 0x4,
    }
}
