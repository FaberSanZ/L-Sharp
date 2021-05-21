// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;
using System.Threading;
using LSharp.IL.Collections.Generic;

namespace LSharp.IL
{

	public interface ICustomAttributeProvider : IMetadataTokenProvider {

		Collection<CustomAttribute> CustomAttributes { get; }

		bool HasCustomAttributes { get; }
	}

	static partial class Mixin {

		public static bool GetHasCustomAttributes (
			this ICustomAttributeProvider self,
			ModuleDefinition module)
		{
			return module.HasImage () && module.Read (self, (provider, reader) => reader.HasCustomAttributes (provider));
		}

		public static Collection<CustomAttribute> GetCustomAttributes (
			this ICustomAttributeProvider self,
			ref Collection<CustomAttribute> variable,
			ModuleDefinition module)
		{
			if (module.HasImage ())
				return module.Read (ref variable, self, (provider, reader) => reader.ReadCustomAttributes (provider));

			Interlocked.CompareExchange (ref variable, new Collection<CustomAttribute> (), null);
			return variable;
		}
	}
}
