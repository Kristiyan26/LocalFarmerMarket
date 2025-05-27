using LocalFarmerMarket.API.Services;
using LocalFarmerMarket.Data.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using LocalFarmerMarket.Data;
using Microsoft.EntityFrameworkCore;
using LocalFarmerMarket.Core.Models;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.FileProviders;

namespace LocalFarmerMarket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });
            builder.Services.AddEndpointsApiExplorer();



            builder.Services.AddDbContext<LocalFarmerMarketDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                       .UseLazyLoadingProxies();
            });


            builder.Services.AddScoped<ProductsRepository>();
            builder.Services.AddScoped<CategoriesRepository>();
            builder.Services.AddScoped<OrdersRepository>();
            builder.Services.AddScoped<CustomersRepository>();
            builder.Services.AddScoped<FarmersRepository>();

            builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

            builder.Services.AddScoped<JwtService>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                options.DefaultChallengeScheme =
                options.DefaultForbidScheme =
                options.DefaultScheme =
                options.DefaultSignInScheme =
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    options.MapInboundClaims = true;

                  

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimType = "azp",
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

                                        };
                  
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var hdr = context.Request.Headers["Authorization"].FirstOrDefault();
                            Console.WriteLine($"[Jwt] Received Authorization header: '{hdr}'");
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"[Jwt] Authentication FAILED: {context.Exception.GetType().Name} - {context.Exception.Message}");
                            Console.ResetColor();
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("[Jwt] Token VALIDATED successfully");
                            Console.ResetColor();
                            return Task.CompletedTask;
                        }
                    };
                });


            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                     new string[]{}
                    }
                });
            });

            var app = builder.Build();

 
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();  
            app.UseAuthorization();

            app.MapControllers();


            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/images"
            });


            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path}");
                    await next();
            });



            app.Run();
        }
    }
}