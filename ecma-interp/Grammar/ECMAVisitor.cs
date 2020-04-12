using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ecma_interp.Grammar
{
    partial class ECMAVisitor : ECMAScriptBaseVisitor<object>
    {
        public override dynamic VisitProgram([NotNull] ECMAScriptParser.ProgramContext context)
        {
            var program = new AST.ProgramNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };

            program.Statement = Visit(context.GetChild(0));

            return program;
        }

        public override dynamic VisitEof([NotNull] ECMAScriptParser.EofContext context)
        {
            return new AST.EmptyNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override dynamic VisitEos([NotNull] ECMAScriptParser.EosContext context)
        {
            return new AST.EmptyNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override dynamic VisitStatementList([NotNull] ECMAScriptParser.StatementListContext context)
        {
            List<AST.StatementListNode> list = new List<AST.StatementListNode>();

            for (int i = 0; i < context.ChildCount; i += 2)
            {
                list.Add((dynamic)Visit(context.children[i]));
            }

            return list;
        }

        public override dynamic VisitStatement([NotNull] ECMAScriptParser.StatementContext context)
        {
            return Visit(context.GetChild(0));
        }

        public override dynamic VisitVariableStatement([NotNull] ECMAScriptParser.VariableStatementContext context)
        {
            var declList = new AST.VarDeclListNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
            };

            declList.VarDecls = (dynamic)Visit(context.children[1]);

            return declList;
        }

        public override dynamic VisitVariableDeclarationList([NotNull] ECMAScriptParser.VariableDeclarationListContext context)
        {
            List<AST.VarDeclNode> list = new List<AST.VarDeclNode>();

            for (int i = 0; i < context.ChildCount; i += 2)
            {
                list.Add((dynamic)Visit(context.children[i]));
            }

            return list;
        }

        public override dynamic VisitVariableDeclaration([NotNull] ECMAScriptParser.VariableDeclarationContext context)
        {
            return new AST.VarDeclNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Name = context.Start.Text,
                Value = context.Stop.Text
            };
        }

        public override dynamic VisitBlock([NotNull] ECMAScriptParser.BlockContext context)
        {
            AST.StatementListNode list = null;
            if (context.ChildCount != 2)
            {
                list = (AST.StatementListNode)Visit(context.GetChild(0));
            }
            return new AST.BlockNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Statements = list
            };
        }

        public override dynamic VisitEmptyStatement([NotNull] ECMAScriptParser.EmptyStatementContext context)
        {
            return new AST.EmptyNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
            };
        }

        public override dynamic VisitExpressionStatement([NotNull] ECMAScriptParser.ExpressionStatementContext context)
        {
            return Visit(context.GetChild(0));
        }

        public override dynamic VisitExpressionSequence([NotNull] ECMAScriptParser.ExpressionSequenceContext context)
        {
            AST.ExprSequenceNode exprSeq = new AST.ExprSequenceNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Exprs = new List<AST.SingleExprNode>()
            };

            for (int i = 0; i < context.ChildCount; i += 2)
            {
                exprSeq.Exprs.Add((AST.SingleExprNode)Visit(context.children[i]));
            }

            return exprSeq;
        }

        public override dynamic VisitIfStatement([NotNull] ECMAScriptParser.IfStatementContext context)
        {
            return new AST.IfNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                ExprSeq = (AST.ExprSequenceNode)Visit(context.children[2]),
                Statement = Visit(context.children[4]),
                ElseStatement = (context.ChildCount == 6)
                    ? (Visit(context.children[6]))
                    : (null)
            };
        }

        public override dynamic VisitIterationStatement([NotNull] ECMAScriptParser.IterationStatementContext context)
        {
            return new AST.WhileNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Statement = Visit(context.children[1]),
                ExprSeq = (AST.ExprSequenceNode)Visit(context.children[4])
            };
        }

        //public override object VisitSingleExpression([NotNull] ECMAScriptParser.SingleExpressionContext context)
        //{
        //    return base.VisitSingleExpression(context);
        //}

        public override dynamic VisitReturnStatement([NotNull] ECMAScriptParser.ReturnStatementContext context)
        {
            return new AST.ReturnNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                ExprSeq = (context.ChildCount == 2)
                    ? (null)
                    : ((AST.ExprSequenceNode)Visit(context.children[1]))
            };
        }

        public override dynamic VisitContinueStatement([NotNull] ECMAScriptParser.ContinueStatementContext context)
        {
            if (context.ChildCount == 3)
            {
                throw new NotImplementedException(msg);
            }

            return new AST.ContinueNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override dynamic VisitBreakStatement([NotNull] ECMAScriptParser.BreakStatementContext context)
        {
            if (context.ChildCount == 3)
            {
                throw new NotImplementedException(msg);
            }

            return new AST.BreakNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override dynamic VisitFunctionExpression([NotNull] ECMAScriptParser.FunctionExpressionContext context)
        {
            //TODO: complete
            return null;
        }
    }

    // For unimplemented rules
    partial class ECMAVisitor : ECMAScriptBaseVisitor<object>
    {
        private string msg = "use unimplemented part of grammar";
        public override object VisitWithStatement(ECMAScriptParser.WithStatementContext context)
        {
            throw new NotImplementedException(msg);
        }

        public override object VisitLiteral(ECMAScriptParser.LiteralContext context)
        {
            if (context.RegularExpressionLiteral() != null)
            {
                throw new NotImplementedException(msg);
            }
            return base.VisitLiteral(context);
        }

        public override object VisitLabelledStatement(ECMAScriptParser.LabelledStatementContext context)
        {
            throw new NotImplementedException(msg);
        }

        public override object VisitTryStatement(ECMAScriptParser.TryStatementContext context)
        {
            throw new NotImplementedException(msg);
        }

        public override object VisitThrowStatement(ECMAScriptParser.ThrowStatementContext context)
        {
            throw new NotImplementedException(msg);
        }

        public override object VisitDebuggerStatement(ECMAScriptParser.DebuggerStatementContext context)
        {
            throw new NotImplementedException(msg);
        }

        public override object VisitSwitchStatement(ECMAScriptParser.SwitchStatementContext context)
        {
            throw new NotImplementedException(msg);
        }
    }
}
