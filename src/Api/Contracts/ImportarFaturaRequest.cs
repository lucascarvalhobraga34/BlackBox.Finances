using Domain.Enums;

namespace Api.Contracts
{
    public class ImportarFaturaRequest
    {
        public TipoFatura Tipo { get; set; }
        public IFormFile Arquivo { get; set; } = default!;
    }
}
