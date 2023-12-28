using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCareApplication.Data;
using PetCareApplication.Dtos;
using PetCareApplication.Repositories;
using PetCareApplication.Validators;

namespace PetCareApplication.Controllers
{
    
    [ApiController]
    [Route("api/v1/pets")]
    public class PetController : Controller
    {
        private readonly PetCareRepository _petCareRepository;
        private readonly PetValidator _validator;
        private readonly IMapper _mapper;
        public PetController(PetCareRepository petCareRepository, PetValidator petValidator, IMapper mapper)
        {
            _petCareRepository = petCareRepository;
            _mapper = mapper;
            _validator = petValidator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePets(PetDto petDto)
        {
            var result = _validator.Validate(petDto);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var entity = _mapper.Map<PetDto, Pet>(petDto);

            await _petCareRepository.CreatePetAsync(entity);

            return CreatedAtAction(nameof(GetById), new { petId = entity.Id }, petDto);
        }

        [HttpGet("/pets")]
        public async Task<IActionResult> GetAll()
        {
            var pets = await _petCareRepository.GetAllPetsAsync();
            var petDto = _mapper.Map<List<PetDto>>(pets);

            return Ok(petDto);
        }

        [HttpGet("{petId}")]
        public async Task<IActionResult> GetById(int petId)
        {
            var pet = await _petCareRepository.GetPetByIdAsync(petId);

            if (pet == null)
            {
                return NotFound();
            }

            var petDto = _mapper.Map<PetDto>(pet);
            return Ok(petDto);
        }

        [HttpPut("{petId}")]
        public async Task<IActionResult> Update(int petId, PetDto petDto)
        {
            var current = await _petCareRepository.GetPetByIdAsync(petId);

            if (current is null)
            {
                return NotFound();
            }

            _mapper.Map(petDto, current);

            await _petCareRepository.UpdatePetAsync(petId, current);

            return Ok();
        }

        [HttpGet("/pets/statistics/{petId}")]
        public async Task<IActionResult> GetPetStatistics(int petId)
        {
            var pet = await _petCareRepository.GetPetByIdAsync(petId);

            if (pet == null)
            {
                return NotFound();
            }

            var activities = await _petCareRepository.GetPetActivitiesAsync(petId);
            var healthConditions = await _petCareRepository.GetPetHealthConditionsAsync(petId);
            var foods = await _petCareRepository.GetPetFoodsAsync(petId);

            var statistics = new
            {
                Activities = _mapper.Map<List<ActivityDto>>(activities),
                HealthConditions = _mapper.Map<List<HealthConditionDto>>(healthConditions),
                Foods = _mapper.Map<List<FoodDto>>(foods)
            };

            return Ok(statistics);
        }


    }
}
