using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static ecma_interp.Grammar.Constants;

namespace ecma_interp.Grammar
{
    public class ASTRepresenter
    {
        public string FilePath { get; private set; } = "";
        public bool NoProg { get; set; } = false;
        public bool KeepInRAM { get; set; } = false;
        public string TextedAST { get; set; }
        private int shift = 0;
        private int step = 4;
        private bool append = false;
        private bool outputChecked = false;

        public ASTRepresenter(string path = "")
        {
            FilePath = path;
        }

        private void PrintRootInfo(dynamic node, string altName = null)
        {
            PrintLn($"+{altName ?? AST.NodeTypes[node.GetType()]}");
            shift += step;
            PrintLn($"Start: {node.Start}");
            PrintLn($"Line: {node.Line}");
        }

        private void PrintProperty(string prop, string val)
        {
            PrintLn($"{prop}: {val}");
        }

        private void PrintNamedNode(string prop, dynamic node)
        {
            PrintLn(prop + ":");
            shift += step;
            VisitNode(node);
            shift -= step;
        }
        private void PrintNamedNodeList(string prop, List<AST.Node> list)
        {
            PrintLn(prop + ":");
            shift += step;
            foreach (var t in list)
            {
                VisitNode((dynamic)t);
            }
            shift -= step;
        }


        private void PrintLn(string s)
        {
            var line = String.Concat(Enumerable.Repeat(" ", shift)) + s;
            if (FilePath == "")
            {
                Console.WriteLine(line);
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(FilePath, append, Encoding.Default))
                {
                    append = true;
                    sw.WriteLine(line);
                }
            }

            if (KeepInRAM == true)
            {
                TextedAST += line + Environment.NewLine;
            }
        }

        public void VisitNode(AST.Node root)
        {
            if (root == null)
            {
                return;
            }
            VisitNode((dynamic)root);
            shift -= step;
        }

        public void VisitNode(AST.IdentNode root)
        {
            if (root == null)
            {
                return;
            }

            PrintRootInfo(root);
            PrintProperty("Name", root.Name);
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
                PrintRootInfo(root);
            }
            if (root.List != null)
            {
                foreach (var t in root.List)
                {
                    VisitNode((dynamic)t);
                }
            }
            shift -= step;
        }

        public void VisitNode(AST.VarDeclListNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
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
            PrintRootInfo(root);
            PrintProperty("Name", root.Name);
            VisitNode((AST.InitialiserNode)root.Init);
            shift -= step;
        }

        public void VisitNode(AST.InitialiserNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            VisitNode((dynamic)root.Value);
            shift -= step;
        }

        public void VisitNode(AST.NumericLiteralNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Value", root.Value);
            shift -= step;
        }

        public void VisitNode(AST.NullLiteralNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Value", root.Value);
            shift -= step;
        }

        public void VisitNode(AST.UndefLiteralNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Value", root.Value);
            shift -= step;
        }

        public void VisitNode(AST.BoolLiteralNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Value", root.Value);
            shift -= step;
        }

        public void VisitNode(AST.StringLiteralNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Value", root.Value);
            shift -= step;
        }


        public void VisitNode(AST.ReturnNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            VisitNode(root.ExprSeq);
            shift -= step;
        }

        public void VisitNode(AST.ExprSequenceNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
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
            PrintRootInfo(root);
            VisitNode(root.Statements);
            shift -= step;
        }

        public void VisitNode(AST.ContinueNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            shift -= step;
        }
        public void VisitNode(AST.BreakNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            shift -= step;
        }
        public void VisitNode(AST.WhileNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
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
            PrintRootInfo(root);
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
            PrintRootInfo(root);
            shift -= step;
        }

        public void VisitNode(AST.FunctionExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Name", root.Name);
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
            PrintRootInfo(root);
            PrintProperty("Name", root.Name);
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
            PrintRootInfo(root);
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
            PrintNamedNode("Condition", root.Cond);
            VisitNode((dynamic)root.Statement);
            if (root.AlterStatement != null)
            {
                PrintNamedNode("Alternative", root.AlterStatement);
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
            PrintRootInfo(root);
            PrintNamedNode("Object", root.Expr);
            PrintNamedNode("Index", root.Ind);
            shift -= step;
        }

        public void VisitNode(AST.MemberDotExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintNamedNode("Object", root.Expr);
            PrintNamedNode("Property", root.Ident);
            shift -= step;
        }

        public void VisitNode(AST.NewExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.DeleteNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.VoidNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.TypeofNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.UnaryOperNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Oper", root.Oper.ToString());
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.InstanceOfNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintNamedNode("Left", root.Left);
            PrintNamedNode("Right", root.Right);
            shift -= step;
        }

        public void VisitNode(AST.ExprUpdateNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Oper", root.Oper.ToString());
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.AdditiveNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Oper", root.Oper.ToString());
            PrintNamedNode("Left", root.Left);
            PrintNamedNode("Right", root.Right);
            shift -= step;
        }

        public void VisitNode(AST.MultiplicNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Oper", root.Oper.ToString());
            PrintNamedNode("Left", root.Left);
            PrintNamedNode("Right", root.Right);
            shift -= step;
        }

        public void VisitNode(AST.BitWiseNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Oper", root.Oper.ToString());
            PrintNamedNode("Left", root.Left);
            PrintNamedNode("Right", root.Right);
            shift -= step;
        }

        public void VisitNode(AST.BitUnaryOperNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Oper", root.Oper.ToString());
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.CalleeExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            VisitNode((dynamic)root.LeftExpr);
            if (root.Args != null)
            {
                PrintNamedNodeList("Args", root.Args);
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
            PrintRootInfo(root);
            PrintProperty("Oper", root.Oper.ToString());
            PrintNamedNode("Left", root.Left);
            PrintNamedNode("Right", root.Right);
            shift -= step;
        }

        public void VisitNode(AST.BinaryBitOperNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintNamedNode("Left", root.Left);
            PrintNamedNode("Right", root.Right);
            shift -= step;
        }

        public void VisitNode(AST.BinaryLogicOperNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Oper", root.Oper.ToString());
            PrintNamedNode("Left", root.Left);
            PrintNamedNode("Right", root.Right);
            shift -= step;
        }

        public void VisitNode(AST.InExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintNamedNode("Left", root.Cls);
            PrintNamedNode("Right", root.Obj);
            shift -= step;
        }

        public void VisitNode(AST.ParenthExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            VisitNode((dynamic)root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.AssignmentExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintNamedNode("Left", root.Left);
            PrintNamedNode("Right", root.Right);
            shift -= step;
        }

        public void VisitNode(AST.ObjectLiteralExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);

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
            PrintRootInfo(root);
            PrintNamedNode("Property name", root.PropName);
            PrintNamedNode("Expression", root.Expr);
            shift -= step;
        }

        public void VisitNode(AST.PropertyGetterNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            VisitNode((dynamic)root.Name);
            VisitNode((dynamic)root.FuncBody);
            shift -= step;
        }

        public void VisitNode(AST.PropertySetterNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            VisitNode((dynamic)root.Name);
            VisitNode((dynamic)root.Param);
            VisitNode((dynamic)root.FuncBody);
        }

        public void VisitNode(AST.ThisExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);

            shift -= step;
        }

        public void VisitNode(AST.ArrayLiteralExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);

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
            PrintRootInfo(root);
            PrintProperty("Keyword", root.Kword.ToString());
            shift -= step;
        }

        public void VisitNode(AST.EqualityExprNode root)
        {
            if (root == null)
            {
                return;
            }
            PrintRootInfo(root);
            PrintProperty("Oper", root.Oper.ToString());
            PrintNamedNode("Left", root.Left);
            PrintNamedNode("Right", root.Right);
            shift -= step;
        }
    }
}
