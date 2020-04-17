using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ecma_interp.Grammar
{
    class ASTRepresenter
    {
        public string FilePath { get; set; } = "";
        public bool NoProg { get; set; } = false;
        private int shift = 0;
        private int step = 4;
        private bool append = false;

        private void PrintLn(string s)
        {
            if (FilePath == "")
            {
                Console.WriteLine(String.Concat(Enumerable.Repeat(" ", shift)) + s);
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(FilePath, append, System.Text.Encoding.Default))
                {
                    append = true;
                    sw.WriteLine(String.Concat(Enumerable.Repeat(" ", shift)) + s);
                }
            }

        }
        public void VisitNode(AST.Node root)
        {
            if (root == null)
            {
                return;
            }
            VisitNode((dynamic)root);
        }

        public void VisitNode(AST.IdentNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn($"Name: {root.Name}");
            shift -= step;
        }

        public void VisitNode(AST.ProgramNode root)
        {
            if (root == null)
            {
                return;
            }
            if (NoProg == false)
            {
                PrintLn($"+{root.Type}");
                shift += step;
                PrintLn($"Start: {root.Start}");
                PrintLn($"End: {root.End}");
            }
            if (root.List != null)
            {
                foreach (var t in root.List)
                {
                    VisitNode((dynamic)t);
                }
            }
            if (NoProg == false)
            {
                shift -= step;
            }
        }

        public void VisitNode(AST.VarDeclListNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            if (root.VarDecls != null)
            {
                foreach (var t in root.VarDecls)
                {
                    VisitNode((dynamic)t);
                }
            }
            shift -= step;
        }

        public void VisitNode(AST.VarDeclNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn($"Name: {root.Name}");
            VisitNode((AST.InitialiserNode)root.Init);
            shift -= step;
        }

        public void VisitNode(AST.InitialiserNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Value);
            shift -= step;
        }

        public void VisitNode(AST.NumericLiteralNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{(dynamic)root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn($"Value: {root.Value}");
            shift -= step;
        }

        public void VisitNode(AST.NullLiteralNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{(dynamic)root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn($"Value: {root.Value}");
            shift -= step;
        }

        public void VisitNode(AST.UndefLiteralNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{(dynamic)root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn($"Value: {root.Value}");
            shift -= step;
        }

        public void VisitNode(AST.BoolLiteralNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{(dynamic)root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn($"Value: {root.Value}");
            shift -= step;
        }

        public void VisitNode(AST.StringLiteralNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{(dynamic)root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn($"Value: {root.Value}");
            shift -= step;
        }


        public void VisitNode(AST.ReturnNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode(root.ExprSeq);
            shift -= step;
        }

        public void VisitNode(AST.ExprSequenceNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            if (root.Exprs != null)
            {
                foreach (var t in root.Exprs)
                {
                    VisitNode((dynamic)t);
                }
            }
            shift -= step;
        }

        public void VisitNode(AST.BlockNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode(root.Statements);
            shift -= step;
        }

        public void VisitNode(AST.ContinueNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            shift -= step;
        }
        public void VisitNode(AST.BreakNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            shift -= step;
        }
        public void VisitNode(AST.WhileNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode(root.Cond);
            VisitNode(root.Body);
            shift -= step;
        }

        public void VisitNode(AST.StatementListNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            if (root.Statements != null)
            {
                foreach (var t in root.Statements)
                {
                    VisitNode((dynamic)t);
                }
            }
            shift -= step;
        }

        public void VisitNode(AST.EmptyNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            shift -= step;
        }

        public void VisitNode(AST.FunctionExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn($"Name: {root.Name}");
            VisitNode(root.Params);
            VisitNode(root.FuncBody);
            shift -= step;

        }

        public void VisitNode(AST.FunctionDeclNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn($"Name: {root.Name}");
            VisitNode(root.Params);
            VisitNode(root.FuncBody);
            shift -= step;

        }

        public void VisitNode(AST.FormalParamList root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            if (root.Idents != null)
            {
                foreach (var t in root.Idents)
                {
                    VisitNode((dynamic)t);
                }
            }
            shift -= step;
        }

        public void VisitNode(AST.IfNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            root.Cond.Type = "Condition";
            VisitNode(root.Cond);
            VisitNode((dynamic)root.Statement);
            if (root.AlterStatement != null)
            {
                PrintLn($"Alternative");
                shift += step;
                VisitNode((dynamic)root.AlterStatement);
                shift -= step;
            }
            shift -= step;
        }

        public void VisitNode(AST.MemberIndExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            root.Ind.Type = "Index expression";
            VisitNode((dynamic)root.Expr);
            VisitNode(root.Ind);
            shift -= step;
        }

        public void VisitNode(AST.MemberDotExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            root.Expr.Type = "Object";
            root.Ident.Type = "Property";
            VisitNode((dynamic)root.Expr);
            VisitNode(root.Ident);
            shift -= step;
        }

        public void VisitNode(AST.NewExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.DeleteNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.VoidNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.TypeofNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.UnaryPlusNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{(dynamic)root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.UnaryMinusNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{(dynamic)root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.UnaryNotNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{(dynamic)root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.InstanceOfNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("Left:");
            shift += step;
            VisitNode((dynamic)root.Left);
            shift -= step;
            PrintLn("Right:");
            shift += step;
            VisitNode((dynamic)root.Right);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.PostIncExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.AdditiveNode root)
        {
            if (root == null)
            {
                return;
            }
            var sign = (root.Oper == AST.AdditiveNode.OperType.Add)
                    ? ("+")
                    : ("-");
            PrintLn($"+{root.Type} ({sign})");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("Left:");
            shift += step;
            VisitNode((dynamic)root.Left);
            shift -= step;
            PrintLn("Right:");
            shift += step;
            VisitNode((dynamic)root.Right);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.MultiplicNode root)
        {
            if (root == null)
            {
                return;
            }
            string sign;
            if (root.Oper == AST.MultiplicNode.OperType.Div)
            {
                sign = "/";
            }
            else if (root.Oper == AST.MultiplicNode.OperType.Mul)
            {
                sign = "*";
            }
            else
            {
                sign = "%";
            }
            PrintLn($"+{root.Type} ({sign})");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("Left:");
            shift += step;
            VisitNode((dynamic)root.Left);
            shift -= step;
            PrintLn("Right:");
            shift += step;
            VisitNode((dynamic)root.Right);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.BitWiseNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type} ({root.Sign})");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("Left:");
            shift += step;
            VisitNode((dynamic)root.Left);
            shift -= step;
            PrintLn("Right:");
            shift += step;
            VisitNode((dynamic)root.Right);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.UnaryBitNotNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{(dynamic)root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.PostDecExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.PrefIncExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.PrefDecExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.CalleeExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.LeftExpr);
            if (root.Args != null)
            {
                PrintLn("Args");
                shift += step;
                foreach (var t in root.Args)
                {
                    VisitNode((dynamic)t);
                }
                shift -= step;
            }
            shift -= step;
        }

        public void VisitNode(AST.RelOperNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type} ({root.Sign})");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("Left:");
            shift += step;
            VisitNode((dynamic)root.Left);
            shift -= step;
            PrintLn("Right:");
            shift += step;
            VisitNode((dynamic)root.Right);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.BinaryBitOperNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type} ({root.Sign})");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("Left:");
            shift += step;
            VisitNode((dynamic)root.Left);
            shift -= step;
            PrintLn("Right:");
            shift += step;
            VisitNode((dynamic)root.Right);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.BinaryLogicOperNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type} ({root.Sign})");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("Left:");
            shift += step;
            VisitNode((dynamic)root.Left);
            shift -= step;
            PrintLn("Right:");
            shift += step;
            VisitNode((dynamic)root.Right);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.InExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("Left:");
            shift += step;
            VisitNode((dynamic)root.Obj);
            shift -= step;
            PrintLn("Right:");
            shift += step;
            VisitNode((dynamic)root.Cls);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.ParenthExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.AssignmentExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("Left:");
            shift += step;
            VisitNode((dynamic)root.Left);
            shift -= step;
            PrintLn("Right:");
            shift += step;
            VisitNode((dynamic)root.Right);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.ObjectLiteralExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            if (root.Exprs != null)
            {
                foreach (var t in root.Exprs)
                {
                    VisitNode((dynamic)t);
                }
            }
            shift -= step;
        }

        public void VisitNode(AST.PropertyExprAssignmentNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("PropertyName");
            shift += step;
            VisitNode((dynamic)root.PropName);
            shift -= step;
            PrintLn("Expression");
            shift += step;
            VisitNode((dynamic)root.Expr);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.PropertyGetterNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            shift += step;
            VisitNode((dynamic)root.Name);
            shift -= step;
            shift += step;
            VisitNode((dynamic)root.FuncBody);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.PropertySetterNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            shift += step;
            VisitNode((dynamic)root.Name);
            shift -= step;
            shift += step;
            VisitNode((dynamic)root.Param);
            shift -= step;
            shift += step;
            VisitNode((dynamic)root.FuncBody);
            shift -= step;
            shift -= step;
        }

        public void VisitNode(AST.ThisExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            shift -= step;
        }

        public void VisitNode(AST.ArrayLiteralExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            if (root.Exprs != null)
            {
                foreach (var a in root.Exprs)
                {
                    VisitNode(a);
                }
            }
            shift -= step;
        }

        public void VisitNode(AST.KeyWordNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn($"Keyword: {root.Kword}");
            shift -= step;
        }

        public void VisitNode(AST.EqualityExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintLn($"+{root.Type} ({root.Sign})");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            PrintLn("Left:");
            shift += step;
            VisitNode((dynamic)root.Left);
            shift -= step;
            PrintLn("Right:");
            shift += step;
            VisitNode((dynamic)root.Left);
            shift -= step;
            shift -= step;
        }
    }
}
