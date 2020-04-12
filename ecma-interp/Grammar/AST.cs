using System;
using System.Collections.Generic;
using System.Text;

namespace ecma_interp.Grammar
{
    /*
     The class represents Abstract Syntax Tree for JavaScript grammar.
     It's built within ECMAVisitor class.
         */

    // TODO: Class fields are not implemented mostly but will be further.
    class AST
    {
        public abstract class Node
        {
            public int Start { get; set; }
            public int End { get; set; }
        }

        public class SingleExprNode : Node
        {

        }

        public class BlockNode : Node
        {

        }

        public class ProgramNode : Node
        {
            public dynamic Statement { get; set; }
        }

        public class EmptyNode : Node
        {

        }

        public class VarDeclNode : Node
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public class VarDeclListNode : Node
        {
            public List<VarDeclNode> VarDecls { get; set; } = new List<VarDeclNode>();
        }

        public class FunctionExprNode : SingleExprNode
        {

        }

        public class MemberIndexExprNode : SingleExprNode
        {

        }

        public class MemberDotExprNode : SingleExprNode
        {

        }

        public class ArgumentExprNode : SingleExprNode
        {

        }

        public class ArgumentListNode : SingleExprNode
        {

        }

    }
}
