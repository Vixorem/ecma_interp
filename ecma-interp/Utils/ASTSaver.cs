using ecma_interp.Grammar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ecma_interp.Utils
{
    class ASTSaver
    {
        public string Path { get; set; } = @"..\..\..\..\ecma-interpTests\Tests\Grammar\TestSrc";

        public void SaveAll()
        {
            foreach (var file in Directory.GetFiles(Path, "*.js", SearchOption.AllDirectories))
            {
                var printer = new ASTRepresenter(file + ".txt");
                var parser = new ParserRunner(File.ReadAllText(file));
                printer.VisitNode(parser.Run());
            }
        }
    }
}
