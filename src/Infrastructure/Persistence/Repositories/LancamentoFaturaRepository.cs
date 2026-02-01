using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class LancamentoFaturaRepository : EfRepository<LancamentoFatura>, ILancamentoFaturaRepository
    {
        public LancamentoFaturaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
