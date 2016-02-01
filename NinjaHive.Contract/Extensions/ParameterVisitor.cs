using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NinjaHive.Contract.Extensions
{
    public class ParameterVisitor: ExpressionVisitor
    {
        private IReadOnlyCollection<ParameterExpression> from, to;

        public ParameterVisitor(IReadOnlyCollection<ParameterExpression> from,
                                IReadOnlyCollection<ParameterExpression> to)
        {
            if(from == null) throw new ArgumentNullException("from");
            if(to == null) throw new ArgumentNullException("from");
            if (from.Count < to.Count) throw new ArgumentException("From must be greater than or equal in length to the To collection.");

            this.from = from;
            this.to = to;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            for(int i=0;i<to.Count;++i)
            {
                if (node == from.ElementAt(i))
                    return to.ElementAt(i);
            }
            return node;
        }
    }

    public static class VisitorExtensions
    {
        public static Expression StripParameters<T>(this Expression<Func<T,bool>> function, params ParameterExpression[] newParams)
        {
            var visitor = new ParameterVisitor(function.Parameters, newParams).VisitAndConvert(function.Body,"ReplaceParameter");

            return visitor;
        }
    }
}
