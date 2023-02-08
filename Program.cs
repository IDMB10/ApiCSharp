using System.Text;
using APIWeb.Data;
using APIWeb.Data.Interfaces;
using APIWeb.Mapper;
using APIWeb.Services;
using APIWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace APIWeb {
    public static class Program {
        private static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Agregando el servicio de SQL
            builder.Services.AddSqlServer<DataContext>(builder.Configuration.GetConnectionString("cnTareasWA"));

            //Agregando los servicios de los modelos
            builder.Services.AddScoped<IApiRepository, ApiRepository>();

            builder.Services.AddScoped<IAuthRepository, AuthRepository>();

            //Agregando servicio de autenticaciòn
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options => {
                    options.TokenValidationParameters = new TokenValidationParameters() {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
            );

            //Agregando Servicio de Token
            builder.Services.AddScoped<ITokenService, TokenService>();

            //Agregando Automapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //Agregando middelware de Autenticación
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}