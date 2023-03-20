using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Producers.Awards.Domain;

namespace Movie.Producers.Awards.Infra.Configurations;

public class MovieAwardsConfiguration : IEntityTypeConfiguration<MovieAwards>
{
    public void Configure(EntityTypeBuilder<MovieAwards> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.Producer);
        builder.Property(c => c.Title);
        builder.Property(c => c.Year);
        builder.Property(c => c.Studio);
        builder.Property(c => c.Winner);
    }
}