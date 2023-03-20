using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Movie.Producers.Awards.Application.Output;
using Movie.Producers.Awards.Domain;
using Movie.Producers.Awards.Infra.Abstractions;
using MovieProducers.Awards.IntegrationTests.Fixture;

namespace MovieProducers.Awards.IntegrationTests;

public class AwardsTests : IClassFixture<AwardsFixture>
{
    private readonly AwardsFixture _fixture;
    private const string BaseAddress = "/api/v1/Awards";

    public AwardsTests(AwardsFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Given_Get_Intervals_Should_Return_From_MovieAwards_Stored()
    {
        // Arrange
        var client = _fixture.CreateClient();
        
        // Act
        var response = await client.GetAsync($"{BaseAddress}/intervals");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var awardsIntervals = JsonSerializer.Deserialize<DefaultOutput<MoviesAwardsIntervals>>(content, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        awardsIntervals.Content.Min.Should().HaveCount(2);
        awardsIntervals.Content.Max.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task Given_Put_Awards_Should_Update_On_Database()
    {
        // Arrange
        var client = _fixture.CreateClient();
        var movieAwards = _fixture.Fixture.CreateMany<MovieAwards>();
        var httpContent = _fixture.BuildContentCsv(movieAwards);
        var repository = _fixture.Services.GetRequiredService<IMovieAwardsRepository>();

        // Act
        var response = await client.PutAsync($"{BaseAddress}", httpContent);

        // Assert
        response.EnsureSuccessStatusCode();

        var fromDb = await repository.GetMoviesAwards();
        fromDb.Should().OnlyContain(maDb=> movieAwards.Any(ma => 
            ma.Producer == maDb.Producer
            && ma.Title == maDb.Title
            && ma.Studio == maDb.Studio
            && ma.Winner == maDb.Winner));
    }
}