using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCareApplication.Dtos;
using PetCareApplication.Validators;
using PetCareApplication.Models;
using PetCareApplication.Data;
using PetCareApplication.Repositories;

namespace PetCareApplication.Controllers
{
    [ApiController]
    [Route("api/v1/trainings")]
    public class TrainingController : Controller
    {
        private readonly TrainingReppository _trainingReppository;
        private readonly TrainingValidator _validator;
        private readonly IMapper _mapper;

        public TrainingController(TrainingReppository trainingReppository, TrainingValidator validator, IMapper mapper)
        {
            _trainingReppository = trainingReppository;
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

            await _trainingReppository.CreateTrainingAsync(entity);

            return CreatedAtAction(nameof(GetById), new {petId = trainingDto.Id }, trainingDto);

        }

        [HttpGet("{petId}")]
        public async Task<IActionResult> GetById(int petId)
        {
            var training = await _trainingReppository.GetPetByIdAsync(petId);
;           if (training == null)
            {
                return NotFound();
            }

            var trainingDto = _mapper.Map<TrainingDto>(training);
            return Ok(trainingDto);
        }
    }
}
