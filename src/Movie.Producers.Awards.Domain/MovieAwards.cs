﻿namespace Movie.Producers.Awards.Domain;

public class MovieAwards
{
    public long Id { get; set; }
    public int Year { get; set; }
    public string Title { get; set; }
    public string Studio { get; set; }
    public string Producer { get; set; }
    public bool Winner { get; set; }
}