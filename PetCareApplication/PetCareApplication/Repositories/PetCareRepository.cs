using Microsoft.EntityFrameworkCore;
using PetCareApplication.Data;
using PetCareApplication.Models;

namespace PetCareApplication.Repositories
{
    public class PetCareRepository
    {
        private readonly PetCareDbContext _context;

        public PetCareRepository(PetCareDbContext context)
        {
            _context = context;
        }

        public async Task CreatePetAsync(Pet pet)
        {
            _context.Pet.Add(pet);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Pet>> GetAllPetsAsync()
        {
            return await _context.Pet.ToListAsync();
        }

        public async Task<Pet> GetPetByIdAsync(int petId)
        {
            return await _context.Pet.FirstOrDefaultAsync(pet => pet.Id == petId);
        }

        public async Task UpdatePetAsync(int petId, Pet updatedPet)
        {
            var currentPet = await _context.Pet.FirstOrDefaultAsync(pet => pet.Id == petId);

            if (currentPet != null)
            {
                currentPet.PetName = updatedPet.PetName;
                currentPet.Kind = updatedPet.Kind;
                currentPet.Age = updatedPet.Age;
                currentPet.Gender = updatedPet.Gender;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Activity>> GetPetActivitiesAsync(int petId)
        {
            return await _context.Activity.Where(x => x.PetId == petId).ToListAsync();
        }

        public async Task<List<HealthCondition>> GetPetHealthConditionsAsync(int petId)
        {
            return await _context.HealthCondition.Where(x => x.PetId == petId).ToListAsync();

        }

        public async Task<List<Food>> GetPetFoodsAsync(int petId)
        {
            return await _context.Food.Where(x => x.PetId == petId).ToListAsync();
        }

    }
}
