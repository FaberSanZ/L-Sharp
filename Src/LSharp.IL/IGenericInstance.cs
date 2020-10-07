// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	IGenericInstance.cs
====================================================================================*/

using System.Text;

using LSharp.IL.Collections.Generic;

namespace LSharp.IL
{

	public interface IGenericInstance : IMetadataTokenProvider {

		bool HasGenericArguments { get; }
		Collection<TypeReference> GenericArguments { get; }
	}

	static partial class Mixin {

		public static bool ContainsGenericParameter (this IGenericInstance self)
		{
			var arguments = self.GenericArguments;

			for (int i = 0; i < arguments.Count; i++)
				if (arguments [i].ContainsGenericParameter)
					return true;

			return false;
		}

		public static void GenericInstanceFullName (this IGenericInstance self, StringBuilder builder)
		{
			builder.Append ("<");
			var arguments = self.GenericArguments;
			for (int i = 0; i < arguments.Count; i++) {
				if (i > 0)
					builder.Append (",");
				builder.Append (arguments [i].FullName);
			}
			builder.Append (">");
		}
	}
}
