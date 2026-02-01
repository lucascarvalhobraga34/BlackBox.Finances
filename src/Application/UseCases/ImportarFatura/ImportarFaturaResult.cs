using Application.DTOs;
using Domain.Entities;

namespace Application.UseCases.ImportarFatura
{
    public class ImportarFaturaResult
    {
        public int TotalLancamentos { get; }
        public IReadOnlyCollection<LancamentoDto> Lancamentos { get; }

        public ImportarFaturaResult(IEnumerable<LancamentoFatura> lancamentos)
        {
            var lista = lancamentos.ToList();

            TotalLancamentos = lista.Count;

            Lancamentos = lista.Select(l => new LancamentoDto
            {
                DataLancamento = l.DataLancamento,
                Descricao = l.Descricao,
                Valor = l.Valor,
                NumeroParcela = l.NumeroParcela,
                TotalParcelas = l.TotalParcelas
            }).ToList();
        }
    }
}
