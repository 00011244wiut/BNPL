using Application.DTOs.Auth;  // Importing necessary namespaces
using Application.DTOs.Card;  // Importing necessary namespaces
using Application.DTOs.LegalData;  // Importing necessary namespaces
using Application.DTOs.Merchant;  // Importing necessary namespaces
using Application.DTOs.Product;  // Importing necessary namespaces
using Application.DTOs.Schedule;  // Importing necessary namespaces
using Application.DTOs.User;  // Importing necessary namespaces
using AutoMapper;  // Importing necessary namespaces
using Domain.Entities;  // Importing necessary namespaces

namespace Application.MappingProfile;  // Namespace declaration

public class Mapping : Profile  // Mapping Profile Class
{
    public Mapping()  // Constructor
    {
        CreateMap<UserEntity, UserResponseDto>().ReverseMap();  // Mapping from UserEntity to UserResponseDto and vice versa
        CreateMap<RegisterUserInfoDto, UserEntity>().ReverseMap();  // Mapping from RegisterUserInfoDto to UserEntity and vice versa
        CreateMap<SubmitCardDto, CardsEntity>().ReverseMap();  // Mapping from SubmitCardDto to CardsEntity and vice versa
        
        CreateMap<MerchantEntity, MerchantResponseDto>().ReverseMap();  // Mapping from MerchantEntity to MerchantResponseDto and vice versa
        CreateMap<RegisterMerchantInfoDto, MerchantEntity>().ReverseMap();  // Mapping from RegisterMerchantInfoDto to MerchantEntity and vice versa

        CreateMap<LegalDataEntity, LegalDataResponseDto>().ReverseMap();  // Mapping from LegalDataEntity to LegalDataResponseDto and vice versa
        CreateMap<LegalDataEntity, LegalDataRequestDto>().ReverseMap();  // Mapping from LegalDataEntity to LegalDataRequestDto and vice versa

        CreateMap<ProductsEntity, ProductRequestDto>().ReverseMap();  // Mapping from ProductsEntity to ProductRequestDto and vice versa
        CreateMap<ProductsEntity, ProductResponseDto>().ReverseMap();  // Mapping from ProductsEntity to ProductResponseDto and vice versa
        CreateMap<ProductsEntity, ProductUpdateDto>().ReverseMap();  // Mapping from ProductsEntity to ProductUpdateDto and vice versa

        CreateMap<SchedulesEntity, ScheduleResponseDto>().ReverseMap();  // Mapping from SchedulesEntity to ScheduleResponseDto and vice versa

        CreateMap<CardsEntity, CardResponseDto>().ReverseMap();  // Mapping from CardsEntity to CardResponseDto and vice versa
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
