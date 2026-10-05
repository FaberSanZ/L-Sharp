// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System.Threading;
using LSharp.IL.Cecil;
using LSharp.IL.Collections.Generic;

namespace LSharp.IL
{

	public interface IGenericParameterProvider : IMetadataTokenProvider {

		bool HasGenericParameters { get; }
		bool IsDefinition { get; }
		ModuleDefinition Module { get; }
		Collection<GenericParameter> GenericParameters { get; }
		GenericParameterType GenericParameterType { get; }
	}

	public enum GenericParameterType {
		Type,
		Method
	}

	interface IGenericContext {

		bool IsDefinition { get; }
		IGenericParameterProvider Type { get; }
		IGenericParameterProvider Method { get; }
	}

	static partial class Mixin {

		public static bool GetHasGenericParameters (
			this IGenericParameterProvider self,
			ModuleDefinition module)
		{
			return module.HasImage () && module.Read (self, (provider, reader) => reader.HasGenericParameters (provider));
		}

		public static Collection<GenericParameter> GetGenericParameters (
			this IGenericParameterProvider self,
			ref Collection<GenericParameter> collection,
			ModuleDefinition module)
		{
			if (module.HasImage ())
				return module.Read (ref collection, self, (provider, reader) => reader.ReadGenericParameters (provider));

			Interlocked.CompareExchange (ref collection, new GenericParameterCollection (self), null);
			return collection;
		}
	}
}
