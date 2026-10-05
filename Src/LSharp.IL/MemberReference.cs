// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;

namespace LSharp.IL
{

	public abstract class MemberReference : IMetadataTokenProvider {

		string name;
		TypeReference declaring_type;

		internal MetadataToken token;
		internal object projection;

		public virtual string Name {
			get { return name; }
			set {
				if (IsWindowsRuntimeProjection && value != name)
					throw new InvalidOperationException ();

				name = value;
			}
		}

		public abstract string FullName {
			get;
		}

		public virtual TypeReference DeclaringType {
			get { return declaring_type; }
			set { declaring_type = value; }
		}

		public MetadataToken MetadataToken {
			get { return token; }
			set { token = value; }
		}

		public bool IsWindowsRuntimeProjection {
			get { return projection != null; }
		}

		internal bool HasImage {
			get {
				var module = Module;
				if (module == null)
					return false;

				return module.HasImage;
			}
		}

		public virtual ModuleDefinition Module {
			get { return declaring_type != null ? declaring_type.Module : null; }
		}

		public virtual bool IsDefinition {
			get { return false; }
		}

		public virtual bool ContainsGenericParameter {
			get { return declaring_type != null && declaring_type.ContainsGenericParameter; }
		}

		internal MemberReference ()
		{
		}

		internal MemberReference (string name)
		{
			this.name = name ?? string.Empty;
		}

		internal string MemberFullName ()
		{
			if (declaring_type == null)
				return name;

			return declaring_type.FullName + "::" + name;
		}

		public IMemberDefinition Resolve ()
		{
			return ResolveDefinition ();
		}

		protected abstract IMemberDefinition ResolveDefinition ();

		public override string ToString ()
		{
			return FullName;
		}
	}
}
