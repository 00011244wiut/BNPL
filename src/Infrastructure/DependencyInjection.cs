using System.Text;
using Application.Contracts;
using Application.Contracts.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Service.Auth;
using Infrastructure.Service.FetchApi;
using Infrastructure.Service.FileUpload;
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
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IFetchApi, FetchApi>();
            
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
        
        // file upload service configuration
        {
            var cloudinarySettings = new CloudinarySettings();
            configuration.Bind(CloudinarySettings.SectionName, cloudinarySettings);
            
            if (string.IsNullOrWhiteSpace(cloudinarySettings.CloudinaryUrl))
            {
                throw new ArgumentException("Please specify Cloudinary account details!");
            }
            
            services.AddSingleton(Options.Create(cloudinarySettings));
        }
        
        return services;

    }
}

/*
   DependencyInjection Class

   This class defines a static extension method named AddInfrastructureServices, which is intended to be used 
   for configuring dependency injection within the infrastructure layer of the application. It extends 
   IServiceCollection, provided by the Microsoft.Extensions.DependencyInjection namespace.

   - AddInfrastructureServices Method:
     This method is responsible for configuring and registering various infrastructure services, such as 
     repositories, database context, and authentication services, using the provided IServiceCollection 
     parameter. It is called during the setup phase of the application.

     - Parameters:
       - services (IServiceCollection): The IServiceCollection instance to which infrastructure services 
         are added.
       - configuration (IConfiguration): The application's configuration, used to retrieve connection 
         strings and JWT token settings.
       - env (IHostEnvironment): The hosting environment of the application.
       - builder (IHostBuilder): The host builder used to configure various aspects of the application.

     - Method Implementation:
       1. Adding Services:
          - Scoped services like TokenService and AuthService are added, which are responsible for 
            token generation and authentication, respectively.
          - Scoped repository interfaces and their corresponding implementations are added. These 
            repositories handle data access and manipulation within the application.

       2. Database Configuration:
          - The connection string for the database is retrieved from the application's configuration. 
            If the connection string is not specified, an ArgumentException is thrown.
          - Entity Framework Core is used to configure the database context (ProjectDbContext) with 
            PostgreSQL database using the specified connection string.

       3. Authentication Configuration:
          - TokenSettings are bound from the configuration, representing JWT token-related settings. 
            If any required token settings are missing, an ArgumentException is thrown.
          - JWT authentication is configured using JwtBearerDefaults.AuthenticationScheme. The token 
            validation parameters are set based on the specified token settings, including issuer, 
            audience, and symmetric security key for token signing.

     - Return Value:
       The method returns the same IServiceCollection instance, allowing for method chaining.

   Note: 
   - This class follows the convention of placing infrastructure-related dependency injection 
     configuration in a separate class (DependencyInjection) to maintain separation of concerns and 
     modularity.
   - It handles registration of various infrastructure services and configuration of database context 
     and authentication settings.
   - Usage of scoped services and repositories ensures proper lifetime management and data isolation 
     within the application.
*/
