using GameDay.API.DbContext;
using GameDay.API.Extensions;
using GameDay.API.Models;
using GameDay.API.Repositories;
using GameDay.API.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequiredLength = 9;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<Game_Day_DBContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllers()
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling
    = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

builder.Services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

builder.Services.AddAuth(jwtSettings);

builder.Services.AddDbContext<Game_Day_DBContext>(options => options.UseSqlServer(
    configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuth();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
