using System.Text;
using BookMarketApi.BLL.Contracts.AuthContracts;
using BookMarketApi.BLL.Contracts.CartContracts;
using BookMarketApi.BLL.Contracts.OnlineBookContracts;
using BookMarketApi.BLL.Logic.AuthImplementations;
using BookMarketApi.BLL.Logic.CartImplementations;
using BookMarketApi.BLL.Logic.OnlineBookImplementations;
using BookMarketApi.Common.Automapper.AutoMapperConfig;
using BookMarketApi.DAL.Contracts.AuthContracts;
using BookMarketApi.DAL.Contracts.CartContracts;
using BookMarketApi.DAL.Contracts.OnlineBookContracts;
using BookMarketApi.DAL.Repositories;
using BookMarketApi.DAL.Repositories.AuthImplementations;
using BookMarketApi.DAL.Repositories.CartImplementations;
using BookMarketApi.DAL.Repositories.OnlineBookImplementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddAutoMapper(typeof(BookMapperProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"];
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddScoped<ICartContract, CartImplementation>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddScoped<IAuthContract, AuthImplementation>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<IOnlineBookRepository, OnlineBookRepository>();
builder.Services.AddScoped<IOnlineBookContract, OnlineBookImplementation>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
