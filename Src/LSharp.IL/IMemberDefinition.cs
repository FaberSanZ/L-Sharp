// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)

namespace LSharp.IL
{

	public interface IMemberDefinition : ICustomAttributeProvider {

		string Name { get; set; }
		string FullName { get; }

		bool IsSpecialName { get; set; }
		bool IsRuntimeSpecialName { get; set; }

		TypeDefinition DeclaringType { get; set; }
	}

	static partial class Mixin {

		public static bool GetAttributes (this uint self, uint attributes)
		{
			return (self & attributes) != 0;
		}

		public static uint SetAttributes (this uint self, uint attributes, bool value)
		{
			if (value)
				return self | attributes;

			return self & ~attributes;
		}

		public static bool GetMaskedAttributes (this uint self, uint mask, uint attributes)
		{
			return (self & mask) == attributes;
		}

		public static uint SetMaskedAttributes (this uint self, uint mask, uint attributes, bool value)
		{
			if (value) {
				self &= ~mask;
				return self | attributes;
			}

			return self & ~(mask & attributes);
		}

		public static bool GetAttributes (this ushort self, ushort attributes)
		{
			return (self & attributes) != 0;
		}

		public static ushort SetAttributes (this ushort self, ushort attributes, bool value)
		{
			if (value)
				return (ushort) (self | attributes);

			return (ushort) (self & ~attributes);
		}

		public static bool GetMaskedAttributes (this ushort self, ushort mask, uint attributes)
		{
			return (self & mask) == attributes;
		}

		public static ushort SetMaskedAttributes (this ushort self, ushort mask, uint attributes, bool value)
		{
			if (value) {
				self = (ushort) (self & ~mask);
				return (ushort) (self | attributes);
			}

			return (ushort) (self & ~(mask & attributes));
		}
	}
}
