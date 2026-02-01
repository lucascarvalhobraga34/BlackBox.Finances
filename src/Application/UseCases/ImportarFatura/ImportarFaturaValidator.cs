using FluentValidation;

namespace Application.UseCases.ImportarFatura
{
    public class ImportarFaturaValidator : AbstractValidator<ImportarFaturaCommand>
    {
        public ImportarFaturaValidator()
        {
            RuleFor(x => x.MesAno)
                .NotEmpty()
                .WithMessage("MesAno da fatura (mm/yyyy) não informado.");

            RuleFor(x => x.Arquivo)
                .NotEmpty()
                .WithMessage("Arquivo da fatura não informado.");

            RuleFor(x => x.CodigoBanco)
                .NotEmpty()
                .WithMessage("Código Banco não informado.");
        }
    }
}
