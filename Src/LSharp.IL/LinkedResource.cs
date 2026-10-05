// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)



namespace LSharp.IL
{

	public sealed class LinkedResource : Resource {

		internal byte [] hash;
		string file;

		public byte [] Hash {
			get { return hash; }
		}

		public string File {
			get { return file; }
			set { file = value; }
		}

		public override ResourceType ResourceType {
			get { return ResourceType.Linked; }
		}

		public LinkedResource (string name, ManifestResourceAttributes flags)
			: base (name, flags)
		{
		}

		public LinkedResource (string name, ManifestResourceAttributes flags, string file)
			: base (name, flags)
		{
			this.file = file;
		}
	}
}
