// Copyright (c) 2020 Faber Leonardo. All Rights Reserved.

/*=============================================================================
	SyntaxNode.cs
=============================================================================*/


using System;
using System.Collections.Generic;
using System.Text;

namespace ZeckLyn
{
    abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}
