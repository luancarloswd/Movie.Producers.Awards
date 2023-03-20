using Microsoft.AspNetCore.Mvc;
using Movie.Producers.Awards.Application.Abstractions;
using Movie.Producers.Awards.Infra.Abstractions;
using Movie.Producers.Awards.Infra.DataSeed;

namespace Movie.Producers.Awards.Api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AwardsController
{
    private readonly ILogger<AwardsController> _logger;
    private readonly IMoviesDataSeedStream _moviesDataSeedStream;
    private readonly IMovieAwardsApplication _movieAwardsApplication;

    public AwardsController(ILogger<AwardsController> logger, IMoviesDataSeedStream moviesDataSeedStream, IMovieAwardsApplication movieAwardsApplication)
    {
        _logger = logger;
        _moviesDataSeedStream = moviesDataSeedStream;
        _movieAwardsApplication = movieAwardsApplication;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutAsync(IFormFile csv)
    {
        await using var stream = csv.OpenReadStream();
        await _movieAwardsApplication.UpdateMovieAwardsAsync(stream);
        return new OkResult();
    }
    
    [HttpGet("intervals")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
    {
        var awardsInterval = await _movieAwardsApplication.GetIntervalMoviesAwards(cancellationToken);
        return new OkObjectResult(awardsInterval);
    }
}