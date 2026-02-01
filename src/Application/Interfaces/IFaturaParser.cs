using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFaturaParser
    {
        List<LancamentoFatura> Parse(Fatura fatura, string mesAno, Stream arquivo);
    }
}
