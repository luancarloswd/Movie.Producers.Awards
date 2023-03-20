using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movie.Producers.Awards.Infra.Abstractions;
using Movie.Producers.Awards.Infra.DataSeed;
using Movie.Producers.Awards.Infra.Repositories;

namespace Movie.Producers.Awards.Infra;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("MovieAwards"), ServiceLifetime.Singleton)
            .AddSingleton<IMovieAwardsRepository, MovieAwardsRepository>()
            .AddSingleton<IMoviesDataSeedStream, MoviesDataSeedStream>();
    }

}