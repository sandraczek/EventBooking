using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBooking.Infrastructure.Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(150);
        
        builder.HasIndex(e => e.Name)
            .IsUnique();

        builder.Property(e => e.Description)
            .HasMaxLength(2000);
        
        builder.Property(e => e.TicketPrice)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}