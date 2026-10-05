// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)

using System.Text;

using LSharp.IL.Collections.Generic;

namespace LSharp.IL
{

	public interface IMethodSignature : IMetadataTokenProvider {

		bool HasThis { get; set; }
		bool ExplicitThis { get; set; }
		MethodCallingConvention CallingConvention { get; set; }

		bool HasParameters { get; }
		Collection<ParameterDefinition> Parameters { get; }
		TypeReference ReturnType { get; set; }
		MethodReturnType MethodReturnType { get; }
	}

	static partial class Mixin {

		public static bool HasImplicitThis (this IMethodSignature self)
		{
			return self.HasThis && !self.ExplicitThis;
		}

		public static void MethodSignatureFullName (this IMethodSignature self, StringBuilder builder)
		{
			builder.Append ("(");

			if (self.HasParameters) {
				var parameters = self.Parameters;
				for (int i = 0; i < parameters.Count; i++) {
					var parameter = parameters [i];
					if (i > 0)
						builder.Append (",");

					if (parameter.ParameterType.IsSentinel)
						builder.Append ("...,");

					builder.Append (parameter.ParameterType.FullName);
				}
			}

			builder.Append (")");
		}
	}
}
