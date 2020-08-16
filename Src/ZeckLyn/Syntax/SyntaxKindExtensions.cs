// Copyright (c) 2020 Faber Leonardo. All Rights Reserved.

/*=============================================================================
	SyntaxKindExtensions.cs
=============================================================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LSharp
{
    internal static class SyntaxKindExtensions
    {
        private static readonly Dictionary<SyntaxKind, string> TokenTexts = new Dictionary<SyntaxKind, string>();


        static SyntaxKindExtensions()
        {
            Type t = typeof(SyntaxKind);

            IEnumerable<FieldInfo> members = t.GetTypeInfo().DeclaredFields.Where(field => field.IsPublic && field.IsStatic);


            foreach (FieldInfo field in members)
            {
                TokenTextAttribute tokenText = field.GetCustomAttribute<TokenTextAttribute>();
                if (tokenText != null)
                {
                    SyntaxKind type = (SyntaxKind)field.GetValue(default);
                    TokenTexts.Add(type, tokenText.Text);
                }
            }
        }


        public static bool HasText(this SyntaxKind type)
        {
            return TokenTexts.ContainsKey(type);
        }

        public static string ToText(this SyntaxKind type)
        {
            TokenTexts.TryGetValue(type, out string value);
            return value;
        }
    }
}
