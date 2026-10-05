// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;

using LSharp.IL.Collections.Generic;

namespace LSharp.IL
{

	sealed class ParameterDefinitionCollection : Collection<ParameterDefinition> {

		readonly IMethodSignature method;

		internal ParameterDefinitionCollection (IMethodSignature method)
		{
			this.method = method;
		}

		internal ParameterDefinitionCollection (IMethodSignature method, int capacity)
			: base (capacity)
		{
			this.method = method;
		}

		protected override void OnAdd (ParameterDefinition item, int index)
		{
			item.method = method;
			item.index = index;
		}

		protected override void OnInsert (ParameterDefinition item, int index)
		{
			item.method = method;
			item.index = index;

			for (int i = index; i < size; i++)
				items [i].index = i + 1;
		}

		protected override void OnSet (ParameterDefinition item, int index)
		{
			item.method = method;
			item.index = index;
		}

		protected override void OnRemove (ParameterDefinition item, int index)
		{
			item.method = null;
			item.index = -1;

			for (int i = index + 1; i < size; i++)
				items [i].index = i - 1;
		}
	}
}
