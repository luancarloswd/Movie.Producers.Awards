using Movie.Producers.Awards.Application.Output;
using Movie.Producers.Awards.Domain;

namespace Movie.Producers.Awards.Application.Abstractions;

public interface IMovieAwardsApplication
{
    Task UpdateMovieAwardsAsync(Stream stream, CancellationToken cancellationToken = default);
    Task<DefaultOutput<MoviesAwardsIntervals>> GetIntervalMoviesAwards(CancellationToken cancellationToken = default);
    Task SeedDataAsync(CancellationToken cancellationToken = default);

}