using HoffTestTask.Application.Queries.Exchanges.GetExchangeByCoordinates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HoffTestTask.Host.Extensions;
using HoffTestTask.Infrastructure.Results;

namespace HoffTestTask.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeController : Controller
{
    private readonly IMediator _mediator;

    public ExchangeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get exchange by coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Result</returns>
    [HttpGet("coordinates/{x}/{y}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<decimal>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result<decimal>))]
    public async Task<IActionResult> GetExchangeByCoordinates(int x, int y,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetExchangeByCoordinatesQuery { X = x, Y = y }, cancellationToken);
        return this.Response(result);
    }
}