
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
                        Description = "A REST API with Swagger for the Indotalent order service.",

                    });

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Description = "JWT из Login или RegisterUser"
                    });

                    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
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
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service v1");
                options.UseResponseInterceptor("""
                    function (response) {
                        try {
                            var url = response.url || "";
                            var isAuth = url.indexOf("Login") >= 0 || url.indexOf("RegisterUser") >= 0;
                            var raw = typeof response.text === "string" ? response.text : response.data;
                            if (response.ok && isAuth && typeof raw === "string") {
                                var payload = JSON.parse(raw);
                                var token = payload.accessToken || payload.AccessToken;
                                if (token && window.ui) {
                                    window.ui.authActions.authorize({
                                        Bearer: {
                                            name: "Bearer",
                                            schema: { type: "http", scheme: "bearer", bearerFormat: "JWT" },
                                            value: token
                                        }
                                    });
                                }
                            }
                        } catch (e) {}
                        return response;
                    }
                    """);
            });

            app.Run();
        }
    }
}
