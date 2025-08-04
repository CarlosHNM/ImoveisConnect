using ImoveisConnect.Domain.Entities;
using System.Linq.Expressions;

namespace ImoveisConnect.Domain.Specification
{
    public class VendasSpec : BaseSpecification<Venda>
    {
        public VendasSpec(Expression<Func<Venda, bool>> criteria) : base(criteria)
        {
            AddInclude(x => x.Cliente);
            AddInclude(x => x.Apartamento);
            AddInclude(x => x.Apartamento.Reservas);
        }

    }
}
