using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecma_interp.Grammar
{
    class ASTRepresenter
    {
        private int shift = 0;
        private int step = 4;

        private void PrintLn(string s)
        {
            Console.WriteLine(String.Concat(Enumerable.Repeat(" ", shift)) + s);
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
            PrintLn($"+{root.Type}");
            shift += step;
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");
            foreach (var t in root.List)
            {
                VisitNode((dynamic)t);
            }
            shift -= step;
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
            foreach (var t in root.VarDecls)
            {
                VisitNode((dynamic)t);
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
            VisitNode(root.Init);
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
            foreach (var t in root.Exprs)
            {
                VisitNode((dynamic)t);
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
            foreach (var t in root.Statements)
            {
                VisitNode((dynamic)t);
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
            foreach (var t in root.Idents)
            {
                VisitNode((dynamic)t);
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
            VisitNode(root.Statement);
            VisitNode(root.AlterStatement);
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
            VisitNode(root.Expr);
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
            // TODO ??????????????????????????????????????????
            shift -= step;
        }
    }
}
