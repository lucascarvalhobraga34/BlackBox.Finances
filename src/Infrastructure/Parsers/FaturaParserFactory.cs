using Application.Interfaces;
using Domain.Enums;
using Infrastructure.Parsers.Bmg;
using Infrastructure.Parsers.Nubank;

namespace Infrastructure.Parsers
{
    public class FaturaParserFactory : IFaturaParserFactory
    {
        public IFaturaParser Create(TipoFatura tipo) =>
        tipo switch
        {
            TipoFatura.Nubank => new NubankCsvParser(),
            TipoFatura.Bmg => new BmgPdfParser(),
            _ => throw new NotSupportedException()
        };
    }
}
