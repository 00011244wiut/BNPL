using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            return services;
        }
    }
}

/*
   DependencyInjection Class

   This class defines a static extension method named AddApplicationServices which is intended to be used 
   for configuring dependency injection within the application. This method extends IServiceCollection, 
   which is provided by the Microsoft.Extensions.DependencyInjection namespace.

   - AddApplicationServices Method:
     This method is responsible for configuring and registering various application services using the 
     provided IServiceCollection parameter. It is called during the setup phase of the application.

     - Parameters:
       - services (IServiceCollection): The IServiceCollection instance to which application services 
         are added.

     - Method Implementation:
       1. AddMediatR:
          This method adds MediatR services to the service collection. MediatR is a simple mediator 
          implementation for .NET. It allows for the implementation of the mediator pattern within 
          applications. The RegisterServicesFromAssembly extension method is used to register all 
          services from the assembly in which this method is called (Assembly.GetExecutingAssembly()).

       2. AddAutoMapper:
          This method adds AutoMapper services to the service collection. AutoMapper is a library used 
          for object-to-object mapping. It helps to eliminate repetitive mapping code in applications. 
          Similar to AddMediatR, it registers AutoMapper services using the assembly in which this 
          method is called.

     - Return Value:
       The method returns the same IServiceCollection instance, allowing for method chaining.

   Note: 
   - This class follows the convention of placing dependency injection configuration in a separate 
     class (DependencyInjection) to keep the main application code clean and modular.
   - It utilizes reflection (Assembly.GetExecutingAssembly()) to automatically register services from 
     the assembly in which it's defined, reducing the need for manual service registration.
   - The AddApplicationServices method is typically called during application startup to configure 
     dependency injection.
*/
