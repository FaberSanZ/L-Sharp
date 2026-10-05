// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;

using LSharp.IL.Collections.Generic;

namespace LSharp.IL
{

	sealed class MemberDefinitionCollection<T> : Collection<T> where T : IMemberDefinition {

		TypeDefinition container;

		internal MemberDefinitionCollection (TypeDefinition container)
		{
			this.container = container;
		}

		internal MemberDefinitionCollection (TypeDefinition container, int capacity)
			: base (capacity)
		{
			this.container = container;
		}

		protected override void OnAdd (T item, int index)
		{
			Attach (item);
		}

		protected sealed override void OnSet (T item, int index)
		{
			Attach (item);
		}

		protected sealed override void OnInsert (T item, int index)
		{
			Attach (item);
		}

		protected sealed override void OnRemove (T item, int index)
		{
			Detach (item);
		}

		protected sealed override void OnClear ()
		{
			foreach (var definition in this)
				Detach (definition);
		}

		void Attach (T element)
		{
			if (element.DeclaringType == container)
				return;

			if (element.DeclaringType != null)
				throw new ArgumentException ("Member already attached");

			element.DeclaringType = this.container;
		}

		static void Detach (T element)
		{
			element.DeclaringType = null;
		}
	}
}
