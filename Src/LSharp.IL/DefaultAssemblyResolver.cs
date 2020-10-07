// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	DefaultAssemblyResolver.cs
====================================================================================*/

using System;
using System.Collections.Generic;

namespace LSharp.IL
{

	public class DefaultAssemblyResolver : BaseAssemblyResolver {

		readonly IDictionary<string, AssemblyDefinition> cache;

		public DefaultAssemblyResolver ()
		{
			cache = new Dictionary<string, AssemblyDefinition> (StringComparer.Ordinal);
		}

		public override AssemblyDefinition Resolve (AssemblyNameReference name)
		{
			Mixin.CheckName (name);

			AssemblyDefinition assembly;
			if (cache.TryGetValue (name.FullName, out assembly))
				return assembly;

			assembly = base.Resolve (name);
			cache [name.FullName] = assembly;

			return assembly;
		}

		protected void RegisterAssembly (AssemblyDefinition assembly)
		{
			if (assembly == null)
				throw new ArgumentNullException ("assembly");

			var name = assembly.Name.FullName;
			if (cache.ContainsKey (name))
				return;

			cache [name] = assembly;
		}

		protected override void Dispose (bool disposing)
		{
			foreach (var assembly in cache.Values)
				assembly.Dispose ();

			cache.Clear ();

			base.Dispose (disposing);
		}
	}
}
