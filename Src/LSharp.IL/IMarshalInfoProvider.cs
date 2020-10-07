// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	IMarshalInfoProvider.cs
====================================================================================*/

namespace LSharp.IL
{

	public interface IMarshalInfoProvider : IMetadataTokenProvider {

		bool HasMarshalInfo { get; }
		MarshalInfo MarshalInfo { get; set; }
	}

	static partial class Mixin {

		public static bool GetHasMarshalInfo (
			this IMarshalInfoProvider self,
			ModuleDefinition module)
		{
			return module.HasImage () && module.Read (self, (provider, reader) => reader.HasMarshalInfo (provider));
		}

		public static MarshalInfo GetMarshalInfo (
			this IMarshalInfoProvider self,
			ref MarshalInfo variable,
			ModuleDefinition module)
		{
			return module.HasImage ()
				? module.Read (ref variable, self, (provider, reader) => reader.ReadMarshalInfo (provider))
				: null;
		}
	}
}
