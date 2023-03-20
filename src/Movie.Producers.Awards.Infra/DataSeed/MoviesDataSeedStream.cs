using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using Movie.Producers.Awards.Infra.Abstractions;
using Movie.Producers.Awards.Infra.Data;

namespace Movie.Producers.Awards.Infra.DataSeed;

public class MoviesDataSeedStream : IMoviesDataSeedStream
{
    
    public IEnumerable<Domain.MovieAwards>? GetMoviesFromStream(Stream? stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture){Delimiter = ";"});
        var records = csv.GetRecords<MovieDto>().ToList();
        return records?.Select(r => r.ToDomain());
    }

    public async Task<IEnumerable<Domain.MovieAwards>?> GetDefaultMoviesAwardsAsync()
    {
        var assembly = Assembly.GetExecutingAssembly();
        const string resourceName = "Movie.Producers.Awards.Infra.DataSeed.movielist.csv";
        await using var stream = assembly.GetManifestResourceStream(resourceName);
        var moviesAwards = GetMoviesFromStream(stream);
        return moviesAwards.Select((ma, i) =>
        {
            ma.Id = i + 1;
            return ma;
        });
    }
}