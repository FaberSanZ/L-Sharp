// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)

namespace LSharp.IL
{

	public interface IConstantProvider : IMetadataTokenProvider {

		bool HasConstant { get; set; }
		object Constant { get; set; }
	}

	static partial class Mixin {

		internal static object NoValue = new object ();
		internal static object NotResolved = new object ();

		public static void ResolveConstant (
			this IConstantProvider self,
			ref object constant,
			ModuleDefinition module)
		{
			if (module == null) {
				constant = Mixin.NoValue;
				return;
			}

			lock (module.SyncRoot) {
				if (constant != Mixin.NotResolved)
					return;
				if (module.HasImage ())
					constant = module.Read (self, (provider, reader) => reader.ReadConstant (provider));
				else
					constant = Mixin.NoValue;
			}
		}
	}
}
