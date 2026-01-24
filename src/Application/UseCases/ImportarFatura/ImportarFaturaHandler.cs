using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.ImportarFatura
{
    public class ImportarFaturaCommand : IRequest<ImportarFaturaResult>
    {
        public TipoFatura Tipo { get; }
        public Stream Arquivo { get; }

        public ImportarFaturaCommand(Stream arquivo, TipoFatura tipo)
        {
            Arquivo = arquivo;
            Tipo = tipo;
        }
    }

    public class ImportarFaturaHandler
        : IRequestHandler<ImportarFaturaCommand, ImportarFaturaResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFaturaParserFactory _factory;
        private readonly ILancamentoCartaoRepository _repository;

        public ImportarFaturaHandler(
            IFaturaParserFactory factory,
            ILancamentoCartaoRepository repository,
            IUnitOfWork uow)
        {
            _factory = factory;
            _repository = repository;
            _uow = uow;
        }

        public async Task<ImportarFaturaResult> Handle(ImportarFaturaCommand request, CancellationToken cancellationToken)
        {
            var parser = _factory.Create(request.Tipo);
            var lancamentos = parser.Parse(request.Arquivo);

            await _repository.AddRangeAsync(lancamentos);
            await _uow.CommitAsync();

            return new ImportarFaturaResult(lancamentos);
        }
    }
}
