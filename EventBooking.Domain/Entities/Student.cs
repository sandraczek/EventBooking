using System;

namespace EventBooking.Domain.Entities;

public class Student
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string IndexNumber { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    
    private Student() { }
    
    public static Student Create(string email, string passwordHash, string firstName, string lastName, string indexNumber)
    {
        if (string.IsNullOrWhiteSpace(email)) 
            throw new ArgumentException("Email is required.", nameof(email));
            
        if (string.IsNullOrWhiteSpace(passwordHash)) 
            throw new ArgumentException("Password hash is required.", nameof(passwordHash));

        return new Student
        {
            Id = Guid.NewGuid(),
            Email = email.ToLowerInvariant(),
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            IndexNumber = indexNumber,
            CreatedAt = DateTime.UtcNow
        };
    }
}