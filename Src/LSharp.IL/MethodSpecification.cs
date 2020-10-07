// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	MethodSpecification.cs
====================================================================================*/

using System;

using LSharp.IL.Collections.Generic;

namespace LSharp.IL
{

	public abstract class MethodSpecification : MethodReference {

		readonly MethodReference method;

		public MethodReference ElementMethod {
			get { return method; }
		}

		public override string Name {
			get { return method.Name; }
			set { throw new InvalidOperationException (); }
		}

		public override MethodCallingConvention CallingConvention {
			get { return method.CallingConvention; }
			set { throw new InvalidOperationException (); }
		}

		public override bool HasThis {
			get { return method.HasThis; }
			set { throw new InvalidOperationException (); }
		}

		public override bool ExplicitThis {
			get { return method.ExplicitThis; }
			set { throw new InvalidOperationException (); }
		}

		public override MethodReturnType MethodReturnType {
			get { return method.MethodReturnType; }
			set { throw new InvalidOperationException (); }
		}

		public override TypeReference DeclaringType {
			get { return method.DeclaringType; }
			set { throw new InvalidOperationException (); }
		}

		public override ModuleDefinition Module {
			get { return method.Module; }
		}

		public override bool HasParameters {
			get { return method.HasParameters; }
		}

		public override Collection<ParameterDefinition> Parameters {
			get { return method.Parameters; }
		}

		public override bool ContainsGenericParameter {
			get { return method.ContainsGenericParameter; }
		}

		internal MethodSpecification (MethodReference method)
		{
			Mixin.CheckMethod (method);

			this.method = method;
			this.token = new MetadataToken (TokenType.MethodSpec);
		}

		public sealed override MethodReference GetElementMethod ()
		{
			return method.GetElementMethod ();
		}
	}
}
