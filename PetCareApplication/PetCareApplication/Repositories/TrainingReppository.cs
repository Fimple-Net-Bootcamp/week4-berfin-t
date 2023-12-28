using Microsoft.EntityFrameworkCore;
using PetCareApplication.Data;
using PetCareApplication.Models;

namespace PetCareApplication.Repositories
{
    public class TrainingReppository
    {
        private readonly PetCareDbContext _context;

        public TrainingReppository(PetCareDbContext context)
        {
            _context = context;
        }

        public async Task CreateTrainingAsync(Training training)
        {
            _context.Training.Add(training);
            await _context.SaveChangesAsync();
        }

        public async Task<Training> GetPetByIdAsync(int petId)
        {
            return await _context.Training.FirstOrDefaultAsync(x => x.PetId == petId);
        }
    }
}
