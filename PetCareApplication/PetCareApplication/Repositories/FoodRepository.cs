using Microsoft.EntityFrameworkCore;
using PetCareApplication.Data;

namespace PetCareApplication.Repositories
{
    public class FoodRepository
    {
        private readonly PetCareDbContext _context;

        public FoodRepository(PetCareDbContext context)
        {
            _context = context;
        }

        public async Task<List<Food>> GetAllFoodsAsync()
        {
            return await _context.Food.ToListAsync();
        }

        public async Task<Food> GetFoodByIdAsync(int petId)
        {
            return await _context.Food.FirstOrDefaultAsync(x => x.PetId == petId);
        }
    }
}
