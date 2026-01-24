using Application.DTOs;
using Domain.Entities;

namespace Application.UseCases.ImportarFatura
{
    public class ImportarFaturaResult
    {
        public int TotalLancamentos { get; }
        public IReadOnlyCollection<LancamentoDto> Lancamentos { get; }

        public ImportarFaturaResult(IEnumerable<LancamentoCartao> lancamentos)
        {
            var lista = lancamentos.ToList();

            TotalLancamentos = lista.Count;

            Lancamentos = lista.Select(l => new LancamentoDto
            {
                Data = l.Data,
                Descricao = l.Descricao,
                Valor = l.Valor,
                NumeroParcela = l.Parcela?.Numero,
                TotalParcelas = l.Parcela?.Total
            }).ToList();
        }
    }
}
