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
        public class Node
        {
            public int Start { get; set; }
            public int End { get; set; }
        }

        //public abstract class SingleExprNode : Node
        //{
        //}

        //public abstract class StatementNode : Node
        //{

        //}

        public class IdentNode : Node
        {
            public string Type = "Identifier";
            public string Name { get; set; }
        }

        public class InitialiserNode : Node
        {
            public string Type = "Initialiser";
            public Node Value { get; set; }
        }

        public class ExprSequenceNode : Node
        {
            public string Type = "Expression sequence";
            public List<Node> Exprs { get; set; }
        }

        public class BlockNode : Node
        {
            public string Type = "Block";
            public StatementListNode Statements { get; set; }
        }

        public class ReturnNode : Node
        {
            public string Type = "Return statement";
            public ExprSequenceNode ExprSeq { get; set; }
        }

        public class ContinueNode : Node
        {
            public string Type = "Continue statement";
        }

        public class BreakNode : Node
        {
            public string Type = "Break statement";
        }

        // dynamic fields are used as a single statement
        public class WhileNode : Node
        {
            public string Type = "While";
            public ExprSequenceNode Cond { get; set; }
            public Node Body { get; set; }
        }

        public class StatementListNode : Node
        {
            public string Type = "Statement list";
            public List<Node> Statements { get; set; }
        }

        public class ProgramNode : Node
        {
            public string Type = "Program";
            public List<Node> List { get; set; }
        }

        public class EmptyNode : Node
        {
            public string Type = "Empty statement";
        }

        public abstract class LiteralNode : Node
        {
            public string Type = "Literal";
            public string Value { get; set; }
        }

        public class NumericLiteralNode : LiteralNode
        {
            public new string Type = "Numeric literal";
            //TODO: enum for dec, hex, oct???
        }

        public class NullLiteralNode : LiteralNode
        {
            public new string Type = "Null literal";
        }

        public class BoolLiteralNode : LiteralNode
        {
            public new string Type = "Bool literal";
        }

        public class StringLiteralNode : LiteralNode
        {
            public new string Type = "String literal";
        }

        public class VarDeclNode : Node
        {
            public string Type = "Variable declaration";
            public string Name { get; set; }
            public InitialiserNode Init { get; set; }
        }

        public class VarDeclListNode : Node
        {
            public string Type = "Variable declaration list";
            public List<VarDeclNode> VarDecls { get; set; } = new List<VarDeclNode>();
        }

        public class FunctionExprNode : Node
        {
            public string Type = "Function expression";
            public string Name { get; set; }
            public FormalParamList Params { get; set; }
            public ExprSequenceNode FuncBody { get; set; }
        }

        public class FunctionDeclNode : Node
        {
            public string Type = "Function declaration";
            public string Name { get; set; }
            public FormalParamList Params { get; set; }
            public ExprSequenceNode FuncBody { get; set; }
        }

        public class FormalParamList : Node
        {
            public string Type = "Formal parameters list";
            public List<IdentNode> Idents { get; set; }
        }


        public class IfNode : Node
        {
            public string Type = "if statement";
            public ExprSequenceNode Cond { get; set; }
            public Node Statement { get; set; }
            public Node AlterStatement { get; set; }
        }

        public class MemberIndExprNode : Node
        {
            public string Type = "Member expression (index)";
            public Node Expr { get; set; }
            public ExprSequenceNode Ind { get; set; }
        }

        public class MemberDotExprNode : Node
        {
            public string Type = "Member expression (dot)";
        }

        public class ArgumentsExprNode : Node
        {
            public Node LeftExpr { get; set; }
            public List<Node> Args { get; set; }
        }

        public class NewExprNode : Node
        {
            public string Type = "new expression";
            public ArgumentsExprNode Expr { get; set; }
        }
    }
}
