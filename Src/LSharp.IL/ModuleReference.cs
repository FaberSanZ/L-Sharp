// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	ModuleReference.cs
====================================================================================*/

namespace LSharp.IL
{

	public class ModuleReference : IMetadataScope {

		string name;

		internal MetadataToken token;

		public string Name {
			get { return name; }
			set { name = value; }
		}

		public virtual MetadataScopeType MetadataScopeType {
			get { return MetadataScopeType.ModuleReference; }
		}

		public MetadataToken MetadataToken {
			get { return token; }
			set { token = value; }
		}

		internal ModuleReference ()
		{
			this.token = new MetadataToken (TokenType.ModuleRef);
		}

		public ModuleReference (string name)
			: this ()
		{
			this.name = name;
		}

		public override string ToString ()
		{
			return name;
		}
	}
}
