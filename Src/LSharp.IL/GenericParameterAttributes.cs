// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	GenericParameterAttributes.cs
====================================================================================*/

using System;

namespace LSharp.IL
{

	[Flags]
	public enum GenericParameterAttributes : ushort {
		VarianceMask	= 0x0003,
		NonVariant		= 0x0000,
		Covariant		= 0x0001,
		Contravariant	= 0x0002,

		SpecialConstraintMask			= 0x001c,
		ReferenceTypeConstraint			= 0x0004,
		NotNullableValueTypeConstraint	= 0x0008,
		DefaultConstructorConstraint	= 0x0010
	}
}
