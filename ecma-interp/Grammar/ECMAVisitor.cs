using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ecma_interp.Grammar
{
    partial class ECMAVisitor : ECMAScriptBaseVisitor<object>
    {
        public override object VisitProgram([NotNull] ECMAScriptParser.ProgramContext context)
        {
            var program = new AST.ProgramNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                List = new List<AST.Node>(),
            };

            program.List = (List<AST.Node>)Visit(context.sourceElements());

            return program;
        }

        public override object VisitEof([NotNull] ECMAScriptParser.EofContext context)
        {
            return new AST.EmptyNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override object VisitEos([NotNull] ECMAScriptParser.EosContext context)
        {
            return new AST.EmptyNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override object VisitStatementList([NotNull] ECMAScriptParser.StatementListContext context)
        {
            List<AST.Node> list = new List<AST.Node>();

            foreach (var s in context.statement())
            {
                list.Add((AST.Node)Visit(s));
            }

            return list;
        }

        //public override dynamic VisitStatement([NotNull] ECMAScriptParser.StatementContext context)
        //{
        //    return Visit(context.GetChild(0));
        //}

        public override object VisitVariableStatement([NotNull] ECMAScriptParser.VariableStatementContext context)
        {
            var declList = new AST.VarDeclListNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
            };

            declList.VarDecls = (List<AST.VarDeclNode>)Visit(context.variableDeclarationList());

            return declList;
        }

        public override object VisitVariableDeclarationList([NotNull] ECMAScriptParser.VariableDeclarationListContext context)
        {
            List<AST.VarDeclNode> list = new List<AST.VarDeclNode>();

            foreach (var v in context.variableDeclaration())
            {
                list.Add((AST.VarDeclNode)Visit(v));
            }

            return list;
        }

        public override object VisitVariableDeclaration([NotNull] ECMAScriptParser.VariableDeclarationContext context)
        {
            return new AST.VarDeclNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Name = context.Start.Text,
                Init = (context.initialiser() == null)
                    ? (null)
                    : ((AST.InitialiserNode)Visit(context.initialiser()))
            };
        }

        public override object VisitInitialiser([NotNull] ECMAScriptParser.InitialiserContext context)
        {
            return new AST.InitialiserNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Value = (context.singleExpression() == null)
                    ? (null)
                    : ((AST.Node)Visit(context.singleExpression()))
            };
        }

        public override object VisitLiteralExpression([NotNull] ECMAScriptParser.LiteralExpressionContext context)
        {
            if (context.literal().NullLiteral() != null)
            {
                return new AST.NullLiteralNode
                {
                    Start = context.Start.StartIndex,
                    End = context.Stop.StopIndex,
                    Value = "null"
                };
            }

            if (context.literal().BooleanLiteral() != null)
            {
                return new AST.BoolLiteralNode
                {
                    Start = context.Start.StartIndex,
                    End = context.Stop.StopIndex,
                    Value = context.literal().BooleanLiteral().Symbol.Text
                };
            }

            if (context.literal().StringLiteral() != null)
            {
                return new AST.StringLiteralNode
                {
                    Start = context.Start.StartIndex,
                    End = context.Stop.StopIndex,
                    Value = context.literal().StringLiteral().Symbol.Text
                };
            }

            return (AST.NumericLiteralNode)Visit(context.literal().numericLiteral());

        }

        public override object VisitNumericLiteral([NotNull] ECMAScriptParser.NumericLiteralContext context)
        {
            if (context.HexIntegerLiteral() != null)
            {
                return new AST.NumericLiteralNode
                {
                    Start = context.Start.StartIndex,
                    End = context.Stop.StopIndex,
                    Value = context.HexIntegerLiteral().Symbol.Text
                };
            }

            if (context.DecimalLiteral() != null)
            {
                return new AST.NumericLiteralNode
                {
                    Start = context.Start.StartIndex,
                    End = context.Stop.StopIndex,
                    Value = context.DecimalLiteral().Symbol.Text
                };
            }

            return new AST.NumericLiteralNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Value = context.OctalIntegerLiteral().Symbol.Text
            };
        }

        public override object VisitBlock([NotNull] ECMAScriptParser.BlockContext context)
        {
            return new AST.BlockNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Statements = (AST.StatementListNode)Visit(context.statementList())
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

        public override object VisitExpressionStatement([NotNull] ECMAScriptParser.ExpressionStatementContext context)
        {
            return Visit(context.expressionSequence());
        }

        public override object VisitExpressionSequence([NotNull] ECMAScriptParser.ExpressionSequenceContext context)
        {
            AST.ExprSequenceNode exprSeq = new AST.ExprSequenceNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Exprs = new List<AST.Node>()
            };

            foreach (var e in context.singleExpression())
            {
                exprSeq.Exprs.Add((AST.Node)Visit(e));
            }

            return exprSeq;
        }

        public override object VisitIfStatement([NotNull] ECMAScriptParser.IfStatementContext context)
        {
            return new AST.IfNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Cond = (AST.ExprSequenceNode)Visit(context.expressionSequence()),
                Statement = (AST.Node)Visit(context.statement()[0]),
                AlterStatement = (AST.Node)Visit(context.statement()[1])
            };
        }

        public override object VisitWhileStatement([NotNull] ECMAScriptParser.WhileStatementContext context)
        {
            return new AST.WhileNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Body = (AST.Node)Visit(context.expressionSequence()),
                Cond = (AST.ExprSequenceNode)Visit(context.statement())
            };
        }

        public override object VisitReturnStatement([NotNull] ECMAScriptParser.ReturnStatementContext context)
        {
            return new AST.ReturnNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                ExprSeq = ((AST.ExprSequenceNode)Visit(context.expressionSequence()))
            };
        }

        public override object VisitContinueStatement([NotNull] ECMAScriptParser.ContinueStatementContext context)
        {
            if (context.Identifier() != null)
            {
                throw new NotImplementedException(msg);
            }

            return new AST.ContinueNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override object VisitBreakStatement([NotNull] ECMAScriptParser.BreakStatementContext context)
        {
            if (context.Identifier() != null)
            {
                throw new NotImplementedException(msg);
            }

            return new AST.BreakNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override object VisitFunctionExpression([NotNull] ECMAScriptParser.FunctionExpressionContext context)
        {
            return new AST.FunctionExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Name = (context.Identifier() == null)
                    ? ("")
                    : (context.Identifier().Symbol.Text),
                Params = (context.formalParameterList() == null)
                    ? (null)
                       : ((AST.FormalParamList)Visit(context.formalParameterList())),
                FuncBody = (AST.ExprSequenceNode)Visit(context.functionBody())
            };
        }

        public override object VisitFunctionBody([NotNull] ECMAScriptParser.FunctionBodyContext context)
        {
            return new AST.ExprSequenceNode
            {
                Type = "Function body",
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Exprs = (List<AST.Node>)Visit(context.sourceElements())
            };
        }

        public override object VisitIdentifierExpression([NotNull] ECMAScriptParser.IdentifierExpressionContext context)
        {
            return new AST.IdentNode
            {
                Name = context.Identifier().Symbol.Text,
                Start = context.Identifier().Symbol.StartIndex,
                End = context.Identifier().Symbol.StopIndex
            };
        }

        public override object VisitFunctionDeclaration([NotNull] ECMAScriptParser.FunctionDeclarationContext context)
        {
            return new AST.FunctionDeclNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Name = (context.Identifier() == null)
                    ? ("")
                    : (context.Identifier().Symbol.Text),
                Params = (context.formalParameterList() == null)
                    ? (null)
                    : ((AST.FormalParamList)Visit(context.formalParameterList())),
                FuncBody = (AST.ExprSequenceNode)Visit(context.functionBody())
            };
        }

        public override object VisitSourceElements([NotNull] ECMAScriptParser.SourceElementsContext context)
        {
            List<AST.Node> list = new List<AST.Node>();

            foreach (var s in context.GetRuleContexts<ParserRuleContext>())
            {
                list.Add((AST.Node)Visit(s));
            }

            return list;
        }

        public override object VisitFormalParameterList([NotNull] ECMAScriptParser.FormalParameterListContext context)
        {
            AST.FormalParamList list = new AST.FormalParamList()
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Idents = new List<AST.IdentNode>()
            };

            foreach (var ident in context.Identifier())
            {
                list.Idents.Add(new AST.IdentNode
                {
                    Name = ident.Symbol.Text,
                    Start = ident.Symbol.StartIndex,
                    End = ident.Symbol.StopIndex
                });
            }

            return list;
        }

        public override object VisitMemberIndexExpression([NotNull] ECMAScriptParser.MemberIndexExpressionContext context)
        {
            return new AST.MemberIndExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.singleExpression()),
                Ind = (AST.ExprSequenceNode)Visit(context.expressionSequence()),
            };
        }

        public override object VisitArgumentsExpression([NotNull] ECMAScriptParser.ArgumentsExpressionContext context)
        {
            return new AST.ArgumentsExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                LeftExpr = (AST.Node)Visit(context.singleExpression()),
                Args = (List<AST.Node>)Visit(context.arguments())
            };
        }

        public override object VisitArgumentList([NotNull] ECMAScriptParser.ArgumentListContext context)
        {
            List<AST.Node> list = new List<AST.Node>();

            foreach (var e in context.singleExpression())
            {
                list.Add((AST.Node)Visit(e));
            }

            return list;
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
