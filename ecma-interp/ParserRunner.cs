using Antlr4.Runtime;
using ecma_interp.Grammar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ecma_interp
{
    class ParserRunner
    {
        public string Input { get; set; }
        public ParserRunner(string input)
        {
            Input = input;
        }

        public dynamic Run()
        {
            var inputStream = new AntlrInputStream(Input);
            var lexer = new ECMAScriptLexer(inputStream);
            lexer.RemoveErrorListeners();
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new ECMAScriptParser(tokenStream) { BuildParseTree = true };
            parser.RemoveErrorListeners();
            parser.AddErrorListener(MyErrorListener<IToken>.INSTANCE);
            var ctx = parser.program();
            var visitor = new ECMAVisitor();
            return visitor.Visit(ctx);
        }
    }
}
