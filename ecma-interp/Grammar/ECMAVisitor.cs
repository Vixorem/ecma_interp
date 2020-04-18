using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Text;
using static ecma_interp.Grammar.Constants;

namespace ecma_interp.Grammar
{
    partial class ECMAVisitor : ECMAScriptBaseVisitor<object>
    {
        public override object VisitProgram([NotNull] ECMAScriptParser.ProgramContext context)
        {
            var program = new AST.ProgramNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
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
                Line = context.Start.Line
            };
        }

        public override object VisitDeleteExpression([NotNull] ECMAScriptParser.DeleteExpressionContext context)
        {
            return new AST.DeleteNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitVoidExpression([NotNull] ECMAScriptParser.VoidExpressionContext context)
        {
            return new AST.VoidNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitUnaryMinusExpression([NotNull] ECMAScriptParser.UnaryMinusExpressionContext context)
        {
            return new AST.UnaryOperNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Oper = UnaryOper.Minus,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitUnaryPlusExpression([NotNull] ECMAScriptParser.UnaryPlusExpressionContext context)
        {
            return new AST.UnaryOperNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Oper = UnaryOper.Plus,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitNotExpression([NotNull] ECMAScriptParser.NotExpressionContext context)
        {
            return new AST.UnaryOperNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Oper = UnaryOper.Neg,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitBitNotExpression([NotNull] ECMAScriptParser.BitNotExpressionContext context)
        {
            return new AST.BitUnaryOperNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Oper = BitUnaryOper.Neg,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitAdditiveExpression([NotNull] ECMAScriptParser.AdditiveExpressionContext context)
        {
            return new AST.AdditiveNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = AdditiveOperMap[context.additOpers().Start.Text]
            };
        }

        public override object VisitMultiplicativeExpression([NotNull] ECMAScriptParser.MultiplicativeExpressionContext context)
        {
            return new AST.MultiplicNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = MultiplicOperMap[context.multOpers().Start.Text]
            };
        }

        public override object VisitBitShiftExpression([NotNull] ECMAScriptParser.BitShiftExpressionContext context)
        {
            return new AST.BitWiseNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Sign = context.bitWiseSigns().Start.Text,
                Oper = BitShiftMap[context.bitWiseSigns().Start.Text],
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1])
            };
        }

        public override object VisitEos([NotNull] ECMAScriptParser.EosContext context)
        {
            return new AST.EmptyNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line
            };
        }

        public override object VisitStatementList([NotNull] ECMAScriptParser.StatementListContext context)
        {
            List<AST.Node> list = new List<AST.Node>();

            foreach (var s in context.statement())
            {
                list.Add((AST.Node)Visit(s));
            }

            return new AST.StatementListNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Statements = list
            };
        }

        public override object VisitVariableStatement([NotNull] ECMAScriptParser.VariableStatementContext context)
        {
            var declList = new AST.VarDeclListNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
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
                Line = context.Start.Line,
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
                Line = context.Start.Line,
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
                    Line = context.Start.Line,
                    Value = "null"
                };
            }

            if (context.literal().BooleanLiteral() != null)
            {
                return new AST.BoolLiteralNode
                {
                    Start = context.Start.StartIndex,
                    Line = context.Start.Line,
                    Value = context.literal().BooleanLiteral().Symbol.Text
                };
            }

            if (context.literal().StringLiteral() != null)
            {
                return new AST.StringLiteralNode
                {
                    Start = context.Start.StartIndex,
                    Line = context.Start.Line,
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
                    Line = context.Start.Line,
                    Value = context.HexIntegerLiteral().Symbol.Text
                };
            }

            if (context.DecimalLiteral() != null)
            {
                return new AST.NumericLiteralNode
                {
                    Start = context.Start.StartIndex,
                    Line = context.Start.Line,
                    Value = context.DecimalLiteral().Symbol.Text
                };
            }

            return new AST.NumericLiteralNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Value = context.OctalIntegerLiteral().Symbol.Text
            };
        }

        public override object VisitBlock([NotNull] ECMAScriptParser.BlockContext context)
        {
            return new AST.BlockNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Statements = (context.statementList() == null)
                    ? (null)
                    : (AST.StatementListNode)Visit(context.statementList())
            };
        }

        public override dynamic VisitEmptyStatement([NotNull] ECMAScriptParser.EmptyStatementContext context)
        {
            return new AST.EmptyNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
            };
        }

        public override object VisitMemberDotExpression([NotNull] ECMAScriptParser.MemberDotExpressionContext context)
        {
            return new AST.MemberDotExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Expr = (AST.Node)Visit(context.singleExpression()),
                Ident = (AST.IdentNode)Visit(context.identifierName())
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
                Line = context.Start.Line,
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
                Line = context.Start.Line,
                Cond = (AST.ExprSequenceNode)Visit(context.expressionSequence()),
                Statement = (AST.Node)Visit(context.statement()[0]),
                AlterStatement = (context.statement().Length == 2)
                    ? ((AST.Node)Visit(context.statement()[1]))
                    : (null)
            };
        }

        public override object VisitWhileStatement([NotNull] ECMAScriptParser.WhileStatementContext context)
        {
            return new AST.WhileNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Body = (AST.Node)Visit(context.statement()),
                Cond = (AST.ExprSequenceNode)Visit(context.expressionSequence())
            };
        }

        public override object VisitReturnStatement([NotNull] ECMAScriptParser.ReturnStatementContext context)
        {
            return new AST.ReturnNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                ExprSeq = (context.expressionSequence() == null)
                    ? (null)
                    : ((AST.ExprSequenceNode)Visit(context.expressionSequence()))
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
                Line = context.Start.Line
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
                Line = context.Start.Line
            };
        }

        public override object VisitFunctionExpression([NotNull] ECMAScriptParser.FunctionExpressionContext context)
        {
            return new AST.FunctionExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Name = context.Identifier()?.Symbol.Text ?? "",
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
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Exprs = (context.sourceElements() == null)
                    ? (null)
                    : ((List<AST.Node>)Visit(context.sourceElements()))
            };
        }

        public override object VisitNewExpression([NotNull] ECMAScriptParser.NewExpressionContext context)
        {
            if (context.arguments() == null)
            {
                return new AST.NewExprNode
                {
                    Start = context.Start.StartIndex,
                    Line = context.Start.Line,
                    Expr = (AST.Node)Visit(context.singleExpression())
                };
            }
            else
            {
                return new AST.NewExprNode
                {
                    Start = context.Start.StartIndex,
                    Line = context.Start.Line,
                    Expr = new AST.CalleeExprNode
                    {
                        Start = context.arguments().Start.StartIndex,
                        Line = context.arguments().Start.Line,
                        LeftExpr = (AST.Node)Visit(context.singleExpression()),
                        Args = (List<AST.Node>)Visit(context.arguments())
                    }
                };
            }
        }

        public override object VisitIdentifierExpression([NotNull] ECMAScriptParser.IdentifierExpressionContext context)
        {
            return new AST.IdentNode
            {
                Name = context.Identifier().Symbol.Text,
                Start = context.Identifier().Symbol.StartIndex,
                Line = context.Identifier().Symbol.Line
            };
        }

        public override object VisitReservedWord([NotNull] ECMAScriptParser.ReservedWordContext context)
        {
            if (context.NullLiteral() != null)
            {
                return new AST.NullLiteralNode
                {
                    Start = context.NullLiteral().Symbol.StartIndex,
                    Line = context.NullLiteral().Symbol.Line,
                    Value = "null"
                };
            }
            else if (context.BooleanLiteral() != null)
            {
                return new AST.BoolLiteralNode
                {
                    Start = context.BooleanLiteral().Symbol.StartIndex,
                    Line = context.BooleanLiteral().Symbol.Line,
                    Value = context.BooleanLiteral().Symbol.Text
                };
            }

            return Visit(context.keyword());
        }

        public override object VisitKeyword([NotNull] ECMAScriptParser.KeywordContext context)
        {
            return new AST.KeyWordNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Kword = KeyWordMap[context.Start.Text]
            };
        }

        public override object VisitEqualityExpression([NotNull] ECMAScriptParser.EqualityExpressionContext context)
        {
            //TODO check this
            return new AST.EqualityExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Oper = EqualityMap[context.Start.Text],
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
            };
        }

        public override object VisitIdentifierName([NotNull] ECMAScriptParser.IdentifierNameContext context)
        {
            return new AST.IdentNode
            {
                Name = context.Identifier()?.Symbol.Text ?? context.reservedWord().Start.Text,
                Start = context.Identifier().Symbol.StartIndex,
                Line = context.Identifier().Symbol.Line
            };
        }

        public override object VisitFunctionDeclaration([NotNull] ECMAScriptParser.FunctionDeclarationContext context)
        {
            return new AST.FunctionDeclNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
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

        public override object VisitRelationalExpression([NotNull] ECMAScriptParser.RelationalExpressionContext context)
        {
            return new AST.RelOperNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Rel = RelationMap[context.relatOper().Start.Text]
            };
        }

        public override object VisitInstanceofExpression([NotNull] ECMAScriptParser.InstanceofExpressionContext context)
        {
            return new AST.InstanceOfNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1])
            };
        }

        public override object VisitBitAndExpression([NotNull] ECMAScriptParser.BitAndExpressionContext context)
        {
            return new AST.BinaryBitOperNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = BitOper.And
            };
        }

        public override object VisitBitOrExpression([NotNull] ECMAScriptParser.BitOrExpressionContext context)
        {
            return new AST.BinaryBitOperNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = BitOper.Or
            };
        }

        public override object VisitBitXOrExpression([NotNull] ECMAScriptParser.BitXOrExpressionContext context)
        {
            return new AST.BinaryBitOperNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = BitOper.Xor
            };
        }

        public override object VisitLogicalAndExpression([NotNull] ECMAScriptParser.LogicalAndExpressionContext context)
        {
            return new AST.BinaryLogicOperNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = LogicalOper.And
            };
        }

        public override object VisitLogicalOrExpression([NotNull] ECMAScriptParser.LogicalOrExpressionContext context)
        {
            return new AST.BinaryLogicOperNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = LogicalOper.And,
            };
        }

        public override object VisitAssignmentExpression([NotNull] ECMAScriptParser.AssignmentExpressionContext context)
        {
            return new AST.AssignmentExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1])
            };
        }

        public override object VisitThisExpression([NotNull] ECMAScriptParser.ThisExpressionContext context)
        {
            return new AST.ThisExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line
            };
        }

        public override object VisitArrayLiteralExpression([NotNull] ECMAScriptParser.ArrayLiteralExpressionContext context)
        {
            return new AST.ArrayLiteralExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Exprs = (List<AST.Node>)Visit(context.arrayLiteral())
            };
        }

        public override object VisitArrayLiteral([NotNull] ECMAScriptParser.ArrayLiteralContext context)
        {
            var list = (List<AST.Node>)Visit(context.elementList());
            if (context.elision() != null)
            {
                for (int i = 0; i < context.elision().GetText().Length; ++i)
                {
                    if (context.elision().GetText()[i] == ',')
                    {
                        list.Add((AST.Node)new AST.UndefLiteralNode
                        {
                            Start = context.elision().SourceInterval.a + i,
                            Line = context.elision().SourceInterval.a + i,
                            Value = "undefined"
                        });
                    }
                }
            }
            return list;
        }

        public override object VisitElementList([NotNull] ECMAScriptParser.ElementListContext context)
        {
            var list = new List<AST.Node>();

            foreach (var t in context.GetRuleContexts<ParserRuleContext>())
            {
                if (t.GetText()[0] == ',')
                {
                    for (int i = 0; i < t.GetText().Length; ++i)
                    {
                        if (t.GetText()[i] == ',')
                        {
                            list.Add((AST.Node)new AST.UndefLiteralNode
                            {
                                Start = t.SourceInterval.a + i,
                                Line = t.SourceInterval.a + i,
                                Value = "undefined"
                            });
                        }
                    }
                }
                else
                {
                    list.Add((AST.Node)Visit(t));
                }
            }

            return list;
        }

        public override object VisitObjectLiteralExpression([NotNull] ECMAScriptParser.ObjectLiteralExpressionContext context)
        {
            return new AST.ObjectLiteralExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Exprs = (List<AST.Node>)Visit(context.objectLiteral())
            };
        }

        public override object VisitObjectLiteral([NotNull] ECMAScriptParser.ObjectLiteralContext context)
        {
            return Visit(context.propertyNameAndValueList());
        }

        public override object VisitPropertyNameAndValueList([NotNull] ECMAScriptParser.PropertyNameAndValueListContext context)
        {
            var list = new List<AST.Node>();

            foreach (var a in context.propertyAssignment())
            {
                list.Add((AST.Node)Visit(a));
            }

            return list;
        }

        public override object VisitPropertyGetter([NotNull] ECMAScriptParser.PropertyGetterContext context)
        {
            return new AST.PropertyGetterNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Name = (AST.IdentNode)Visit(context.getter()),
                FuncBody = (AST.Node)Visit(context.functionBody())
            };
        }

        public override object VisitGetter([NotNull] ECMAScriptParser.GetterContext context)
        {
            return (AST.IdentNode)Visit(context.propertyName().identifierName());
        }

        public override object VisitPropertySetter([NotNull] ECMAScriptParser.PropertySetterContext context)
        {
            return new AST.PropertySetterNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Name = (AST.IdentNode)Visit(context.setter()),
                Param = (AST.IdentNode)Visit(context.propertySetParameterList()),
                FuncBody = (AST.Node)Visit(context.functionBody())
            };
        }

        public override object VisitPropertySetParameterList([NotNull] ECMAScriptParser.PropertySetParameterListContext context)
        {
            return new AST.IdentNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Name = context.Identifier().Symbol.Text
            };
        }

        public override object VisitSetter([NotNull] ECMAScriptParser.SetterContext context)
        {

            return (AST.IdentNode)Visit(context.propertyName().identifierName());
        }

        public override object VisitPropertyName([NotNull] ECMAScriptParser.PropertyNameContext context)
        {
            if (context.StringLiteral() != null)
            {
                return new AST.StringLiteralNode
                {
                    Start = context.Start.StartIndex,
                    Line = context.Start.Line,
                    Value = context.StringLiteral().Symbol.Text
                };
            }
            else if (context.numericLiteral() != null)
            {
                return (AST.NumericLiteralNode)Visit(context.numericLiteral());
            }
            else
            {
                return (AST.IdentNode)Visit(context.identifierName());
            }
        }

        public override object VisitPropertyExpressionAssignment([NotNull] ECMAScriptParser.PropertyExpressionAssignmentContext context)
        {
            return new AST.PropertyExprAssignmentNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                PropName = (AST.Node)Visit(context.propertyName()),
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitParenthesizedExpression([NotNull] ECMAScriptParser.ParenthesizedExpressionContext context)
        {
            return new AST.ParenthExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Expr = (AST.Node)Visit(context.expressionSequence())
            };
        }

        public override object VisitFormalParameterList([NotNull] ECMAScriptParser.FormalParameterListContext context)
        {
            AST.FormalParamList list = new AST.FormalParamList()
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Idents = new List<AST.IdentNode>()
            };

            foreach (var ident in context.Identifier())
            {
                list.Idents.Add(new AST.IdentNode
                {
                    Name = ident.Symbol.Text,
                    Start = ident.Symbol.StartIndex,
                    Line = ident.Symbol.Line
                });
            }

            return list;
        }

        public override object VisitMemberIndexExpression([NotNull] ECMAScriptParser.MemberIndexExpressionContext context)
        {
            return new AST.MemberIndExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Expr = (AST.Node)Visit(context.singleExpression()),
                Ind = (AST.ExprSequenceNode)Visit(context.expressionSequence()),
            };
        }

        public override object VisitArgumentsExpression([NotNull] ECMAScriptParser.ArgumentsExpressionContext context)
        {
            return new AST.CalleeExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                LeftExpr = (AST.Node)Visit(context.singleExpression()),
                Args = (List<AST.Node>)Visit(context.arguments())
            };
        }

        public override object VisitArguments([NotNull] ECMAScriptParser.ArgumentsContext context)
        {
            return (context.argumentList() == null)
                    ? (null)
                    : (Visit(context.argumentList()));
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

        public override object VisitPostIncrementExpression([NotNull] ECMAScriptParser.PostIncrementExpressionContext context)
        {
            return new AST.PostIncExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitPostDecreaseExpression([NotNull] ECMAScriptParser.PostDecreaseExpressionContext context)
        {
            return new AST.PostDecExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitPreIncrementExpression([NotNull] ECMAScriptParser.PreIncrementExpressionContext context)
        {
            return new AST.PrefDecExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitPreDecreaseExpression([NotNull] ECMAScriptParser.PreDecreaseExpressionContext context)
        {
            return new AST.PostDecExprNode
            {
                Start = context.Start.StartIndex,
                Line = context.Start.Line,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
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
