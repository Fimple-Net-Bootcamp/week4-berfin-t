using PetCareApplication.Data;

namespace PetCareApplication.Dtos
{
    public class PetDto
    {
        public int Id { get; set; }
        public string PetName { get; set; }
        public string Kind { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public int UserId { get; set; }
        public List<ActivityDto> ActivitiesDto { get; set; }
        public List<HealthConditionDto> HealthConditionDto { get; set; }
        public List<FoodDto> FoodDto { get; set; }
    }
}
