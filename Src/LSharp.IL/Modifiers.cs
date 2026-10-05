// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;

using MD = LSharp.IL.Metadata;

namespace LSharp.IL {

	public interface IModifierType {
		TypeReference ModifierType { get; }
		TypeReference ElementType { get; }
	}

	public sealed class OptionalModifierType : TypeSpecification, IModifierType {

		TypeReference modifier_type;

		public TypeReference ModifierType {
			get { return modifier_type; }
			set { modifier_type = value; }
		}

		public override string Name {
			get { return base.Name + Suffix; }
		}

		public override string FullName {
			get { return base.FullName + Suffix; }
		}

		string Suffix {
			get { return " modopt(" + modifier_type + ")"; }
		}

		public override bool IsValueType {
			get { return false; }
			set { throw new InvalidOperationException (); }
		}

		public override bool IsOptionalModifier {
			get { return true; }
		}

		public override bool ContainsGenericParameter {
			get { return modifier_type.ContainsGenericParameter || base.ContainsGenericParameter; }
		}

		public OptionalModifierType (TypeReference modifierType, TypeReference type)
			: base (type)
		{
			if (modifierType == null)
				throw new ArgumentNullException (Mixin.Argument.modifierType.ToString ());
			Mixin.CheckType (type);
			this.modifier_type = modifierType;
			this.etype = MD.ElementType.CModOpt;
		}
	}

	public sealed class RequiredModifierType : TypeSpecification, IModifierType {

		TypeReference modifier_type;

		public TypeReference ModifierType {
			get { return modifier_type; }
			set { modifier_type = value; }
		}

		public override string Name {
			get { return base.Name + Suffix; }
		}

		public override string FullName {
			get { return base.FullName + Suffix; }
		}

		string Suffix {
			get { return " modreq(" + modifier_type + ")"; }
		}

		public override bool IsValueType {
			get { return false; }
			set { throw new InvalidOperationException (); }
		}

		public override bool IsRequiredModifier {
			get { return true; }
		}

		public override bool ContainsGenericParameter {
			get { return modifier_type.ContainsGenericParameter || base.ContainsGenericParameter; }
		}

		public RequiredModifierType (TypeReference modifierType, TypeReference type)
			: base (type)
		{
			if (modifierType == null)
				throw new ArgumentNullException (Mixin.Argument.modifierType.ToString ());
			Mixin.CheckType (type);
			this.modifier_type = modifierType;
			this.etype = MD.ElementType.CModReqD;
		}

	}
}
