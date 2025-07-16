Resource Booking App

A simple ASP.NET Core MVC web application for managing and booking shared company resources such as meeting rooms, vehicles, or equipment.

---

Features

- Create, view, and manage resources
- Book available resources for specific time slots
- Prevent double-bookings with conflict detection
- User-friendly interface with Bootstrap styling
- Server-side and client-side form validation

---
Setup Instructions

Clone the Repository

bash
git clone https://github.com/khanyisile-cent/web-app.git
cd web-app
`

Open in Visual Studio Code

bash
code .


Configure the Database

* Open `appsettings.json`
* Replace the existing connection string with the following:

json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ResourceAndBookingDb;Trust Server Certificate=True;MultipleActiveResultSets=true; Integrated Security = true;"
}


> Ensure your local SQL Server instance is running.
> If using a named instance (like `SQLEXPRESS`), update `Server=localhost\\SQLEXPRESS`.

4. Apply Migrations & Seed Data

Run the following in the terminal:

bash
dotnet ef database update


You must have `Microsoft.EntityFrameworkCore.Tools` installed.

5. Run the App

bash
dotnet run


Then visit: [http://localhost:5000](http://localhost:5000)

---

Requirements

* [.NET SDK 7.0+](https://dotnet.microsoft.com/en-us/download)
* SQL Server (LocalDB or full version)
* Entity Framework Core

---

Project Structure


/Controllers     → MVC controllers for Resource and Booking logic  
/Models          → Data models for Entity Framework  
/Views           → Razor views for UI  
/Migrations      → EF Core database migrations  
/appsettings.json→ Configuration (e.g. connection strings)


---

Author

**Khanyisile Momoti**
Systems Analyst | Developer
[GitHub](https://github.com/khanyisile-cent)

---

License

This project is for educational use.

