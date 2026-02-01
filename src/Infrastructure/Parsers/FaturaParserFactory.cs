using Application.Interfaces;
using Domain.Enums;
using Infrastructure.Parsers.Bmg;
using Infrastructure.Parsers.Nubank;

namespace Infrastructure.Parsers
{
    public class FaturaParserFactory : IFaturaParserFactory
    {
        public IFaturaParser Create(CodigoBanco tipo) =>
        tipo switch
        {
            CodigoBanco.Nubank => new NubankCsvParser(),
            CodigoBanco.BancoBmg => new BmgPdfParser(),
            _ => throw new NotSupportedException()
        };
    }
}
