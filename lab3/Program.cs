using System.Security.Claims;
using System.Text;
using lab3.Data.Context;
using lab3.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("StudentsDb");
builder.Services.AddDbContext<StudentsContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddIdentity<Student, IdentityRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<StudentsContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Std";
    options.DefaultChallengeScheme = "Std";
})
    .AddJwtBearer("Std", options =>
    {
        var secretKeyString = builder.Configuration.GetValue<string>("SecretKey") ?? "";
        var secretKyInBytes = Encoding.ASCII.GetBytes(secretKeyString);
        var securityKey = new SymmetricSecurityKey(secretKyInBytes);

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = securityKey,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.RequireClaim(ClaimTypes.Role, "User", "Admin"));

    options.AddPolicy("AllowAdminsOnly",
        builder => builder.RequireClaim(ClaimTypes.Role, "Admin"));

    options.AddPolicy("AllowUsersOnly",
        builder => builder.RequireClaim(ClaimTypes.Role, "User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
