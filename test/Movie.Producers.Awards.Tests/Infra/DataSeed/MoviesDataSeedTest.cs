using FluentAssertions;
using Movie.Producers.Awards.Infra.DataSeed;

namespace Movie.Producers.Awards.Tests.Infra.DataSeed;

public class MoviesDataSeedTest
{
    [Fact]
    public async Task Given_GetMovies_Should_Load_Movies_From_Csv()
    {
        // Arrange
        var dataSeed = new MoviesDataSeedStream();
        const int moviesExpected = 206;
        
        // Act
        var movies = await dataSeed.GetDefaultMoviesAwardsAsync();

        // Assert
        movies.Should().HaveCount(moviesExpected);
    }
}