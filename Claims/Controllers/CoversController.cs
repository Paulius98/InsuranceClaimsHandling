using Claims.Application.Commands.Covers;
using Claims.Application.Queries.Covers;
using Claims.Domain.Enums;
using Claims.Models.Requests;
using Claims.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Claims.Controllers;

[ApiController]
[Route("[controller]")]
public class CoversController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CoversController> _logger;

    public CoversController(IMediator mediator, ILogger<CoversController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Compute Premium Cover
    /// </summary>
    /// <param name="startDate">Start Date of Cover</param>
    /// <param name="endDate">End Date of Cover</param>
    /// <param name="coverType">Cover Type</param>
    /// <returns></returns>
    [HttpGet("compute")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CoverPremiumResponseDto>> GetPremium(
        [FromQuery] DateTimeOffset startDate,
        [FromQuery] DateTimeOffset endDate,
        [FromQuery] CoverType coverType)
    {
        var premium = await _mediator.Send(new GetCoverPremiumQuery(startDate, endDate, coverType));
        return Ok(new CoverPremiumResponseDto { Premium = premium });
    }

    /// <summary>
    /// Get All Existing Covers
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CoverResponseDto>>> GetAllAsync()
    {
        var covers = await _mediator.Send(new GetCoversQuery());
        var results = covers.Select(CoverResponseDto.FromDomain);
        return Ok(results);
    }

    /// <summary>
    /// Get Existing Cover by Unique Id
    /// </summary>
    /// <param name="id">Unique Id of Cover</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CoverResponseDto>> GetByIdAsync(Guid id)
    {
        var cover = await _mediator.Send(new GetCoverByIdQuery(id));
        var result = CoverResponseDto.FromDomain(cover);
        return Ok(result);
    }

    /// <summary>
    /// Create a new Cover
    /// </summary>
    /// <param name="request">Cover to Add</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CoverResponseDto>> CreateAsync([FromBody] CoverRequestDto request)
    {
        var cover = await _mediator.Send(new CreateCoverCommand(
            request.StartDate,
            request.EndDate,
            request.Type));

        var result = CoverResponseDto.FromDomain(cover);
        _logger.LogInformation($"Cover was created. {result}");
        return Ok(result);
    }

    /// <summary>
    /// Delete Existing Cover
    /// </summary>
    /// <param name="id">Unique Id of the Cover</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _mediator.Publish(new DeleteCoverCommand(id));

        _logger.LogInformation($"Cover with {id} id was deleted");
        return NoContent();
    }
}
