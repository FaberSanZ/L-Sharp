using System;
using System.Collections.Generic;
using System.Text;

namespace LSharp
{

    [AttributeUsage(AttributeTargets.Field)]
    internal class TokenTextAttribute : Attribute
    {
        public TokenTextAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
