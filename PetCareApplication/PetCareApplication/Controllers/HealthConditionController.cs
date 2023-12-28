using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCareApplication.Data;
using PetCareApplication.Validators;
using PetCareApplication.Dtos;
using PetCareApplication.Repositories;

namespace PetCareApplication.Controllers
{
    [ApiController]
    [Route("api/v1/healthConditions")]
    public class HealthConditionController : Controller
    {
        private readonly HealthConditionRepository _healthConditionRepository;
        private readonly HealthConditionValidator _validator;
        private readonly IMapper _mapper;

        public HealthConditionController(HealthConditionRepository healthConditionRepository, HealthConditionValidator healthConditionValidator, IMapper mapper)
        {
            _healthConditionRepository = healthConditionRepository;
            _validator = healthConditionValidator;
            _mapper = mapper;
        }

        [HttpGet("{petId}")]
        public async Task<IActionResult> GetById(int petId)
        {
            var healthCondition = await _healthConditionRepository.GetHealthConditionByIdAsync(petId);
            if (healthCondition == null)
            {                
                return NotFound();
            }
            var healthConditionDto = _mapper.Map<HealthConditionDto>(healthCondition);
            return Ok(healthCondition);
        }

        [HttpPut("{petId}")]
        public async Task<IActionResult> Update(int petId, HealthConditionDto healthConditionDto)
        {
            var current = await _healthConditionRepository.GetHealthConditionByIdAsync(petId);
            if (current == null)
            {
                return NotFound();
            }

            _mapper.Map(healthConditionDto, current);

            await _healthConditionRepository.UpdateHealthConditionAsync(petId, current);

            return Ok();
        }

    }
}
