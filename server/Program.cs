using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using server.DAL;
using server.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddControllers();


// Add authentication and 
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/api/{controller=Account}/{action=login}");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

// Add Db configurtion
builder.Services
    .AddDbContext<ApplicationContext>(options =>
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
        );
    })
    .AddTransient<IPasswordValidator<User>, PasswordValidator>(serv => new PasswordValidator())
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>();


var app = builder.Build();

app.UseCors(
    builder => builder.WithOrigins("http://localhost:3000")
);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = app.Services.GetRequiredService<ILogger<Program>>();

    //Seeding the Db
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DbDataInitializer.InitializeAsync(userManager, rolesManager, logger);
    }
    catch (Exception e)
    {
        logger.LogError(e, "An error occurred while seeding the database.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthentication();

app.MapControllers();

app.Run();
