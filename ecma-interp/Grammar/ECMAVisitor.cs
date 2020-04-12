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
            var program = new AST.ProgramNode()
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };

            program.Statement = Visit(context.GetChild(0));

            return program;
        }

        //public override dynamic VisitSourceElements([NotNull] ECMAScriptParser.SourceElementsContext context)
        //{
        //    return Visit(context.GetChild(0));
        //}

        public override dynamic VisitEof([NotNull] ECMAScriptParser.EofContext context)
        {
            return new AST.EmptyNode()
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }

        public override dynamic VisitEos([NotNull] ECMAScriptParser.EosContext context)
        {
            return new AST.EmptyNode()
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex
            };
        }


        public override dynamic VisitStatement([NotNull] ECMAScriptParser.StatementContext context)
        {
            return Visit(context.GetChild(0));
        }

        public override dynamic VisitVariableStatement([NotNull] ECMAScriptParser.VariableStatementContext context)
        {
            var declList = new AST.VarDeclListNode()
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
            return new AST.VarDeclNode()
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
                Name = context.Start.Text,
                Value = context.Stop.Text
            };
        }

        public override dynamic VisitEmptyStatement([NotNull] ECMAScriptParser.EmptyStatementContext context)
        {
            return new AST.EmptyNode()
            {
                Start = context.Start.StartIndex,
                End = context.Stop.StopIndex,
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
