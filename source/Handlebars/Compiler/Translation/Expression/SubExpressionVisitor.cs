﻿using Magxe.Handlebars.Compiler.Structure;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Magxe.Handlebars.Compiler.Translation.Expression
{
    internal class SubExpressionVisitor : HandlebarsExpressionVisitor
    {
        public static System.Linq.Expressions.Expression Visit(System.Linq.Expressions.Expression expr, CompilationContext context)
        {
            return new SubExpressionVisitor(context).Visit(expr);
        }

        private SubExpressionVisitor(CompilationContext context)
            : base(context)
        {
        }

        protected override System.Linq.Expressions.Expression VisitSubExpression(SubExpressionExpression subex)
        {
            if (!(subex.Expression is MethodCallExpression helperCall))
            {
                throw new HandlebarsCompilerException("Sub-expression does not contain a converted MethodCall expression");
            }
            HandlebarsHelper helper = GetHelperDelegateFromMethodCallExpression(helperCall);
            return System.Linq.Expressions.Expression.Call(
#if netstandard
                new Func<HandlebarsHelper, object, object[], string>(CaptureTextWriterOutputFromHelper).GetMethodInfo(),
#else
                new Func<HandlebarsHelper, object, object[], string>(CaptureTextWriterOutputFromHelper).Method,
#endif
                System.Linq.Expressions.Expression.Constant(helper),
                Visit(helperCall.Arguments[1]),
                Visit(helperCall.Arguments[2]));
        }

        private static HandlebarsHelper GetHelperDelegateFromMethodCallExpression(MethodCallExpression helperCall)
        {
            object target = helperCall.Object;
            HandlebarsHelper helper;
            if (target != null)
            {
                if (target is ConstantExpression)
                {
                    target = ((ConstantExpression)target).Value;
                }
                else
                {
                    throw new NotSupportedException("Helper method instance target must be reduced to a ConstantExpression");
                }
#if netstandard
                helper = (HandlebarsHelper)helperCall.Method.CreateDelegate(typeof(HandlebarsHelper), target);
#else
                helper = (HandlebarsHelper)Delegate.CreateDelegate(typeof(HandlebarsHelper), target, helperCall.Method);
#endif
            }
            else
            {
#if netstandard
                helper = (HandlebarsHelper)helperCall.Method.CreateDelegate(typeof(HandlebarsHelper));
#else
                helper = (HandlebarsHelper)Delegate.CreateDelegate(typeof(HandlebarsHelper), helperCall.Method);
#endif
            }
            return helper;
        }

        private static string CaptureTextWriterOutputFromHelper(
            HandlebarsHelper helper,
            object context,
            object[] arguments)
        {
            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
            {
                helper(writer, context, arguments);
            }
            return builder.ToString();
        }
    }
}

