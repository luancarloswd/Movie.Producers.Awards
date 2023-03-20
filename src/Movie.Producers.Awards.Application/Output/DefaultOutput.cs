namespace Movie.Producers.Awards.Application.Output;

public class DefaultOutput<TContent>
{
    public DefaultOutput() { }
    public DefaultOutput(TContent content,params string[] errors)
    {
        Content = content;
        Errors = errors;
    }
    public TContent Content { get; set; }
    public IEnumerable<string> Errors { get; set; }
}