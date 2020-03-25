using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ecma_interp.Grammar
{
    public class MyErrorListener<T> : IAntlrErrorListener<T>
    {
        private static bool REPORT_SYNTAX_ERRORS = true;
        public static MyErrorListener<T> INSTANCE = new MyErrorListener<T>();

        public void SyntaxError(IRecognizer recognizer, T offendingSymbol,
            int line, int charPositionInLine, string msg, RecognitionException e)
        {
            if (!REPORT_SYNTAX_ERRORS)
            {
                return;
            }

            Console.Error.WriteLine("line " + line + ":" +
                charPositionInLine + " " + msg);
        }

    }
}
