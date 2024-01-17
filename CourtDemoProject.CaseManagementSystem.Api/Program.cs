using CourtDemoProject.CaseManagementSystem.Api.Services;
using CourtDemoProject.CaseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
builder.Services.AddDbContext<CaseManagementSystemDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<CaseDetailService>();
builder.Services.AddScoped<CaseParticipantService>();
builder.Services.AddScoped<CaseService>();
builder.Services.AddScoped<ChargeService>();

var app = builder.Build();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<CaseManagementSystemDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();