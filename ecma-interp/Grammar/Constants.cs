using System;
using System.Collections.Generic;
using System.Text;

namespace ecma_interp.Grammar
{
    public class Constants
    {
        public enum Relation
        {
            Less,
            Greater,
            LessEq,
            GreaterEq
        }

        public enum Equality
        {
            Eq,
            StrictEq,
            NotEq,
            NotStrictEq
        }

        public enum LogicalOper
        {
            And,
            Or
        }

        public enum UnaryOper
        {
            Neg,
            Plus,
            Minus
        }

        public enum BitUnaryOper
        {
            Neg
        }

        public enum BitOper
        {
            And,
            Or,
            Xor
        }

        public enum BitShift
        {
            ZeroLeft,
            SignedRight,
            ZeroRight
        }

        public enum MultiplicOper
        {
            Mul,
            Div,
            Mod
        }

        public enum AdditiveOper
        {
            Add,
            Sub
        }

        public enum KeyWord
        {
            Break,
            Instanceof,
            Typeof,
            Else,
            New,
            Var,
            Return,
            Void,
            Continue,
            While,
            Function,
            This,
            If,
            Delete,
            In
        }

        public enum ExprUpdate
        {
            PrefixInc,
            PrefixDec,
            PostfixInc,
            PostfixDec
        }

        public static Dictionary<string, Relation> RelationMap = new Dictionary<string, Relation>
        {
            {"<", Relation.Less},
            {">", Relation.Greater},
            {"<=", Relation.LessEq},
            {">=", Relation.GreaterEq}
        };

        public static Dictionary<string, Equality> EqualityMap = new Dictionary<string, Equality>
        {
            {"==", Equality.Eq},
            {"!=", Equality.NotEq},
            {"===", Equality.StrictEq},
            {"!==", Equality.NotStrictEq}
        };

        public static Dictionary<string, LogicalOper> LogicalOperMap = new Dictionary<string, LogicalOper>
        {
            {"&&", LogicalOper.And},
            {"||", LogicalOper.Or}
        };

        public static Dictionary<string, UnaryOper> UnaryOperMap = new Dictionary<string, UnaryOper>
        {
            {"!", UnaryOper.Neg},
            {"-", UnaryOper.Minus},
            {"+", UnaryOper.Plus}
        };

        public static Dictionary<string, BitUnaryOper> BitUnaryOperMap = new Dictionary<string, BitUnaryOper>
        {
            {"~", BitUnaryOper.Neg}
        };

        public static Dictionary<string, BitOper> BitOperMap = new Dictionary<string, BitOper>
        {
            {"&", BitOper.And},
            {"|", BitOper.Or},
            {"^", BitOper.Xor}
        };

        public static Dictionary<string, MultiplicOper> MultiplicOperMap = new Dictionary<string, MultiplicOper>
        {
            {"*", MultiplicOper.Mul},
            {"/", MultiplicOper.Div},
            {"%", MultiplicOper.Mod}
        };

        public static Dictionary<string, BitShift> BitShiftMap = new Dictionary<string, BitShift>
        {
            {"<<", BitShift.ZeroLeft},
            {">>", BitShift.SignedRight},
            {">>>", BitShift.ZeroRight}
        };

        public static Dictionary<string, AdditiveOper> AdditiveOperMap = new Dictionary<string, AdditiveOper>
        {
            {"+", AdditiveOper.Add},
            {"-", AdditiveOper.Sub}
        };

        public static Dictionary<string, KeyWord> KeyWordMap = new Dictionary<string, KeyWord>
        {
            {"break", KeyWord.Break},
            {"instanceof", KeyWord.Instanceof},
            {"typeof", KeyWord.Typeof},
            {"else", KeyWord.Else},
            {"new", KeyWord.New},
            {"var", KeyWord.Var},
            {"return", KeyWord.Return},
            {"void", KeyWord.Void},
            {"continue", KeyWord.Continue},
            {"while", KeyWord.While},
            {"function", KeyWord.Function},
            {"this", KeyWord.This},
            {"if", KeyWord.If},
            {"delete", KeyWord.Delete},
            {"in", KeyWord.In},
        };

    }
}
