using Api.Contracts;
using Application.UseCases.ImportarFatura;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/faturas")]
    public class FaturasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FaturasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("importar")]
        public async Task<IActionResult> Importar(
            [FromForm] ImportarFaturaRequest request)
        {
            using var arquivoFatura = request.Arquivo.OpenReadStream();

            var command = new ImportarFaturaCommand(request.MesAno, request.CodigoBanco, arquivoFatura);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors});

            return Ok(result.Value);
        }
    }
}
