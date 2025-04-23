
using System;
using System.Text;
using System.Text.Json.Serialization;
using Common.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using R.BAL.Services.Implementation;
using R.BAL.Services.Interface;
using R.DAL.Context;
using R.DAL.Repositories.Implementation;
using R.DAL.Repositories.Interface;



namespace NTireRestaurant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IUserRepositories, UserRepositories>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IMenuRepositories, MenuRepositories>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddSwaggerGen();

            var jwtSettings = builder.Configuration.GetSection("Jwt");
            builder.Services.AddSingleton(new JWTService(
                jwtSettings["Key"],
                jwtSettings["Issuer"],
                jwtSettings["Audience"]));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                    };
                });

            builder.Services.AddDbContext<RestaurantDbContext>(options =>
            options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("R.DAL"))
);

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
        }
    }

  
}
