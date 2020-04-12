using System;
using System.Collections.Generic;
using System.Text;

namespace ecma_interp.Grammar
{
    class ASTRepresenter
    {
        private int Shift { get; set; }

        public ASTRepresenter(AST.Node root, int shift = 4)
        {
            Shift = shift;
        }
    }
}
