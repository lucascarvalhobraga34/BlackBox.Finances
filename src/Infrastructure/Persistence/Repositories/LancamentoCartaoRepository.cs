using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class LancamentoCartaoRepository : ILancamentoCartaoRepository
    {
        private readonly AppDbContext _context;

        public LancamentoCartaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<LancamentoCartao> lancamentos)
        {
            await _context.Lancamentos.AddRangeAsync(lancamentos);
        }
    }
}
