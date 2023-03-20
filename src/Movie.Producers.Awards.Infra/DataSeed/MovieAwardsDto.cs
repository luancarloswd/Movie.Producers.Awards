using CsvHelper.Configuration.Attributes;
namespace Movie.Producers.Awards.Infra.Data;

public class MovieDto
{
    [Name("year")]
    public string? Year { get; set; }
    [Name("title")]
    public string? Title { get; set; }
    [Name("studios")]
    public string? Studio { get; set; }
    [Name("producers")]
    public string? Producer { get; set; }
    [Name("winner")]
    public string? Winner { get; set; }

    private int TryGetYear()
    {
        int.TryParse(Year, out var year);
        return year;
    }
    
    private bool TryGetWinner() => Winner?.Equals("yes", StringComparison.InvariantCultureIgnoreCase) ?? false;

    public Domain.MovieAwards ToDomain() => new()
    {
        Year = TryGetYear(),
        Producer = Producer!,
        Studio = Studio!,
        Title = Title!,
        Winner = TryGetWinner(),
    };

}