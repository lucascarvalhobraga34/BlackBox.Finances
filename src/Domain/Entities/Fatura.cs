namespace Domain.Entities
{
    public class Fatura
    {
        public Guid Id { get; private set; }
        public Guid IdCartao { get; private set; }
        public string MesAno { get; private set; }
        public DateTime DataCadastro { get; private set; }

        protected Fatura() { }

        public Fatura(Cartao cartao, string mesAno)
        {
            Id = Guid.NewGuid();
            IdCartao = cartao.Id;
            MesAno = mesAno;
            DataCadastro = DateTime.Now;
        }

        public Cartao Cartao { get; set; }

        public IEnumerable<LancamentoFatura> Lancamentos { get; set; }
    }
}
