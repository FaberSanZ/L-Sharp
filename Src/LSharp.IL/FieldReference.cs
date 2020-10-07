// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	FieldReference.cs
====================================================================================*/


using System;

namespace LSharp.IL
{

	public class FieldReference : MemberReference {

		TypeReference field_type;

		public TypeReference FieldType {
			get { return field_type; }
			set { field_type = value; }
		}

		public override string FullName {
			get { return field_type.FullName + " " + MemberFullName (); }
		}

		public override bool ContainsGenericParameter {
			get { return field_type.ContainsGenericParameter || base.ContainsGenericParameter; }
		}

		internal FieldReference ()
		{
			this.token = new MetadataToken (TokenType.MemberRef);
		}

		public FieldReference (string name, TypeReference fieldType)
			: base (name)
		{
			Mixin.CheckType (fieldType, Mixin.Argument.fieldType);

			this.field_type = fieldType;
			this.token = new MetadataToken (TokenType.MemberRef);
		}

		public FieldReference (string name, TypeReference fieldType, TypeReference declaringType)
			: this (name, fieldType)
		{
			Mixin.CheckType (declaringType, Mixin.Argument.declaringType);

			this.DeclaringType = declaringType;
		}

		protected override IMemberDefinition ResolveDefinition ()
		{
			return this.Resolve ();
		}

		public new virtual FieldDefinition Resolve ()
		{
			var module = this.Module;
			if (module == null)
				throw new NotSupportedException ();

			return module.Resolve (this);
		}
	}
}
