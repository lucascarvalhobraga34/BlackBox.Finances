namespace Domain.Entities
{
    public class LancamentoFatura
    {
        public Guid Id { get; private set; }
        public Guid IdFatura { get; private set; }
        public DateTime DataLancamento { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public int NumeroParcela { get; private set; }
        public int TotalParcelas { get; private set; }
        protected LancamentoFatura() { }

        public LancamentoFatura(
            Fatura fatura,
            DateTime dataLancamento,
            string descricao,
            decimal valor,
            int numParcela,
            int totalParcelas)
        {
            Id = Guid.NewGuid();
            IdFatura = fatura.Id;
            DataCadastro = DateTime.Now;
            DataLancamento = dataLancamento;
            Descricao = descricao;
            Valor = valor;
            NumeroParcela = numParcela;
            TotalParcelas = totalParcelas;
        }

        public Fatura Fatura { get; set; }
    }
}
