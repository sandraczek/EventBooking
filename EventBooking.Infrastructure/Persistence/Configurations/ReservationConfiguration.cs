using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBooking.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(r => r.Id);
        
        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(r => r.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne<Event>()
            .WithMany()
            .HasForeignKey(r => r.EventId);

        builder.Property(r => r.EventId)
            .IsRequired();

        builder.Property(r => r.StudentId)
            .IsRequired();

        builder.HasIndex(r => new {r.EventId, r.StudentId })
        .IsUnique();
        
        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>();
    }
}