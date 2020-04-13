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
            PrintLn($"Name: {root.Name}");
            PrintLn($"Start: {root.Start}");
            PrintLn($"End: {root.End}");

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
    }
}
