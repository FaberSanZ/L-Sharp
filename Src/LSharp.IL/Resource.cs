// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


namespace LSharp.IL
{

	public enum ResourceType {
		Linked,
		Embedded,
		AssemblyLinked,
	}

	public abstract class Resource {

		string name;
		uint attributes;

		public string Name {
			get { return name; }
			set { name = value; }
		}

		public ManifestResourceAttributes Attributes {
			get { return (ManifestResourceAttributes) attributes; }
			set { attributes = (uint) value; }
		}

		public abstract ResourceType ResourceType {
			get;
		}


		public bool IsPublic {
			get { return attributes.GetMaskedAttributes ((uint) ManifestResourceAttributes.VisibilityMask, (uint) ManifestResourceAttributes.Public); }
			set { attributes = attributes.SetMaskedAttributes ((uint) ManifestResourceAttributes.VisibilityMask, (uint) ManifestResourceAttributes.Public, value); }
		}

		public bool IsPrivate {
			get { return attributes.GetMaskedAttributes ((uint) ManifestResourceAttributes.VisibilityMask, (uint) ManifestResourceAttributes.Private); }
			set { attributes = attributes.SetMaskedAttributes ((uint) ManifestResourceAttributes.VisibilityMask, (uint) ManifestResourceAttributes.Private, value); }
		}


		internal Resource (string name, ManifestResourceAttributes attributes)
		{
			this.name = name;
			this.attributes = (uint) attributes;
		}
	}
}
