using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILancamentoCartaoRepository
    {
        Task AddRangeAsync(IEnumerable<LancamentoCartao> lancamentos);
    }
}
