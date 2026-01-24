using Domain.ValueObjects;

namespace Domain.Entities
{
    public class LancamentoCartao
    {
        public Guid Id { get; private set; }
        public DateTime Data { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public Parcela? Parcela { get; private set; }

        protected LancamentoCartao() { } // EF

        public LancamentoCartao(
            DateTime data,
            string descricao,
            decimal valor,
            Parcela? parcela)
        {
            Id = Guid.NewGuid();
            Data = data;
            Descricao = descricao;
            Valor = valor;
            Parcela = parcela;
        }
    }
}
