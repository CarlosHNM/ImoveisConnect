using ImoveisConnect.Domain.Entities;
using System.Linq.Expressions;

namespace ImoveisConnect.Domain.Specification
{
    internal class ApartamentoSpec : BaseSpecification<Apartamento>
    {
        public ApartamentoSpec(Expression<Func<Apartamento, bool>> criteria) : base(criteria)
        {
            AddInclude(a => a.Reservas);
            AddInclude(a => a.Vendas);
        }
    }
}
