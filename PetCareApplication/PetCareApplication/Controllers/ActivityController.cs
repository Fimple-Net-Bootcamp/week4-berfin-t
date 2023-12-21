using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCareApplication.Validators;
using PetCareApplication.Dtos;
using PetCareApplication.Data;

namespace PetCareApplication.Controllers
{
    [ApiController]
    [Route("api/v1/activities")]
    public class ActivityController : Controller
    {
        private readonly PetCareDbContext _context; 
        private readonly ActivityValidator _validator;
        private readonly IMapper _mapper;

        public ActivityController(PetCareDbContext petCareDbContext, ActivityValidator activitiesValidator, IMapper mapper)
        {
            _context = petCareDbContext;
            _validator = activitiesValidator;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(ActivityDto activityDto)
        {
            var result = _validator.Validate(activityDto);

            if(!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var entity = _mapper.Map<ActivityDto, Activity>(activityDto);

            _context.Activities.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { petId = activityDto.Id }, activityDto);
        }

        [HttpGet("{petId}")]
        public IActionResult GetById(int petId)
        {
            var activities = _context.Activities.Where(x => x.PetId == petId).FirstOrDefault();

            if (activities == null)
            {
                return NotFound();
            }

            var activitiesDto = _mapper.Map<ActivityDto>(activities);
            return Ok(activitiesDto);
        }
    }
}
