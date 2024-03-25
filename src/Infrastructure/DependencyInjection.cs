using System.Text;
using Application.Contracts;
using Application.Contracts.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Service.Auth;
using Infrastructure.Service.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment env, IHostBuilder builder)
    {
        // Add Repositories, Services, etc.
        {
            // Add Services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            
            // Add Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserDocumentsRepository, UserDocumentsRepository>();
            services.AddScoped<ISimulationRepository, SimulationRepository>();
            services.AddScoped<ISchedulesRepository, SchedulesRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseLimitRepository, PurchaseLimitRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();
            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<IMerchantDocumentsRepository, MerchantDocumentsRepository>();
            services.AddScoped<ILegalDataRepository, LegalDataRepository>();
            services.AddScoped<ICardsRepository, CardsRepository>();
            services.AddScoped<IBankInfoRepository, BankInfoRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
        }
        
        // Database Configuration
        {
            var connectionString = configuration.GetConnectionString("ProjectLocalDbConnection");
            
            if (connectionString.IsNullOrEmpty())
            {
                throw new ArgumentException("Please specify Database connection string!");
            }

            services.AddDbContext<ProjectDbContext>(options => 
                options.UseNpgsql(connectionString)
            );
        }
        
        // Authentication Configuration
        {
            var tokenSettings = new TokenSettings();
            configuration.Bind(TokenSettings.SectionName, tokenSettings);
            
            if (new[] {
                        tokenSettings.AccessTokenSecret,
                        tokenSettings.RefreshTokenSecret,
                        tokenSettings.Issuer,
                        tokenSettings.Audience
                        }.Any(string.IsNullOrWhiteSpace)
                ) throw new ArgumentException("Please specify JWT details!");
            
            services.AddSingleton(Options.Create(tokenSettings));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenSettings.Issuer,
                    ValidAudience = tokenSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.AccessTokenSecret))
                };
            });
        }
        
        return services;

    }
}