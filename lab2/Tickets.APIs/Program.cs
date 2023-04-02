using Microsoft.EntityFrameworkCore;
using Tickets.BL;
using Tickets.BL.Dtos;
using Tickets.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("TicketsDb");
builder.Services.AddDbContext<TicketsContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDepartmentsRepo, DepartmentsRepo>();
builder.Services.AddScoped<ITicketsRepo, TicketsRepo>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITicketsManager, TicketsManager>();
builder.Services.AddScoped<IDepartmentsManager, DepartmentsManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
