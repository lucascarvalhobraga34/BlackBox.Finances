namespace Domain.Entities
{
    public class Banco
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Codigo { get; private set; }
        public DateTime DataCadastro { get; private set; }

        protected Banco() { }

        public IEnumerable<Cartao> Cartoes { get; private set; }
    }
}
