// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


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
