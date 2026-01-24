using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFaturaParser
    {
        List<LancamentoCartao> Parse(Stream arquivo);
    }
}
