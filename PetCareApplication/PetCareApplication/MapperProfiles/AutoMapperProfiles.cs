using AutoMapper;
using PetCareApplication.Dtos;
using PetCareApplication.Data;
using PetCareApplication.Models;

namespace PetCareApplication.MapperProfiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
        CreateMap<Pet, PetDto>().ReverseMap();

        CreateMap<Data.Activity, ActivityDto>().ReverseMap();

        CreateMap<HealthCondition, HealthConditionDto>().ReverseMap();

        CreateMap<Food, FoodDto>().ReverseMap();

        CreateMap<Training, TrainingDto>().ReverseMap();
        }
    }
}
