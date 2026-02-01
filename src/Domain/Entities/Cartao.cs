namespace Domain.Entities
{
    public class Cartao
    {
        public Guid Id { get; private set; }
        public Guid IdUsuario { get; private set; }
        public Guid IdBanco { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Descricao { get; private set; }

        protected Cartao() { }

        public Usuario Usuario { get; set; }

        public Banco Banco { get; set; }

        public IEnumerable<Fatura> Faturas { get; set; }
    }
}
