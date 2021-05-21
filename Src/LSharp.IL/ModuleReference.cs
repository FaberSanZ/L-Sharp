// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


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
