namespace Movie.Producers.Awards.Domain;

public class MoviesAwardsIntervals
{
    public MoviesAwardsIntervals() { }
    public MoviesAwardsIntervals(IEnumerable<MovieAwards> moviesAwards)
    {
        var awardsInterval = moviesAwards.GroupBy(ma => ma.Producer)
            .Select(g =>
            {
                var followingWin = g.OrderByDescending(ma => ma.Year).First().Year;
                var previousWin = g.Where(ma=> ma.Year != followingWin).MaxBy(ma => ma.Year)?.Year ?? followingWin;
                return new MovieAwardsInterval
                {
                    Producer = g.First().Producer,
                    PreviousWin = previousWin,
                    FollowingWin = followingWin,
                };
            }).ToList();

        var maxInterval = awardsInterval.MaxBy(ma => ma.Interval)?.Interval ?? default;
        var minInterval = awardsInterval.Where(ma=> ma.Interval > 0).MinBy(ma => ma.Interval)?.Interval ?? default;

        Max = awardsInterval.Where(ai => ai.Interval == maxInterval).ToList();
        Min = awardsInterval.Where(ai => ai.Interval == minInterval).ToList();

    }
    public IEnumerable<MovieAwardsInterval> Min { get; set; }   
    public IEnumerable<MovieAwardsInterval> Max { get; set; }   
}