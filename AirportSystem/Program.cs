
using AirportSystem.Data;
using AirportSystem.Service.Interface;
using AirportSystem.Service;
using AirportSystem.Validator.Interface;
using AirportSystem.Validator;
using AirportSystem.API;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AirportDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IAirportDataService, AirportDataService>();
            builder.Services.AddScoped<AirportDataService>();
            builder.Services.AddScoped<ICountryDataService, CountryDataService>();
            builder.Services.AddScoped<IRouteDataService, RouteDataService>();
            builder.Services.AddScoped<IAirportGroupDataService, AirportGroupDataService>();
            builder.Services.AddScoped<AirportGroupDataService>();
            builder.Services.AddScoped<IRouteValidator, RouteValidator>();
            builder.Services.AddScoped<IAirportGroupValidator, AirportGroupValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Add minimal Api endpoints
            app.MapAirportEndpoints();
            app.MapCountryEndpoints();
            app.MapRouteEndpoints();

            app.Run();
        }
    }
}
