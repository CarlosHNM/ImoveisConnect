using ImoveisConnect.Application.Core;
using ImoveisConnect.Application.Delegates;
using ImoveisConnect.Domain.Interfaces;
using System.Linq.Expressions;

namespace ImoveisConnect.Domain.Specification
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        protected BaseSpecification(Expression<Func<T, bool>> criteria,
           PageAndSort pageAndSort)
        {
            Criteria = criteria;
            ApplyPaging(pageAndSort.PageNumber, pageAndSort.PageSize);
            SortEntityExp = pageAndSort.SortEntityExp != null && pageAndSort.SortEntityExp.Length > 0 ? pageAndSort.SortEntityExp/*.ToLower()*/ : null;
            SortDirection = pageAndSort.SortDirection != null && pageAndSort.SortDirection.Length > 0 ? pageAndSort.SortDirection/*.ToLower()*/ : null;
            //SortEntityExpType = pageAndSort.SortEntityExpType != null && pageAndSort.SortEntityExpType.Length > 0 ? pageAndSort.SortEntityExpType : "OBJECT";

            OrderBy_String = SortEntityExp;
            OrderBy_DirectionString = SortDirection;

        }

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        protected BaseSpecification()
        {

        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();

        public Expression<Func<T, bool>> OrderBy_Boolean { get; private set; } = null;
        public Expression<Func<T, bool>> OrderByDescending_Boolean { get; private set; } = null;

        public Expression<Func<T, object>> OrderBy { get; private set; } = null;
        public Expression<Func<T, object>> OrderByDescending { get; private set; } = null;

        public string OrderBy_String { get; private set; } = null;
        public string OrderBy_DirectionString { get; private set; } = null;

        public Expression<Func<T, object>> GroupBy { get; private set; }
        public OnSpecificationOrderByEvaluating<T> OnSpecificationOrderByEvaluating { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public string SortEntityExp { get; private set; } = null;
        /// <summary>
        /// OBJECT OR BOOL
        /// </summary>
        public string SortEntityExpType { get; private set; } = "OBJECT";
        public string SortDirection { get; private set; } = null;
        public bool IsPagingEnabled { get; private set; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeExpression"></param>
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeString"></param>
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        protected virtual void ApplyPaging(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            Skip = skip < 0 ? 0 : skip;
            Take = pageSize;
            IsPagingEnabled = true;
        }

        protected virtual void ApplyPaging(PageAndSort pageAndSort)
        {
            var skip = (pageAndSort.PageNumber - 1) * pageAndSort.PageSize;
            Skip = skip < 0 ? 0 : skip;
            Take = pageAndSort.PageSize;
            IsPagingEnabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        protected virtual void ApplyOrderByExpression(OnSpecificationOrderByEvaluating<T> exp)
        {
            OnSpecificationOrderByEvaluating = exp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderByExpression"></param>
        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectAsString"></param>
        /// <param name="direction">ASC | DESC</param>
        protected virtual void ApplyOrderBy_String(string objectAsString, string direction)
        {
            OrderBy_String = objectAsString;
            OrderBy_DirectionString = direction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderByDescendingExpression"></param>
        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupByExpression"></param>
        protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}
