using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PetCareApplication.Dtos;
using PetCareApplication.Validators;
using PetCareApplication.Data;
using PetCareApplication.Repositories;

namespace PetCareApplication.Controllers
{
    [ApiController]
    [Route("api/v1/foods")]
    public class FoodController : Controller
    {
        private readonly FoodRepository _foodRepository;
        private readonly FoodValidator _validator;
        private readonly IMapper _mapper;

        public FoodController(FoodRepository foodRepository, FoodValidator foodValidator, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _validator = foodValidator;
            _mapper = mapper;
        }

        [HttpGet("/foods")]
        public async Task<IActionResult> GetAll()
        {
            var food = await _foodRepository.GetAllFoodsAsync();
            var foodDto = _mapper.Map<List<FoodDto>>(food);

            return Ok(foodDto);
        }

        [HttpGet("{petId}")]
        public async Task<IActionResult> GetById(int petId)
        {
            var food = await _foodRepository.GetFoodByIdAsync(petId);
            if (food == null)
            {
                return NotFound();
            }
            var foodDto = _mapper.Map<FoodDto>(food);
            return Ok(foodDto);
        }
    }
}
