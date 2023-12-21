using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareApplication.Data;
using PetCareApplication.Dtos;
using PetCareApplication.Validators;

namespace PetCareApplication.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/pets")]
    public class PetController : Controller
    {
        private readonly PetCareDbContext _context;
        private readonly PetValidator _validator;
        private readonly IMapper _mapper;
        public PetController(PetCareDbContext petCareDbContext, PetValidator petValidator, IMapper mapper)
        {
            _context = petCareDbContext;
            _validator = petValidator;
            _mapper = mapper;
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

            _context.Pet.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { petId = petDto.Id }, petDto);
        }       

        [HttpGet("/pets")]
        public async Task<IActionResult> GetAll()
        {
            var pet = await _context.Pet.ToListAsync();
            var petDto = _mapper.Map<List<PetDto>>(pet);

            return Ok(petDto);
        }        

        [HttpGet("{petId}")]
        public IActionResult GetById(int petId)
        {
            var pet = _context.Pet.Where(x => x.Id == petId).FirstOrDefault();
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
            var current = _context.Pet.Where(x => x.Id == petId).FirstOrDefault();

            if (current is null)
            {
                return NotFound();
            }

            current.PetName = petDto.PetName;
            current.Kind = petDto.Kind;
            current.Age = petDto.Age;
            current.Gender = petDto.Gender;

            _mapper.Map(petDto, current);

            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpGet("/pets/statistics/petId")]
        public async Task<IActionResult> GetPetStatistics(int petId)
        {
            var pet = await _context.Pet
                .Include(p => p.Activities)
                .Include(p => p.HealthConditions)
                .Include(p => p.Foods)
                .FirstOrDefaultAsync(pet => pet.Id == petId);

            if (pet == null)
            {
                return NotFound();
            }
            var statistics = new
            {
                Activities = _mapper.Map<List<ActivityDto>>(pet.Activities),
                HealthConditions = _mapper.Map<List<HealthConditionDto>>(pet.HealthConditions),
                Foods = _mapper.Map<List<FoodDto>>(pet.Foods)
            };
            return Ok(statistics);
        }

    }
}
