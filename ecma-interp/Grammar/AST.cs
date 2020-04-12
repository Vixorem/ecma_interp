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
            public string Text { get; set; }
        }

        public class SingleExprNode : Node
        {

        }

        public class IdentNode : Node
        {
            public string Name { get; set; }
        }

        public class ExprSequenceNode : Node
        {
            public List<SingleExprNode> Exprs { get; set; }
        }

        public class BlockNode : Node
        {
            public StatementListNode Statements { get; set; }
        }

        public class ReturnNode : Node
        {
            public ExprSequenceNode ExprSeq { get; set; }
        }

        public class ContinueNode : Node
        {

        }

        public class BreakNode : Node
        {

        }

        // dynamic fields are used as a single statement
        public class WhileNode : Node
        {
            public ExprSequenceNode ExprSeq { get; set; }
            public dynamic Statement { get; set; }
        }

        public class StatementListNode : Node
        {
            public List<dynamic> Statements { get; set; }
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
            public string Name { get; set; }
            public FormalParamList Params { get; set; }
            public dynamic Statement { get; set; }
        }

        public class FormalParamList : Node
        {
            public List<IdentNode> Idents { get; set; }
        }

        public class IfNode : Node
        {
            public ExprSequenceNode ExprSeq { get; set; }
            public dynamic Statement { get; set; }
            public dynamic ElseStatement { get; set; }
        }

        public class MemberIndExpr : SingleExprNode
        {

        }
    }
}
