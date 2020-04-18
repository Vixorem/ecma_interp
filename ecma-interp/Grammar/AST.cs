using System;
using System.Collections.Generic;
using System.Text;
using static ecma_interp.Grammar.Constants;

namespace ecma_interp.Grammar
{
    /*
     The class represents Abstract Syntax Tree for JavaScript grammar.
     It's built within ECMAVisitor class.
    */

    public class AST
    {
        public static Dictionary<Type, string> NodeTypes = new Dictionary<Type, string>
        {
            {typeof(Node), "Node"},
            {typeof(IdentNode), "Identifier"},
            {typeof(InitialiserNode), "Initialiser"},
            {typeof(ExprSequenceNode), "Expression sequence"},
            {typeof(BlockNode), "Block"},
            {typeof(ReturnNode), "Return statement"},
            {typeof(ContinueNode), "Continue statement"},
            {typeof(BreakNode), "Break statement"},
            {typeof(WhileNode), "While"},
            {typeof(StatementListNode), "Statement list"},
            {typeof(ProgramNode), "Program"},
            {typeof(EmptyNode), "Empty statement"},
            {typeof(LiteralNode), "Literal"},
            {typeof(NumericLiteralNode), "Numeric literal"},
            {typeof(NullLiteralNode), "Null literal"},
            {typeof(UndefLiteralNode), "Undefined literal"},
            {typeof(BoolLiteralNode), "Bool literal"},
            {typeof(StringLiteralNode), "String literal"},
            {typeof(VarDeclNode), "Variable declaration"},
            {typeof(VarDeclListNode), "Variable declaration list"},
            {typeof(FunctionExprNode), "Function expression"},
            {typeof(FunctionDeclNode), "Function declaration"},
            {typeof(FormalParamList), "Formal parameters list"},
            {typeof(IfNode), "if statement"},
            {typeof(MemberIndExprNode), "Member expression (index)"},
            {typeof(MemberDotExprNode), "Member expression (dot)"},
            {typeof(CalleeExprNode), "Callee expression"},
            {typeof(NewExprNode), "'new' expression"},
            {typeof(ExprUpdateNode), "Expression update"},
            {typeof(DeleteNode), "Delete statement"},
            {typeof(VoidNode), "Void statement"},
            {typeof(TypeofNode), "Typeof statement"},
            {typeof(InstanceOfNode), "InstanceOf statement"},
            {typeof(UnaryOperNode), "Unary operation"},
            {typeof(BitUnaryOperNode), "Bit unary operation"},
            {typeof(MultiplicNode), "Multiplicative operation"},
            {typeof(AdditiveNode), "Additive operation"},
            {typeof(BitWiseNode), "Bitwise operator"},
            {typeof(RelOperNode), "Relational operator"},
            {typeof(BinaryBitOperNode), "Binary bit operation"},
            {typeof(BinaryLogicOperNode), "Binary logic operation"},
            {typeof(InExprNode), "'in' expression"},
            {typeof(ParenthExprNode), "Parenthesized expression"},
            {typeof(AssignmentExprNode), "Assignment expression"},
            {typeof(ThisExprNode), "'this' statement"},
            {typeof(ArrayLiteralExprNode), "Array literal"},
            {typeof(PropertyExprAssignmentNode), "Property assignment"},
            {typeof(PropertyGetterNode), "Property getter"},
            {typeof(PropertySetterNode), "Property setter"},
            {typeof(ObjectLiteralExprNode), "Object literal"},
            {typeof(EqualityExprNode), "Equality expression"},
            {typeof(KeyWordNode), "Keyword"}
        };

        public class Node
        {
            public int Start { get; set; }
            public int Line { get; set; }
        }
        public class IdentNode : Node
        {
            public string Name { get; set; }
        }

        public class InitialiserNode : Node
        {
            public Node Value { get; set; }
        }

        public class ExprSequenceNode : Node
        {
            public List<Node> Exprs { get; set; }
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

        public class WhileNode : Node
        {
            public ExprSequenceNode Cond { get; set; }
            public Node Body { get; set; }
        }

        public class StatementListNode : Node
        {
            public List<Node> Statements { get; set; }
        }

        public class ProgramNode : Node
        {
            public List<Node> List { get; set; }
        }

        public class EmptyNode : Node
        {
        }

        public abstract class LiteralNode : Node
        {
            public string Value { get; set; }
        }

        public class NumericLiteralNode : LiteralNode
        {
            //TODO: enum for dec, hex, oct???
        }

        public class NullLiteralNode : LiteralNode
        {
        }

        public class UndefLiteralNode : LiteralNode
        {
        }

        public class BoolLiteralNode : LiteralNode
        {
        }

        public class StringLiteralNode : LiteralNode
        {
        }

        public class VarDeclNode : Node
        {
            public string Name { get; set; }
            public InitialiserNode Init { get; set; }
        }

        public class VarDeclListNode : Node
        {
            public List<VarDeclNode> VarDecls { get; set; } = new List<VarDeclNode>();
        }

        public class FunctionExprNode : Node
        {
            public string Name { get; set; }
            public FormalParamList Params { get; set; }
            public ExprSequenceNode FuncBody { get; set; }
        }

        public class FunctionDeclNode : Node
        {
            public string Name { get; set; }
            public FormalParamList Params { get; set; }
            public ExprSequenceNode FuncBody { get; set; }
        }

        public class FormalParamList : Node
        {
            public List<IdentNode> Idents { get; set; }
        }


        public class IfNode : Node
        {
            public ExprSequenceNode Cond { get; set; }
            public Node Statement { get; set; }
            public Node AlterStatement { get; set; }
        }

        public class MemberIndExprNode : Node
        {
            public Node Expr { get; set; }
            public ExprSequenceNode Ind { get; set; }
        }

        public class MemberDotExprNode : Node
        {
            public Node Expr { get; set; }
            public IdentNode Ident { get; set; }
        }

        //Functor is ment to be an expr with parenthesis
        public class CalleeExprNode : Node
        {
            public Node LeftExpr { get; set; }
            public List<Node> Args { get; set; }
        }

        public class NewExprNode : Node
        {
            public Node Expr { get; set; }
        }

        public class ExprUpdateNode : Node
        {
            public ExprUpdate Oper { get; set; }
            public Node Expr { get; set; }
        }

        public class DeleteNode : Node
        {
            public Node Expr { get; set; }
        }

        public class VoidNode : Node
        {
            public Node Expr { get; set; }
        }

        public class TypeofNode : Node
        {
            public Node Expr { get; set; }
        }

        public class InstanceOfNode : Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class UnaryOperNode : Node
        {
            public UnaryOper Oper { get; set; }
            public Node Expr { get; set; }
        }

        public class BitUnaryOperNode : Node
        {
            public BitUnaryOper Oper { get; set; }
            public Node Expr { get; set; }
        }

        public class MultiplicNode : Node
        {

            public MultiplicOper Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class AdditiveNode : Node
        {
            public AdditiveOper Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class BitWiseNode : Node
        {
            public string Sign;
            public BitShift Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class RelOperNode : Node
        {
            public Relation Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class BinaryBitOperNode : Node
        {
            public BitOper Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class BinaryLogicOperNode : Node
        {
            public LogicalOper Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class InExprNode : Node
        {
            public Node Obj { get; set; }
            public Node Cls { get; set; }
        }

        public class ParenthExprNode : Node
        {
            public Node Expr { get; set; }
        }

        public class AssignmentExprNode : Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class ThisExprNode : Node
        {
        }

        public class ArrayLiteralExprNode : Node
        {
            public List<Node> Exprs { get; set; }
        }

        public class PropertyExprAssignmentNode : Node
        {
            public Node PropName { get; set; }
            public Node Expr { get; set; }
        }

        public class PropertyGetterNode : Node
        {
            public IdentNode Name { get; set; }
            public Node FuncBody { get; set; }
        }

        public class PropertySetterNode : Node
        {
            public IdentNode Name { get; set; }
            public Node Param { get; set; }
            public Node FuncBody { get; set; }
        }

        public class ObjectLiteralExprNode : Node
        {
            public List<Node> Exprs { get; set; }
        }

        public class EqualityExprNode : Node
        {
            public Equality Oper { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        public class KeyWordNode : Node
        {
            public KeyWord Kword { get; set; }
        }
    }
}
