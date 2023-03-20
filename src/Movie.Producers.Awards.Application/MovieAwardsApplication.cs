using Movie.Producers.Awards.Application.Abstractions;
using Movie.Producers.Awards.Application.Output;
using Movie.Producers.Awards.Domain;
using Movie.Producers.Awards.Infra.Abstractions;
using Movie.Producers.Awards.Infra.DataSeed;

namespace Movie.Producers.Awards.Application;

public class MovieAwardsApplication : IMovieAwardsApplication
{
    private readonly IMovieAwardsRepository _movieAwardsRepository;
    private readonly IMoviesDataSeedStream _moviesDataSeedStream;

    public MovieAwardsApplication(IMovieAwardsRepository movieAwardsRepository, IMoviesDataSeedStream moviesDataSeedStream)
    {
        _movieAwardsRepository = movieAwardsRepository;
        _moviesDataSeedStream = moviesDataSeedStream;
    }

    public async Task UpdateMovieAwardsAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        var moviesAwards = _moviesDataSeedStream.GetMoviesFromStream(stream);
        await _movieAwardsRepository.RemoveAllAsync(cancellationToken);
        await _movieAwardsRepository.AddRangeAsync(moviesAwards, cancellationToken);
    }

    public async Task<DefaultOutput<MoviesAwardsIntervals>> GetIntervalMoviesAwards(CancellationToken cancellationToken = default)
    {
        var moviesAwards = await _movieAwardsRepository.GetMoviesAwards(cancellationToken);
      
        var intervals = new MoviesAwardsIntervals(moviesAwards);
        return new DefaultOutput<MoviesAwardsIntervals>(intervals);
    }

    public async Task SeedDataAsync(CancellationToken cancellationToken = default)
    {
        var moviesAwards= await _moviesDataSeedStream.GetDefaultMoviesAwardsAsync();
        if (moviesAwards != null) await _movieAwardsRepository.AddRangeAsync(moviesAwards, cancellationToken);
    }
}