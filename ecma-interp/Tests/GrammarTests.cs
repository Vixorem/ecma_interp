using Antlr4.Runtime;
using ecma_interp.Grammar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ecma_interp.Tests
{
    [TestClass]
    public class GrammarTests
    {
        private ECMAScriptParser Setup(string file)
        {
            var inputStream = new AntlrInputStream(file);
            var speakLexer = new ECMAScriptLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(speakLexer);
            var speakParser = new ECMAScriptParser(commonTokenStream);
            return speakParser;
        }

    }
}
