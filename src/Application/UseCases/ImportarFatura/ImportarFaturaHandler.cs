using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.UseCases.ImportarFatura
{
    public class ImportarFaturaCommand : IRequest<Result<ImportarFaturaResult>>
    {
        public string MesAno { get; set; }
        public CodigoBanco CodigoBanco { get; }
        public Stream Arquivo { get; }

        public ImportarFaturaCommand(string mesAno, CodigoBanco codigoBanco, Stream fatura)
        {
            MesAno = mesAno;
            Arquivo = fatura;
            CodigoBanco = codigoBanco;
        }
    }

    public class ImportarFaturaHandler
        : IRequestHandler<ImportarFaturaCommand, Result<ImportarFaturaResult>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFaturaParserFactory _factory;
        private readonly ILancamentoFaturaRepository _lancamentoRepository;
        private readonly IFaturaRepository _faturaRepository;
        private readonly ICartaoRepository _cartaoRepository;

        private readonly IValidator<ImportarFaturaCommand> _validator;

        public ImportarFaturaHandler(
            IFaturaParserFactory factory,
            ILancamentoFaturaRepository lancamentoRepository,
            IValidator<ImportarFaturaCommand> validator,
            IFaturaRepository faturaRepository,
            ICartaoRepository cartaoRepository,
            IUnitOfWork uow)
        {
            _factory = factory;
            _faturaRepository = faturaRepository;
            _cartaoRepository = cartaoRepository;
            _lancamentoRepository = lancamentoRepository;
            _validator = validator;
            _uow = uow;
        }

        public async Task<Result<ImportarFaturaResult>> Handle(
            ImportarFaturaCommand command,
            CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(command, cancellationToken);

            if (!validation.IsValid)
                return Result<ImportarFaturaResult>.Fail(validation.Errors.Select(e => e.ErrorMessage));

            var idUsuario = Guid.Parse("32BB6142-177B-4258-863C-91504C908514");

            var cartao = await ValidarCartaoAsync(command, idUsuario);
            var fatura = await ValidarFaturaAsync(cartao);

            if (fatura is null)
            {
                return await CriarFaturaAsync(command, cartao);
            }

            return await AtualizarFaturaJaExistenteAsync(command, fatura);
        }

        private async Task<Result<ImportarFaturaResult>> CriarFaturaAsync(ImportarFaturaCommand command, Cartao? cartao)
        {
            var fatura = new Fatura(cartao!, command.MesAno);

            var lancamentosLidos = LerArquivo(command, fatura);

            await _faturaRepository.AddAsync(fatura);
            await _lancamentoRepository.AddRangeAsync(lancamentosLidos);
            await _uow.CommitAsync();

            return Result<ImportarFaturaResult>.Ok(new ImportarFaturaResult(lancamentosLidos));
        }

        private async Task<Result<ImportarFaturaResult>> AtualizarFaturaJaExistenteAsync(ImportarFaturaCommand command, Fatura fatura)
        {
            var lancamentosLidos = LerArquivo(command, fatura);

            var lancamentosDoMes = await _lancamentoRepository.FindAsync(x => x.IdFatura == fatura.Id);

            var lancamentosExistentes = lancamentosDoMes
                .Select(x => (x.DataLancamento, x.Descricao, x.Valor))
                .ToHashSet();

            var novosLancamentos = lancamentosLidos
                .Where(x => !lancamentosExistentes.Contains(
                    (x.DataLancamento, x.Descricao, x.Valor)))
                .ToList();

            await _lancamentoRepository.AddRangeAsync(novosLancamentos);
            await _uow.CommitAsync();

            return Result<ImportarFaturaResult>.Ok(new ImportarFaturaResult(lancamentosLidos));
        }

        private async Task<Fatura?> ValidarFaturaAsync(Cartao? cartao)
        {
            return (await _faturaRepository.FindAsync(f => f.IdCartao == cartao.Id)).FirstOrDefault();
        }

        private async Task<Cartao?> ValidarCartaoAsync(ImportarFaturaCommand command, Guid idUsuario)
        {
            return (await _cartaoRepository.FindAsync(c => c.Banco.Codigo == command.CodigoBanco.GetDescription() && c.IdUsuario == idUsuario)).FirstOrDefault();
        }

        private List<LancamentoFatura> LerArquivo(ImportarFaturaCommand command, Fatura? fatura)
        {
            var parser = _factory.Create(command.CodigoBanco);
            var lancamentosLidos = parser.Parse(fatura, command.MesAno, command.Arquivo).ToList();
            return lancamentosLidos;
        }
    }
}