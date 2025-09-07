# DotNetCoreThreeTier.Template

A clean and extensible **Three-Tier Architecture template** built with **.NET Core 8**, designed for maintainability, scalability, and easy integration of SQL or MongoDB persistence.

---

## 🏗️ Architecture Diagram

```
       ┌───────────────────┐
       │      WebApis      │
       │   (Controllers)   │
       │    Program.cs     │
       └─────────┬─────────┘
                 │
                 ▼
       ┌───────────────────┐
       │    Application    │
       │  (Services / DI)  │
       └─────────┬─────────┘
                 │
                 ▼
       ┌───────────────────┐
       │  Infrastructure   │
       │-------------------│
       │ - SQL DbContext   │
       │ - MongoDbContext  │
       │ - Repositories    │
       └─────────┬─────────┘
                 │
                 ▼
       ┌───────────────────┐
       │       Core        │
       │ Entities / DTOs   │
       │ Contracts         │
       └───────────────────┘
```

**Key point:**  
- Switching between **SQL and MongoDB** happens only in the **Infrastructure layer DI registration**.  
- **WebApi** and **Application** layers remain unchanged.  
- Application services and controllers depend only on **Core contracts**, not the actual database.

---

## 📂 Project Structure

```
DotNetCoreThreeTier.Template/
│
├── DotNetCoreThreeTier.Core/           # Domain layer
│   ├── Contracts/                      # Interfaces (IRepository, IUserRepository)
│   ├── Dtos/                           # Data transfer objects (ServiceResponse.cs)
│   ├── Entities/                       # Domain entities (User.cs)
│   └── Exceptions/                     # Custom exceptions
│
├── DotNetCoreThreeTier.Application/    # Application layer
│   ├── Users/Implementations/          # Service implementations (UserService.cs)
│   ├── DependencyInjection.cs          # Registers services with DI container
│
├── DotNetCoreThreeTier.Infrastructure/ # Infrastructure layer
│   ├── Persistence/
│   │   ├── SQL/                        # SQL DbContext, Repositories, Migrations
│   │   └── Mongo/                      # MongoDbContext, Repositories, Collections
│   ├── DependencyInjection.cs          # Registers repositories and persistence services
│
├── DotNetCoreThreeTier.WebApis/        # Presentation layer (ASP.NET Core Web API)
│   ├── Controllers/                    # API controllers (UserController.cs)
│   ├── Program.cs                       # Application startup
│   ├── appsettings.json                 # Configuration file
│   └── Properties/launchSettings.json   # Debug/run settings
│
├── DotNetCoreThreeTier.sln             # Solution file
└── .gitignore
```

---

## ⚙️ Dependency Injection & Layer Injection

### WebApi (Program.cs)

```csharp
var builder = WebApplication.CreateBuilder(args);

// Infrastructure choice (switch here only)
builder.Services.AddInfrastructure(builder.Configuration); // SQL or MongoDB
builder.Services.AddApplication();                          // No change needed

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

### Application Layer DI

```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}
```

### Infrastructure Layer DI (SQL example)

```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SqlDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
```

### Infrastructure Layer DI (MongoDB example)

```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
        services.AddSingleton(mongoSettings);
        services.AddScoped<MongoDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
```

---

## 📝 Example Usage

### UserService (Application Layer)

```csharp
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ServiceResponse<User>> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return new ServiceResponse<User>(user != null, user, user == null ? "User not found" : null);
    }
}
```

### UserController (WebApis Layer)

```csharp
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var response = await _userService.GetUserByIdAsync(id);
        if (!response.Success) return NotFound(response.Message);
        return Ok(response.Data);
    }
}
```

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet)
- SQL Server or MongoDB
- Visual Studio 2022 / VS Code

### Setup & Run
```bash
git clone https://github.com/karl130757/DotNetCoreThreeTier.Template.git
cd DotNetCoreThreeTier.Template

dotnet restore
dotnet build
cd DotNetCoreThreeTier.WebApis
dotnet run
```

Access API at: `https://localhost:5001/swagger`

---

## 🧪 Testing

```bash
dotnet test
```

---

## 🤝 Contributing

1. Fork the repository  
2. Create a feature branch (`git checkout -b feature/your-feature`)  
3. Commit your changes (`git commit -m 'Add new feature'`)  
4. Push the branch (`git push origin feature/your-feature`)  
5. Open a Pull Request  

---

## 📜 License

MIT License. See [LICENSE](LICENSE) for details.

