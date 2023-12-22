using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCareApplication.Dtos;
using PetCareApplication.Validators;
using PetCareApplication.Models;
using PetCareApplication.Data;

namespace PetCareApplication.Controllers
{
    [ApiController]
    [Route("api/v1/trainings")]
    public class TrainingController : Controller
    {
        private readonly PetCareDbContext _context;
        private readonly TrainingValidator _validator;
        private readonly IMapper _mapper;

        public TrainingController(PetCareDbContext context, TrainingValidator validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTraining(TrainingDto trainingDto)
        {
            var result = _validator.Validate(trainingDto);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            var entity = _mapper.Map<TrainingDto, Training>(trainingDto);

            _context.Training.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {petId = trainingDto.Id }, trainingDto);

        }

        [HttpGet("petId")]
        public IActionResult GetById(int petId)
        {
            var training = _context.Training.Where(x=>x.PetId == petId).FirstOrDefault();
            if (training == null)
            {
                return NotFound();
            }

            var trainingDto = _mapper.Map<TrainingDto>(training);
            return Ok(trainingDto);
        }
    }
}
