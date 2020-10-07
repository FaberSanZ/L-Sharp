// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	GenericInstanceType.cs
====================================================================================*/

using System;
using System.Text;
using System.Threading;
using LSharp.IL.Collections.Generic;

using MD = LSharp.IL.Metadata;

namespace LSharp.IL
{

	public sealed class GenericInstanceType : TypeSpecification, IGenericInstance, IGenericContext {

		Collection<TypeReference> arguments;

		public bool HasGenericArguments {
			get { return !arguments.IsNullOrEmpty (); }
		}

		public Collection<TypeReference> GenericArguments {
			get {
				if (arguments == null)
					Interlocked.CompareExchange (ref arguments, new Collection<TypeReference> (), null);

				return arguments;
			}
		}

		public override TypeReference DeclaringType {
			get { return ElementType.DeclaringType; }
			set { throw new NotSupportedException (); }
		}

		public override string FullName {
			get {
				var name = new StringBuilder ();
				name.Append (base.FullName);
				this.GenericInstanceFullName (name);
				return name.ToString ();
			}
		}

		public override bool IsGenericInstance {
			get { return true; }
		}

		public override bool ContainsGenericParameter {
			get { return this.ContainsGenericParameter () || base.ContainsGenericParameter; }
		}

		IGenericParameterProvider IGenericContext.Type {
			get { return ElementType; }
		}

		public GenericInstanceType (TypeReference type)
			: base (type)
		{
			base.IsValueType = type.IsValueType;
			this.etype = MD.ElementType.GenericInst;
		}

		internal GenericInstanceType (TypeReference type, int arity)
			: this (type)
		{
			this.arguments = new Collection<TypeReference> (arity);
		}
	}
}
