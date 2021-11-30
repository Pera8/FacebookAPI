using FacebookApiTest.Repository;
using FacebookApiTest.Repository.Models;
using Mapster;
using Shared.DTO;
using System;
using System.Linq;

namespace Service.Configuration
{
    public static class MapperConfig
    {
        public static void RegisterUserMapping()
        {
            TypeAdapterConfig<User, UserAuth>.NewConfig()
                .Map(dest => dest.Id,
                    src => src.Id)
                .Map(dest => dest.Email,
                    src => src.Email)
                .Map(dest => dest.UserName,
                    src => src.UserName);



        }
    }
}
