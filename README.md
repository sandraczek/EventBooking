 ------------ AGH WI Event Booking System ------------                  
A full-stack web application built for the Student Government (WRSS) at the Faculty of Computer Science (AGH University).

The main goal of this project is to handle event registrations (like the annual Faculty Ball), where limited tickets often sell out in a minute.
I built this to solve a specific problem: when hundreds of students click "Book" at exactly the same time,
standard CRUD operations can easily lead to database locks or overselling due to race conditions.

How it works under the hood:

Instead of writing directly to the database on every HTTP request, the backend uses a queue-based approach:
Incoming booking requests are validated and pushed to an in-memory queue (Channel<T>).
A Background Worker processes this queue sequentially.
This completely eliminates race conditions—available spots are updated safely one by one.
The system uses CQRS (via MediatR) to separate the booking logic from simple data fetching.

 ---------- Tech Stack ----------

 - Backend - (main focus)

C# / .NET 8 (ASP.NET Core Web API)

PostgreSQL + Entity Framework Core

CQRS (MediatR)

Asynchronous Processing (System.Threading.Channels, BackgroundService)

SMTP (emails)


 - Frontend - (not main focus)

Angular (Standalone Components)

TypeScript

Reactive Forms (strictly typed to match backend DTOs)

Bootstrap 5 / SCSS


 --------- Current Features ---------

Asynchronous ticket booking system.

Real-time seat allocation with strict limit enforcement.

Student email confirmation.

