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
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(r => r.Event)
            .WithMany()
            .HasForeignKey(r => r.EventId)
            .IsRequired();

        builder.HasIndex(r => new {r.EventId, StudentId = r.UserId })
        .IsUnique();
        
        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>();
    }
}