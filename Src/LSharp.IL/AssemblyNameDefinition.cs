// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;

namespace LSharp.IL
{

	public sealed class AssemblyNameDefinition : AssemblyNameReference {

		public override byte [] Hash {
			get { return Empty<byte>.Array; }
		}

		internal AssemblyNameDefinition ()
		{
			this.token = new MetadataToken (TokenType.Assembly, 1);
		}

		public AssemblyNameDefinition (string name, Version version)
			: base (name, version)
		{
			this.token = new MetadataToken (TokenType.Assembly, 1);
		}
	}
}
