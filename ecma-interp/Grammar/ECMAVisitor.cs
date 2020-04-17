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

        public override object VisitDeleteExpression([NotNull] ECMAScriptParser.DeleteExpressionContext context)
        {
            return new AST.DeleteNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitVoidExpression([NotNull] ECMAScriptParser.VoidExpressionContext context)
        {
            return new AST.VoidNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitUnaryMinusExpression([NotNull] ECMAScriptParser.UnaryMinusExpressionContext context)
        {
            return new AST.UnaryMinusNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitUnaryPlusExpression([NotNull] ECMAScriptParser.UnaryPlusExpressionContext context)
        {
            return new AST.UnaryPlusNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitNotExpression([NotNull] ECMAScriptParser.NotExpressionContext context)
        {
            return new AST.UnaryNotNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitBitNotExpression([NotNull] ECMAScriptParser.BitNotExpressionContext context)
        {
            return new AST.UnaryBitNotNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitAdditiveExpression([NotNull] ECMAScriptParser.AdditiveExpressionContext context)
        {
            return new AST.AdditiveNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = (context.additOpers().Start.Text == "+")
                    ? (AST.AdditiveNode.OperType.Add)
                    : (AST.AdditiveNode.OperType.Sub)
            };
        }

        public override object VisitMultiplicativeExpression([NotNull] ECMAScriptParser.MultiplicativeExpressionContext context)
        {
            AST.MultiplicNode.OperType op;
            if (context.multOpers().Start.Text == "*")
            {
                op = AST.MultiplicNode.OperType.Mul;
            }
            else if (context.multOpers().Start.Text == "/")
            {
                op = AST.MultiplicNode.OperType.Div;
            }
            else
            {
                op = AST.MultiplicNode.OperType.Mod;
            }
            return new AST.MultiplicNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = op
            };
        }

        public override object VisitBitShiftExpression([NotNull] ECMAScriptParser.BitShiftExpressionContext context)
        {

            AST.BitWiseNode.WiseType type;
            if (context.bitWiseSigns().Start.Text == ">>>")
            {
                type = AST.BitWiseNode.WiseType.ZeroRight;
            }
            else if (context.bitWiseSigns().Start.Text == ">>")
            {
                type = AST.BitWiseNode.WiseType.SignedRight;
            }
            else
            {
                type = AST.BitWiseNode.WiseType.ZeroLeft;
            }
            return new AST.BitWiseNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Sign = context.bitWiseSigns().Start.Text,
                Wise = type,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1])
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

            return new AST.StatementListNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Statements = list
            };
        }

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
                End = context.Stop.StopIndex,
            };
        }

        public override object VisitMemberDotExpression([NotNull] ECMAScriptParser.MemberDotExpressionContext context)
        {
            return new AST.MemberDotExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
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
                End = context.Stop.StopIndex,
                Body = (AST.Node)Visit(context.statement()),
                Cond = (AST.ExprSequenceNode)Visit(context.expressionSequence())
            };
        }

        public override object VisitReturnStatement([NotNull] ECMAScriptParser.ReturnStatementContext context)
        {
            return new AST.ReturnNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
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
                Type = "Function body",
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
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
                    End = context.Stop.StopIndex,
                    Expr = (AST.Node)Visit(context.singleExpression())
                };
            }
            else
            {
                return new AST.NewExprNode
                {
                    Start = context.Start.StartIndex,
                    End = context.Stop.StopIndex,
                    Expr = new AST.CalleeExprNode
                    {
                        Start = context.arguments().Start.StartIndex,
                        End = context.arguments().Stop.StopIndex,
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
                End = context.Identifier().Symbol.StopIndex
            };
        }

        public override object VisitReservedWord([NotNull] ECMAScriptParser.ReservedWordContext context)
        {
            if (context.NullLiteral() != null)
            {
                return new AST.NullLiteralNode
                {
                    Start = context.NullLiteral().Symbol.StartIndex,
                    End = context.NullLiteral().Symbol.StopIndex,
                    Value = "null"
                };
            }
            else if (context.BooleanLiteral() != null)
            {
                return new AST.BoolLiteralNode
                {
                    Start = context.BooleanLiteral().Symbol.StartIndex,
                    End = context.BooleanLiteral().Symbol.StopIndex,
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
                End = context.Start.StopIndex,
                Kword = context.Start.Text
            };
        }

        public override object VisitEqualityExpression([NotNull] ECMAScriptParser.EqualityExpressionContext context)
        {
            var sign = context.Start.Text;
            AST.EqualityExprNode.OperType op;
            if (sign == "==")
            {
                op = AST.EqualityExprNode.OperType.Eq;
            }
            else if (sign == "!=")
            {
                op = AST.EqualityExprNode.OperType.NotEq;
            }
            else if (sign == "===")
            {
                op = AST.EqualityExprNode.OperType.StrictEq;
            }
            else
            {
                op = AST.EqualityExprNode.OperType.NotStrictEq;
            }

            return new AST.EqualityExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Start.StopIndex,
                Sign = sign,
                Oper = op,
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

        public override object VisitRelationalExpression([NotNull] ECMAScriptParser.RelationalExpressionContext context)
        {
            AST.RelOperNode.RelType t;
            var sign = context.relatOper().Start.Text;
            if (sign == "<")
            {
                t = AST.RelOperNode.RelType.Less;
            }
            else if (sign == "<=")
            {
                t = AST.RelOperNode.RelType.LessEq;
            }
            else if (sign == ">")
            {
                t = AST.RelOperNode.RelType.Greater;
            }
            else
            {
                t = AST.RelOperNode.RelType.GreaterEq;
            }
            return new AST.RelOperNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Rel = t,
                Sign = sign
            };
        }

        public override object VisitInstanceofExpression([NotNull] ECMAScriptParser.InstanceofExpressionContext context)
        {
            return new AST.InstanceOfNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1])
            };
        }

        public override object VisitBitAndExpression([NotNull] ECMAScriptParser.BitAndExpressionContext context)
        {
            return new AST.BinaryBitOperNode
            {
                Type = "Bit AND operation",
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = AST.BinaryBitOperNode.OperType.BitAnd,
                Sign = "&"
            };
        }

        public override object VisitBitOrExpression([NotNull] ECMAScriptParser.BitOrExpressionContext context)
        {
            return new AST.BinaryBitOperNode
            {
                Type = "Bit OR operation",
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = AST.BinaryBitOperNode.OperType.BitOr,
                Sign = "|"
            };
        }

        public override object VisitBitXOrExpression([NotNull] ECMAScriptParser.BitXOrExpressionContext context)
        {
            return new AST.BinaryBitOperNode
            {
                Type = "Bit XOR operation",
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = AST.BinaryBitOperNode.OperType.BitXor,
                Sign = "^"
            };
        }

        public override object VisitLogicalAndExpression([NotNull] ECMAScriptParser.LogicalAndExpressionContext context)
        {
            return new AST.BinaryLogicOperNode
            {
                Type = "Logic AND operation",
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = AST.BinaryLogicOperNode.OperType.And,
                Sign = "&&"
            };
        }

        public override object VisitLogicalOrExpression([NotNull] ECMAScriptParser.LogicalOrExpressionContext context)
        {
            return new AST.BinaryLogicOperNode
            {
                Type = "Logic OR operation",
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1]),
                Oper = AST.BinaryLogicOperNode.OperType.And,
                Sign = "||"
            };
        }

        public override object VisitAssignmentExpression([NotNull] ECMAScriptParser.AssignmentExpressionContext context)
        {
            return new AST.AssignmentExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Left = (AST.Node)Visit(context.singleExpression()[0]),
                Right = (AST.Node)Visit(context.singleExpression()[1])
            };
        }

        public override object VisitThisExpression([NotNull] ECMAScriptParser.ThisExpressionContext context)
        {
            return new AST.ThisExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override object VisitArrayLiteralExpression([NotNull] ECMAScriptParser.ArrayLiteralExpressionContext context)
        {
            return new AST.ArrayLiteralExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Exprs = (List<AST.Node>)Visit(context.arrayLiteral())
            };
        }

        public override object VisitArrayLiteral([NotNull] ECMAScriptParser.ArrayLiteralContext context)
        {
            var list = (List<AST.Node>)Visit(context.elementList());
            if (context.elision() != null)
            {
                //todo
                list.Add((AST.Node)Visit(context.elision()));
            }
            return list;
        }

        public override object VisitElision([NotNull] ECMAScriptParser.ElisionContext context)
        {
            return new AST.EmptyNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override object VisitElementList([NotNull] ECMAScriptParser.ElementListContext context)
        {
            var list = new List<AST.Node>();

            foreach (var t in context.GetRuleContexts<ParserRuleContext>())
            {
                //todo
                list.Add((AST.Node)Visit(t));
            }

            return list;
        }

        public override object VisitObjectLiteralExpression([NotNull] ECMAScriptParser.ObjectLiteralExpressionContext context)
        {
            return new AST.ObjectLiteralExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
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
                End = context.Stop.StopIndex,
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
                End = context.Stop.StopIndex,
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
                End = context.Stop.StopIndex,
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
                    End = context.Stop.StopIndex,
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
                End = context.Stop.StopIndex,
                PropName = (AST.Node)Visit(context.propertyName()),
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitParenthesizedExpression([NotNull] ECMAScriptParser.ParenthesizedExpressionContext context)
        {
            return new AST.ParenthExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.expressionSequence())
            };
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
            return new AST.CalleeExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                LeftExpr = (AST.Node)Visit(context.singleExpression()),
                Args = (List<AST.Node>)Visit(context.arguments())
            };
        }

        public override object VisitArguments([NotNull] ECMAScriptParser.ArgumentsContext context)
        {
            return Visit(context.argumentList());
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
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitPostDecreaseExpression([NotNull] ECMAScriptParser.PostDecreaseExpressionContext context)
        {
            return new AST.PostDecExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitPreIncrementExpression([NotNull] ECMAScriptParser.PreIncrementExpressionContext context)
        {
            return new AST.PrefDecExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Expr = (AST.Node)Visit(context.singleExpression())
            };
        }

        public override object VisitPreDecreaseExpression([NotNull] ECMAScriptParser.PreDecreaseExpressionContext context)
        {
            return new AST.PostDecExprNode
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
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
