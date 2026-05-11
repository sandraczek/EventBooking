using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBooking.Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(s => s.LastName).IsRequired().HasMaxLength(50);
        builder.Property(s => s.IndexNumber).IsRequired().HasMaxLength(6);
        
        builder.HasIndex(s => s.IndexNumber).IsUnique();
        
        builder.HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Student>(s => s.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}