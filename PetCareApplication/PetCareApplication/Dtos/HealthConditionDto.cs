namespace PetCareApplication.Dtos
{
    public class HealthConditionDto
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public bool IsHealth { get; set; }
        public int PetId { get; set; }
    }
}
