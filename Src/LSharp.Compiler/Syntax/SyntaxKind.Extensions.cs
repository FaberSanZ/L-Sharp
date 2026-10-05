// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	SyntaxKindExtensions.cs
====================================================================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LSharp.Compiler.Syntax
{
    internal static class SyntaxKindExtensions
    {
        private static readonly Dictionary<SyntaxKind, string> TokenTexts = new Dictionary<SyntaxKind, string>();


        static SyntaxKindExtensions()
        {

            IEnumerable<FieldInfo> fields = typeof(SyntaxKind).GetTypeInfo().DeclaredFields.Where(field => field.IsPublic && field.IsStatic);


            foreach (FieldInfo field in fields)
            {
                TokenTextAttribute tokenText = field.GetCustomAttribute<TokenTextAttribute>();

                if (tokenText is not null)
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
