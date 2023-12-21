using PetCareApplication.Data;

namespace PetCareApplication.Repositories
{
    public class PetRepository
    {
        public readonly PetCareDbContext _context;

        public PetRepository(PetCareDbContext context)
        {
            _context = context;
        }

        public void CreateUser(Pet pet)
        {
            _context.Pet.Add(pet);
            _context.SaveChanges();
        }
        public Pet GetPetById(int petId)
        {
            return _context.Pet.Find(petId);
        }

        public List<Pet> GetAll()
        {
            return _context.Pet.ToList();
        }

        public void SaveChangesAsync()
        {
            _context.SaveChangesAsync();
        }
    }
}
