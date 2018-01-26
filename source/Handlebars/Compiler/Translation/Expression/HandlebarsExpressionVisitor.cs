using System.Linq.Expressions;
using Magxe.Handlebars.Compiler.Structure;

namespace Magxe.Handlebars.Compiler.Translation.Expression
{
    internal abstract class HandlebarsExpressionVisitor : ExpressionVisitor
    {
        private readonly CompilationContext _compilationContext;

        protected HandlebarsExpressionVisitor(CompilationContext compilationContext)
        {
            _compilationContext = compilationContext;
        }

        protected virtual CompilationContext CompilationContext
        {
            get { return _compilationContext; }
        }

        public override System.Linq.Expressions.Expression Visit(System.Linq.Expressions.Expression exp)
        {
            if (exp == null)
            {
                return null;
            }
            switch ((HandlebarsExpressionType)exp.NodeType)
            {
                case HandlebarsExpressionType.StatementExpression:
                    return VisitStatementExpression((StatementExpression)exp);
                case HandlebarsExpressionType.StaticExpression:
                    return VisitStaticExpression((StaticExpression)exp);
                case HandlebarsExpressionType.HelperExpression:
                    return VisitHelperExpression((HelperExpression)exp);
                case HandlebarsExpressionType.BlockExpression:
                    return VisitBlockHelperExpression((BlockHelperExpression)exp);
                case HandlebarsExpressionType.HashParametersExpression:
                    return VisitHashParametersExpression((HashParametersExpression)exp);
                case HandlebarsExpressionType.PathExpression:
                    return VisitPathExpression((PathExpression)exp);
                case HandlebarsExpressionType.IteratorExpression:
                    return VisitIteratorExpression((IteratorExpression)exp);
                case HandlebarsExpressionType.DeferredSection:
                    return VisitDeferredSectionExpression((DeferredSectionExpression)exp);
                case HandlebarsExpressionType.PartialExpression:
                    return VisitPartialExpression((PartialExpression)exp);
                case HandlebarsExpressionType.BoolishExpression:
                    return VisitBoolishExpression((BoolishExpression)exp);
                case HandlebarsExpressionType.SubExpression:
                    return VisitSubExpression((SubExpressionExpression)exp);
                default:
                    return base.Visit(exp);
            }
        }

        protected virtual System.Linq.Expressions.Expression VisitStatementExpression(StatementExpression sex)
        {
            return sex;
        }

        protected virtual System.Linq.Expressions.Expression VisitPathExpression(PathExpression pex)
        {
            return pex;
        }

        protected virtual System.Linq.Expressions.Expression VisitHelperExpression(HelperExpression hex)
        {
            return hex;
        }

        protected virtual System.Linq.Expressions.Expression VisitBlockHelperExpression(BlockHelperExpression bhex)
        {
            return bhex;
        }

        protected virtual System.Linq.Expressions.Expression VisitStaticExpression(StaticExpression stex)
        {
            return stex;
        }

        protected virtual System.Linq.Expressions.Expression VisitIteratorExpression(IteratorExpression iex)
        {
            return iex;
        }

        protected virtual System.Linq.Expressions.Expression VisitDeferredSectionExpression(DeferredSectionExpression dsex)
        {
            return dsex;
        }

        protected virtual System.Linq.Expressions.Expression VisitPartialExpression(PartialExpression pex)
        {
            return pex;
        }

        protected virtual System.Linq.Expressions.Expression VisitBoolishExpression(BoolishExpression bex)
        {
            return bex;
        }

        protected virtual System.Linq.Expressions.Expression VisitSubExpression(SubExpressionExpression subex)
        {
            return subex;
        }

        protected virtual System.Linq.Expressions.Expression VisitHashParametersExpression(HashParametersExpression hpex)
        {
            return hpex;
        }
    }
}

