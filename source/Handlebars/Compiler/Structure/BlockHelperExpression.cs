﻿using System.Collections.Generic;
using System.Linq.Expressions;

namespace Magxe.Handlebars.Compiler.Structure
{
    internal class BlockHelperExpression : HelperExpression
    {
        private readonly Expression _body;
        private readonly Expression _inversion;

        public BlockHelperExpression(
            string helperName,
            IEnumerable<Expression> arguments,
            Expression body,
            Expression inversion)
            : base(helperName, arguments)
        {
            _body = body;
            _inversion = inversion;
        }

        public Expression Body
        {
            get { return _body; }
        }

        public Expression Inversion
        {
            get { return _inversion; }
        }

        public override ExpressionType NodeType
        {
            get { return (ExpressionType)HandlebarsExpressionType.BlockExpression; }
        }
    }
}

