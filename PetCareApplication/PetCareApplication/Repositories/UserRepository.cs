using Microsoft.EntityFrameworkCore;
using PetCareApplication.Data;

namespace PetCareApplication.Repositories
{
    public class UserRepository
    {
        private readonly PetCareDbContext _context;

        public UserRepository(PetCareDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetPetByIdAsync(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<List<Pet>> GetUserPetAsync(int userId)
        {
            return await _context.Pet.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
