using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ecma_interp.Grammar;
using ecma_interp.Utils;
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
            var parser = new ParserRunner(text.ToString());
            var node = parser.Run();
            var printer = new ASTRepresenter();
            printer.VisitNode(node);
            //var saver = new ASTSaver();
            //saver.SaveAll();
        }
    }
}
