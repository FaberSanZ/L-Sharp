// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	GenericInstanceMethod.cs
====================================================================================*/

using System;
using System.Text;
using System.Threading;
using LSharp.IL.Collections.Generic;

namespace LSharp.IL
{

	public sealed class GenericInstanceMethod : MethodSpecification, IGenericInstance, IGenericContext {

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

		public override bool IsGenericInstance {
			get { return true; }
		}

		IGenericParameterProvider IGenericContext.Method {
			get { return ElementMethod; }
		}

		IGenericParameterProvider IGenericContext.Type {
			get { return ElementMethod.DeclaringType; }
		}

		public override bool ContainsGenericParameter {
			get { return this.ContainsGenericParameter () || base.ContainsGenericParameter; }
		}

		public override string FullName {
			get {
				var signature = new StringBuilder ();
				var method = this.ElementMethod;
				signature.Append (method.ReturnType.FullName)
					.Append (" ")
					.Append (method.DeclaringType.FullName)
					.Append ("::")
					.Append (method.Name);
				this.GenericInstanceFullName (signature);
				this.MethodSignatureFullName (signature);
				return signature.ToString ();

			}
		}

		public GenericInstanceMethod (MethodReference method)
			: base (method)
		{
		}

		internal GenericInstanceMethod (MethodReference method, int arity)
			: this (method)
		{
			this.arguments = new Collection<TypeReference> (arity);
		}
	}
}
