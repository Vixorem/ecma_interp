using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ecma_interp.Grammar;
using System;
using System.Text;

namespace ecma_interp
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            StringBuilder text = new StringBuilder();

            // CTRL+D and then <enter>
            while ((input = Console.ReadLine()) != "\u0004")
            {
                text.AppendLine(input);
            }
            var inputStream = new AntlrInputStream(text.ToString());
            var lexer = new ECMAScriptLexer(inputStream);
            lexer.RemoveErrorListeners();
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ECMAScriptParser(tokenStream) { BuildParseTree = true };
            parser.RemoveErrorListeners();
            parser.AddErrorListener(MyErrorListener<IToken>.INSTANCE);
            var ctx = parser.program();
            var visitor = new ECMAVisitor();
            var node = visitor.Visit(ctx);
            //Console.WriteLine(ctx.ToStringTree());
            var printer = new ASTRepresenter((AST.Node)node);
        }
    }
}
