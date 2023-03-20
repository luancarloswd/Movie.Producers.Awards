namespace Movie.Producers.Awards.Infra.Abstractions;

public interface IMoviesDataSeedStream
{
    Task<IEnumerable<Domain.MovieAwards>?> GetDefaultMoviesAwardsAsync();
    IEnumerable<Domain.MovieAwards>? GetMoviesFromStream(Stream? reader);
}