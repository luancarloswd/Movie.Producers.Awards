using System.Net.Http.Headers;
using System.Text;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Movie.Producers.Awards.Domain;

namespace MovieProducers.Awards.IntegrationTests.Fixture;

public class AwardsFixture : WebApplicationFactory<Movie.Producers.Awards.Api.Program>
{
    public IFixture Fixture = new AutoFixture.Fixture();

    public MultipartFormDataContent BuildContentCsv(IEnumerable<MovieAwards> movieAwards)
    {
        var header = "year;title;studios;producers;winner";
        var body = movieAwards.Select(ma => $"{ma.Year};{ma.Title};{ma.Studio};{ma.Producer};{(ma.Winner ? "yes" : "")}")
            .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");

        var csvText = $"{header}{Environment.NewLine}{body}";
        var bytes = Encoding.ASCII.GetBytes(csvText);
        var fileContent = new ByteArrayContent(bytes);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/csv");
        
        var content = new MultipartFormDataContent();
        content.Add(fileContent, "csv", "test.csv");  
        return content;
    }
}