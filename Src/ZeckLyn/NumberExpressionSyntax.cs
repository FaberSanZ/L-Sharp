using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Linq;

namespace ZeckLyn
{
    public sealed partial class NumberExpressionSyntax : ExpressionSyntax
    {
        public NumberExpressionSyntax(SyntaxToken numbertoken)
        {
            Numbertoken = numbertoken;

        }

        public override SyntaxKind Kind => SyntaxKind.BadToken;

        public SyntaxToken Numbertoken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Numbertoken;
        }
    }
}
