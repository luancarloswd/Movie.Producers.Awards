using Microsoft.EntityFrameworkCore;
using Movie.Producers.Awards.Domain;
using Movie.Producers.Awards.Infra.Abstractions;
using Movie.Producers.Awards.Infra.Configurations;
using Movie.Producers.Awards.Infra.DataSeed;

namespace Movie.Producers.Awards.Infra;

public class ApiContext : DbContext
{
    private readonly IMoviesDataSeedStream _moviesDataSeedStream;

    public ApiContext(DbContextOptions<ApiContext> options, IMoviesDataSeedStream moviesDataSeedStream)
        : base(options)
    {
        _moviesDataSeedStream = moviesDataSeedStream;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieAwardsConfiguration());
        base.OnModelCreating(modelBuilder);
    }


    public DbSet<MovieAwards> MoviesAwards { get; set; }
}