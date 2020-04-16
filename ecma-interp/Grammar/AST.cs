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
            public string Type = "Node";
            public int Start { get; set; }
            public int End { get; set; }
        }
        public class IdentNode : Node
        {
            public new string Type = "Identifier";
            public string Name { get; set; }
        }

        public class InitialiserNode : Node
        {
            public new string Type = "Initialiser";
            public Node Value { get; set; }
        }

        public class ExprSequenceNode : Node
        {
            public new string Type = "Expression sequence";
            public List<Node> Exprs { get; set; }
        }

        public class BlockNode : Node
        {
            public new string Type = "Block";
            public StatementListNode Statements { get; set; }
        }

        public class ReturnNode : Node
        {
            public new string Type = "Return statement";
            public ExprSequenceNode ExprSeq { get; set; }
        }

        public class ContinueNode : Node
        {
            public new string Type = "Continue statement";
        }

        public class BreakNode : Node
        {
            public new string Type = "Break statement";
        }

        public class WhileNode : Node
        {
            public new string Type = "While";
            public ExprSequenceNode Cond { get; set; }
            public Node Body { get; set; }
        }

        public class StatementListNode : Node
        {
            public new string Type = "Statement list";
            public List<Node> Statements { get; set; }
        }

        public class ProgramNode : Node
        {
            public new string Type = "Program";
            public List<Node> List { get; set; }
        }

        public class EmptyNode : Node
        {
            public new string Type = "Empty statement";
        }

        public abstract class LiteralNode : Node
        {
            public new string Type = "Literal";
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
            public new string Type = "Variable declaration";
            public string Name { get; set; }
            public InitialiserNode Init { get; set; }
        }

        public class VarDeclListNode : Node
        {
            public new string Type = "Variable declaration list";
            public List<VarDeclNode> VarDecls { get; set; } = new List<VarDeclNode>();
        }

        public class FunctionExprNode : Node
        {
            public new string Type = "Function expression";
            public string Name { get; set; }
            public FormalParamList Params { get; set; }
            public ExprSequenceNode FuncBody { get; set; }
        }

        public class FunctionDeclNode : Node
        {
            public new string Type = "Function declaration";
            public string Name { get; set; }
            public FormalParamList Params { get; set; }
            public ExprSequenceNode FuncBody { get; set; }
        }

        public class FormalParamList : Node
        {
            public new string Type = "Formal parameters list";
            public List<IdentNode> Idents { get; set; }
        }


        public class IfNode : Node
        {
            public new string Type = "if statement";
            public ExprSequenceNode Cond { get; set; }
            public Node Statement { get; set; }
            public Node AlterStatement { get; set; }
        }

        public class MemberIndExprNode : Node
        {
            public new string Type = "Member expression (index)";
            public Node Expr { get; set; }
            public ExprSequenceNode Ind { get; set; }
        }

        public class MemberDotExprNode : Node
        {
            public new string Type = "Member expression (dot)";
            public Node Expr { get; set; }
            public IdentNode Ident { get; set; }
        }

        //Functor is ment to be an expr with parenthesis
        public class FunctorExprNode : Node
        {
            public new string Type = "Functor expression";
            public Node LeftExpr { get; set; }
            public List<Node> Args { get; set; }
        }

        public class NewExprNode : Node
        {
            public new string Type = "'new' expression";
            public Node Expr { get; set; }
        }

        public class PostIncExprNode : Node
        {
            public new string Type = "Postfix increment";
            public Node Expr { get; set; }
        }

        public class PostDecExprNode : Node
        {
            public new string Type = "Postfix decrement";
            public Node Expr { get; set; }
        }

        public class PrefIncExprNode : Node
        {
            public string Type = "Prefix increment";
            public Node Expr { get; set; }
        }

        public class PrefDecExprNode : Node
        {
            public new string Type = "Prefix decrement";
            public Node Expr { get; set; }
        }

        public class DeleteNode : Node
        {
            public new string Type = "Delete statement";
            public Node Expr { get; set; }
        }

        public class VoidNode : Node
        {
            public new string Type = "Void statement";
            public Node Expr { get; set; }
        }

        public class TypeofNode : Node
        {
            public new string Type = "Typeof statement";
            public Node Expr { get; set; }
        }

        public class InstanceOfNode : Node
        {
            public new string Type = "InstanceOf statement";
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class UnaryOperNode : Node
        {
            public new string Type = "Unary operation";
            public Node Expr { get; set; }
        }

        public class UnaryPlusNode : UnaryOperNode
        {
            public new string Type = "Unary plus";
        }

        public class UnaryMinusNode : UnaryOperNode
        {
            public new string Type = "Unary minus";
        }

        public class UnaryNotNode : UnaryOperNode
        {
            public new string Type = "Negation";
        }

        public class UnaryBitNotNode : UnaryOperNode
        {
            public new string Type = "Bit negation";
        }

        public class MultiplicNode : Node
        {
            public enum OperType
            {
                Mul,
                Div,
                Mod
            }
            public new string Type = "Multiplicative operation";
            public OperType Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class AdditiveNode : Node
        {
            public enum OperType
            {
                Add,
                Sub
            }
            public new string Type = "Additive operation";
            public OperType Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class BitWiseNode : Node
        {
            public enum WiseType
            {
                ZeroLeft,
                SignedRight,
                ZeroRight
            }
            public new string Type = "Bitwise operator";
            public string Sign;
            public WiseType Wise { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class RelOperNode : Node
        {
            public enum RelType
            {
                Less,
                Greater,
                LessEq,
                GreaterEq
            }
            public RelType Rel { get; set; }
            public new string Type = "Relational operator";
            public string Sign { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class BinaryBitOperNode : Node
        {
            public enum OperType
            {
                BitAnd,
                BitOr,
                BitXor
            }
            public new string Type = "Binary bit operation";
            public string Sign { get; set; }
            public OperType Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class BinaryLogicOperNode : Node
        {
            public enum OperType
            {
                And,
                Or
            }
            public new string Type = "Binary logic operation";
            public string Sign { get; set; }
            public OperType Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class InExprNode : Node
        {
            public new string Type = "'in' expression";
            public Node Obj { get; set; }
            public Node Cls { get; set; }
        }

        public class ParenthExprNode : Node
        {
            public new string Type = "Parenthesized expression";
            public Node Expr { get; set; }
        }

        public class AssignmentExprNode : Node
        {
            public new string Type = "Assignment expression";
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class ThisExprNode : Node
        {
            public new string Type = "'this' statement";
        }


        public class ArrayLiteralExprNode : Node
        {
            public new string Type = "Array literal";
            public List<Node> Exprs { get; set; }
        }

        public class PropertyExprAssignmentNode : Node
        {
            public new string Type = "Property assignment";
            public Node PropName { get; set; }
            public Node Expr { get; set; }
        }

        public class PropertyGetterNode : Node
        {
            public new string Type = "Property getter";
            public Node Getter { get; set; }
            public Node FuncBody { get; set; }
        }

        public class PropertySetterNode : Node
        {
            public new string Type = "Property setter";
            public Node Param { get; set; }
            public Node FuncBody { get; set; }
        }

        public class ObjectLiteralExprNode : Node
        {
            public new string Type = "Object literal";
            public List<Node> Exprs { get; set; }
        }

        public class EqualityExprNode : Node
        {
            public new string Type = "Equality expression";
            public enum OperType
            {
                Eq,
                StrictEq,
                NotEq,
                NotStrictEq
            }
            public OperType Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public string Sign { get; set; }
        }

        public class KeyWordNode : Node
        {
            public new string Type = "Keyword";
            public string Kword { get; set; }
        }
    }
}
