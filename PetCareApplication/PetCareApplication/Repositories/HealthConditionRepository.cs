using Microsoft.EntityFrameworkCore;
using PetCareApplication.Data;

namespace PetCareApplication.Repositories
{
    public class HealthConditionRepository
    {
        private readonly PetCareDbContext _context;

        public HealthConditionRepository(PetCareDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCondition> GetHealthConditionByIdAsync(int petId)
        {
            return await _context.HealthCondition.FirstOrDefaultAsync(x => x.PetId == petId);
        }

        public async Task UpdateHealthConditionAsync(int petId, HealthCondition updatedHealthCondition)
        {
            var currentHealthCondition = await _context.HealthCondition.FirstOrDefaultAsync(x => x.Id == petId);
        
            if (currentHealthCondition != null)
            {
                currentHealthCondition.Diagnosis = updatedHealthCondition.Diagnosis;    
                currentHealthCondition.IsHealth = updatedHealthCondition.IsHealth;

                await _context.SaveChangesAsync();
            }
        }
    }
}
