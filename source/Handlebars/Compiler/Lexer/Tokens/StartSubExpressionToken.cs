﻿namespace Magxe.Handlebars.Compiler.Lexer.Tokens
{
    internal class StartSubExpressionToken : ExpressionScopeToken
    {
        public StartSubExpressionToken()
        {
        }

        public override string Value
        {
            get { return "("; }
        }

        public override TokenType Type
        {
            get { return TokenType.StartSubExpression; }
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}

