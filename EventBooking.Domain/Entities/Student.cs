namespace EventBooking.Domain.Entities;

public class Student
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string IndexNumber { get; private set; } = null!;
    
    // AGH email convention -----------
    public string UniversityEmail => $"{IndexNumber}@student.agh.edu.pl";
    
    private Student() { }
    
    public static Student Create(Guid userId, string firstName, string lastName, string indexNumber)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId is required and cannot be empty.", nameof(userId));
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.", nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.", nameof(lastName));
        if (string.IsNullOrWhiteSpace(indexNumber))
            throw new ArgumentException("Index number is required.", nameof(indexNumber));

        return new Student
        {
            Id = userId,
            FirstName = firstName,
            LastName = lastName,
            IndexNumber = indexNumber
        };
    }
}