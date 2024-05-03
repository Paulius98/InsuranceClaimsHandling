using Claims.Application.Commands.Claims;
using Claims.Application.Queries.Claims;
using Claims.Models.Requests;
using Claims.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Claims.Controllers;

[ApiController]
[Route("api/v1/claims")]
public class ClaimsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ClaimsController> _logger;

    public ClaimsController(IMediator mediator, ILogger<ClaimsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get All Registered Claims
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ClaimResponseDto>>> GetAllAsync()
    {
        var claims = await _mediator.Send(new GetClaimsQuery());
        var results = claims.Select(ClaimResponseDto.FromDomain);
        return Ok(results);
    }

    /// <summary>
    /// Get Claim by Id
    /// </summary>
    /// <param name="id">Unique Id of the Claim</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ClaimResponseDto?>> GetAsync(Guid id)
    {
        var claim = await _mediator.Send(new GetClaimByIdQuery(id));
        var result = ClaimResponseDto.FromDomain(claim);
        return Ok(result);
    }

    /// <summary>
    /// Create a new Claim
    /// </summary>
    /// <param name="request">Claim to Add</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ClaimResponseDto>> CreateAsync([FromBody] ClaimRequestDto request)
    {
        var claim = await _mediator.Send(new CreateClaimCommand(
            request.CoverId,
            request.Created,
            request.Name, 
            request.Type, 
            request.DamageCost));

        var result = ClaimResponseDto.FromDomain(claim);

        _logger.LogInformation($"Claim was created. {result}");
        return Ok(result);
    }

    /// <summary>
    /// Delete Existing Claim
    /// </summary>
    /// <param name="id">Unique Id of Claim</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _mediator.Publish(new DeleteClaimCommand(id));

        _logger.LogInformation($"Claim with {id} id was deleted");
        return NoContent();
    }
}
