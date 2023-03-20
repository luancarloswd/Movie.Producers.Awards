namespace Movie.Producers.Awards.Domain;

public class MovieAwardsInterval
{
    public string Producer { get; set; }
    public int PreviousWin { get; set; }
    public int FollowingWin { get; set; }
    public int Interval => FollowingWin - PreviousWin;
}