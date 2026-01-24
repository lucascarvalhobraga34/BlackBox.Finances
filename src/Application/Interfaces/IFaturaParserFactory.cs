using Domain.Enums;

namespace Application.Interfaces
{
    public interface IFaturaParserFactory
    {
        IFaturaParser Create(TipoFatura tipo);
    }
}
