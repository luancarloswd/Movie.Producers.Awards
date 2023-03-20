using Microsoft.EntityFrameworkCore;
using Movie.Producers.Awards.Domain;
using Movie.Producers.Awards.Infra.Abstractions;

namespace Movie.Producers.Awards.Infra.Repositories;

public class MovieAwardsRepository : IMovieAwardsRepository
{
    private readonly ApiContext _apiContext;

    public MovieAwardsRepository(ApiContext apiContext) => _apiContext = apiContext;

    public async Task RemoveAllAsync(CancellationToken cancellationToken = default)
    {
        _apiContext.MoviesAwards.RemoveRange(_apiContext.MoviesAwards);
        await _apiContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<MovieAwards> moviesAwards, CancellationToken cancellationToken = default)
    {
        await _apiContext.MoviesAwards.AddRangeAsync(moviesAwards, cancellationToken);
        await _apiContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<MovieAwards>> GetMoviesAwards(CancellationToken cancellationToken = default) => 
        await _apiContext.MoviesAwards.ToListAsync(cancellationToken: cancellationToken);
}
