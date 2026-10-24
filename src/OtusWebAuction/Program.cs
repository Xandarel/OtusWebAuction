
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using OtusWebAuction.BL.Interfaces;
using OtusWebAuction.BL.Users;
using OtusWebAuction.DB.DI;
using OtusWebAuction.DB.Options;
using OtusWebAuction.DB.Repositories;
using OtusWebAuction.Options;
using OtusWebAuction.Security;

namespace OtusWebAuction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Host.ConfigureServices((context, services) =>
            {
                services.AddControllers();
                services.AddOpenApi();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Order Service API",
                        Version = "v1",
                        Description = "A REST API with Swagger for the Indotalent order service."
                    });
                });

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // указывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,
                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,
                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });

                services.AddTransient<IUserRepository, UserRepository>();
                services.AddTransient<UserHandler>();
                services.AddTransient<IPasswordHasher, UserPasswordHasher>();
                services.AddTransient<JwtTokenFactory>();
                services.AddOptions<PostgresOptions>().BindConfiguration(PostgresOptions.SectionName);
                PostgresOptions postgres = context.Configuration
                    .GetSection(PostgresOptions.SectionName)
                    .Get<PostgresOptions>()
                    ?? throw new InvalidOperationException("Section 'Postgres' is missing.");
                services.AddDatabase(postgres.BuildConnectionString());
            });

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service v1"));

            app.Run();
        }
    }
}
