using Application.UseCases.ImportarFatura;
using Domain.Enums;
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
            IFormFile arquivo,
            TipoFatura tipo)
        {
            if (arquivo is null || arquivo.Length == 0)
                return BadRequest("Arquivo inválido");

            using var stream = arquivo.OpenReadStream();

            var command = new ImportarFaturaCommand(stream, tipo);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
