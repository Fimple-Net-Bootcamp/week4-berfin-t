using PetCareApplication.Data;
using Microsoft.EntityFrameworkCore;
using PetCareApplication.Models;

namespace PetCareApplication.Repositories
{
    public class ActivityRepository
    {
        private readonly PetCareDbContext _context;

        public ActivityRepository(PetCareDbContext context)
        {
            _context = context;
        }

        public async Task CreateActivityAsync(Activity activity)
        {
            _context.Activity.Add(activity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Activity>> GetActivitiesAsync()
        {
            return await _context.Activity.ToListAsync();
        }
        public async Task<Activity> GetActivityByIdAsync(int petId)
        {
            return await _context.Activity.FirstOrDefaultAsync(x => x.PetId == petId);
        }
    }
}
