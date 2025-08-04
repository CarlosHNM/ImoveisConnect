using ImoveisConnect.Application.Core;
using ImoveisConnect.Application.Extensions;
using ImoveisConnect.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImoveisConnect.Infra.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            // A new query where the result set will not be tracked by the context.
            query = query.AsNoTracking();

            if (specification == null) return query;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Include any string-based include statements
            query = specification.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.OrderBy_String != null)
            {
                if (specification.OrderBy_DirectionString == "DESC")
                    query = query.OrderByColumnDescending(specification.OrderBy_String);
                else
                    query = query.OrderByColumn(specification.OrderBy_String);
            }

            //query = query.OrderBy()

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }

            //string strQuery = query.ToQueryString();

            return query;
        }
    }
}
