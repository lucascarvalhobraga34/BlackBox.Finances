using Domain.Enums;

namespace Api.Contracts
{
    public class ImportarFaturaRequest
    {
        public string MesAno { get; set; }
        public CodigoBanco CodigoBanco { get; set; }
        public IFormFile Arquivo { get; set; } = default!;
    }
}
