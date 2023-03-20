using Movie.Producers.Awards.Domain;

namespace Movie.Producers.Awards.Infra.Abstractions;

public interface IMovieAwardsRepository
{
    Task RemoveAllAsync(CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<MovieAwards> moviesAwards, CancellationToken cancellationToken = default);
    Task<IEnumerable<MovieAwards>> GetMoviesAwards(CancellationToken cancellationToken = default);
}