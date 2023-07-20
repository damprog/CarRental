using CarRental.Core.Repositories;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Repositories;
using CarRental.Infrastructure.Services;
using CarRental.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddSingleton<IJwtHandler, JwtHandler>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("jwt"));

// Add connection to database
builder.Services.AddDbContext<CarRentalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarRentalCS")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// For jwt authentication
var jwtSetting = app.Services.GetService<JwtSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            /* 
             * Example:
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = "YourIssuer", // Zmieñ na swoj¹ nazwê/nazwê us³ugi, która generuje tokeny JWT
             ValidAudience = "YourAudience", // Zmieñ na swoj¹ nazwê/nazwê odbiorcy, który u¿ywa tokenów JWT
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey")) // Zmieñ na swoje tajne has³o u¿ywane do generowania i weryfikowania tokenów*/
            ValidIssuer = jwtSetting.Issuer,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key))
        };
    }); 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // for jwt authentication

app.UseAuthorization();

app.MapControllers();

app.Run();
