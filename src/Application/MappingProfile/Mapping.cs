using Application.DTOs.Auth;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfile;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UserEntity, UserResponseDto>().ReverseMap();
        CreateMap<RegisterUserInfoDto, UserEntity>().ReverseMap();
        CreateMap<SubmitCardDto, CardsEntity>().ReverseMap();
    }
}

/*
   Mapping Profile Class

   This class defines a mapping profile using AutoMapper, a library that simplifies object-to-object 
   mapping in .NET applications. It is intended to define mappings between domain entities and DTOs 
   (Data Transfer Objects) used within the application.

   - Mapping Class:
     This class inherits from AutoMapper's Profile class and is responsible for defining mappings 
     between different types of objects.

     - Constructor:
       - The constructor of the Mapping class defines mappings between specific types of objects using 
         AutoMapper's CreateMap method.
       - In this example, mappings are defined between UserEntity and UserResponseDto, as well as 
         RegisterUserInfoDto and UserEntity. The ReverseMap method is called to enable bidirectional 
         mapping between the source and destination types.

       - Mapping Definitions:
         - UserEntity to UserResponseDto: Mapping from the domain entity representing a user to the DTO 
           representing user response.
         - RegisterUserInfoDto to UserEntity: Mapping from the DTO containing user registration information 
           to the domain entity representing a user.

     - AutoMapper Configuration:
       - This class is typically registered during the application startup to configure AutoMapper's 
         mapping profiles.

   Note: 
   - AutoMapper simplifies the process of mapping data between objects with different structures, 
     reducing the need for manual mapping code.
   - Mapping profiles like this help maintain separation of concerns and promote reusability by 
     encapsulating mapping logic in a dedicated class.
   - The ReverseMap method is used to create bidirectional mappings, allowing for seamless mapping 
     between source and destination types in both directions.
*/
