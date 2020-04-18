using Antlr4.Runtime;
using ecma_interp.Grammar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ecma_interp.Tests
{
    [TestClass]
    public class GrammarTests
    {
        public string Path { get; set; } = @"..\..\..\Tests\Grammar\TestSrc";

        [TestMethod]
        public void TestGeneratedAST()
        {
            foreach (var file in Directory.GetFiles(Path, "*.js", SearchOption.AllDirectories))
            {
                var printer = new ASTRepresenter()
                {
                    KeepInRAM = true
                };
                var parser = new ParserRunner(File.ReadAllText(file));
                printer.VisitNode(parser.Run());

                var expected = File.ReadAllText(file + ".txt");
                Assert.AreEqual(printer.TextedAST, expected);

                printer.TextedAST = "";
            }
        }
    }
}
