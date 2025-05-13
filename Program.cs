
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TesteSeniors.Data;
using TesteSeniors.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TesteSeniors.Services;
using TesteSeniors.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace TesteSeniors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<UsuarioDbContext>(opt => opt.UseInMemoryDatabase("DbinMemory"));

            builder.Services
                .AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<UsuarioDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("Aquiseriaachavesecretadaempresa!123")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("IdadeMinima", policy => policy.AddRequirements(new IdadeMinima(18)));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
        }
    }
}
