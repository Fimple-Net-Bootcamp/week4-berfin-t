using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PetCareApplication.Dtos;
using PetCareApplication.Validators;
using PetCareApplication.Data;

namespace PetCareApplication.Controllers
{
    [ApiController]
    [Route("api/v1/foods")]
    public class FoodController : Controller
    {
        private readonly PetCareDbContext _context;
        private readonly FoodValidator _validator;
        private readonly IMapper _mapper;

        public FoodController(PetCareDbContext petCareDbContext, FoodValidator foodValidator, IMapper mapper)
        {
            _context = petCareDbContext;
            _validator = foodValidator;
            _mapper = mapper;
        }

        [HttpGet("/foods")]
        public async Task<IActionResult> GetAll()
        {
            var food = await _context.Food.ToListAsync();
            var foodDto = _mapper.Map<FoodDto>(food);

            return Ok(foodDto);
        }

        [HttpGet("{petId}")]
        public IActionResult GetById(int petId)
        {
            var food = _context.Food.Where(x => x.PetId == petId).FirstOrDefault();
            if (food == null)
            {
                return NotFound();
            }
            var foodDto = _mapper.Map<FoodDto>(food);
            return Ok(foodDto);
        }
    }
}
