using ImoveisConnect.Domain.Entities;
using System.Linq.Expressions;

namespace ImoveisConnect.Domain.Specification
{
    internal class ReservaSpec : BaseSpecification<Reserva>
    {
        public ReservaSpec(Expression<Func<Reserva, bool>> criteria) : base(criteria)
        {
            AddInclude(x => x.Cliente);
            AddInclude(x => x.Apartamento);
        }
    }
}
