// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	AssemblyLinkedResource.cs
====================================================================================*/

using System;

namespace LSharp.IL
{

	public sealed class AssemblyLinkedResource : Resource {

		AssemblyNameReference reference;

		public AssemblyNameReference Assembly {
			get { return reference; }
			set { reference = value; }
		}

		public override ResourceType ResourceType {
			get { return ResourceType.AssemblyLinked; }
		}

		public AssemblyLinkedResource (string name, ManifestResourceAttributes flags)
			: base (name, flags)
		{
		}

		public AssemblyLinkedResource (string name, ManifestResourceAttributes flags, AssemblyNameReference reference)
			: base (name, flags)
		{
			this.reference = reference;
		}
	}
}
