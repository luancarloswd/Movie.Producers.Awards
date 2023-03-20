using Microsoft.Extensions.DependencyInjection;
using Movie.Producers.Awards.Application.Abstractions;

namespace Movie.Producers.Awards.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplications(this IServiceCollection serviceCollection) => 
        serviceCollection.AddSingleton<IMovieAwardsApplication, MovieAwardsApplication>();
}