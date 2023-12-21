using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCareApplication.Data;
using PetCareApplication.Validators;
using PetCareApplication.Dtos;

namespace PetCareApplication.Controllers
{
    [ApiController]
    [Route("api/v1/healthConditions")]
    public class HealthConditionController : Controller
    {
        private readonly PetCareDbContext _context;
        private readonly HealthConditionValidator _validator;
        private readonly IMapper _mapper;

        public HealthConditionController(PetCareDbContext petCareDbContext, HealthConditionValidator healthConditionValidator, IMapper mapper)
        {
            _context = petCareDbContext;
            _validator = healthConditionValidator;
            _mapper = mapper;
        }

        [HttpGet("{petId}")]
        public IActionResult GetById(int petId)
        {
            var healthCondition = _context.HealthCondition.Where(x => x.Id == petId).FirstOrDefault();
            if (healthCondition == null)
            {                
                return NotFound();
            }
            var healthConditionDto = _mapper.Map<HealthConditionDto>(healthCondition);
            return Ok(healthCondition);
        }

        [HttpPatch("{petId}")]
        public IActionResult Update(int petId, [FromBody] HealthCondition updatedHealthCondition)
        {
            var existingHealthCondition = _context.HealthCondition.FirstOrDefault(x => x.PetId == petId);

            if (existingHealthCondition == null)
            {
                return NotFound();
            }

            _context.SaveChanges();

            return Ok(existingHealthCondition);
        }

    }
}
