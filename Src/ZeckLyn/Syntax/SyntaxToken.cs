// Copyright (c) 2020 Faber Leonardo. All Rights Reserved.

/*=============================================================================
	SyntaxToken.cs
=============================================================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSharp
{
    public class SyntaxToken : SyntaxNode
    {

        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }


        public override SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}
