using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ZeckLyn
{
    internal static class TokenTypeExtensions
    {
        private static readonly Dictionary<TokenType, string> TokenTexts = new Dictionary<TokenType, string>();


        static TokenTypeExtensions()
        {
            Type t = typeof(TokenType);

            IEnumerable<FieldInfo> members = t.GetTypeInfo().DeclaredFields.Where(field => field.IsPublic && field.IsStatic);


            foreach (var field in members)
            {
                TokenTextAttribute tokenText = field.GetCustomAttribute<TokenTextAttribute>();
                if (tokenText != null)
                {
                    TokenType type = (TokenType)field.GetValue(default);
                    TokenTexts.Add(type, tokenText.Text);
                }
            }
        }


        public static bool HasText(this TokenType type)
        {
            return TokenTexts.ContainsKey(type);
        }

        public static string ToText(this TokenType type)
        {
            TokenTexts.TryGetValue(type, out string value);
            return value;
        }
    }
}
