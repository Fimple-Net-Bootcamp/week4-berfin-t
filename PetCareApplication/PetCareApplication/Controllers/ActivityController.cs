using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetCareApplication.Validators;
using PetCareApplication.Dtos;
using PetCareApplication.Data;
using PetCareApplication.Repositories;

namespace PetCareApplication.Controllers
{
    [ApiController]
    [Route("api/v1/socialInteractions")]
    public class ActivityController : Controller
    {
        private readonly ActivityRepository _activityRepository;
        private readonly ActivityValidator _validator;
        private readonly IMapper _mapper;

        public ActivityController(ActivityRepository activityRepository, ActivityValidator activitiesValidator, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _validator = activitiesValidator;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityDto activityDto)
        {
            var result = _validator.Validate(activityDto);

            if(!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var entity = _mapper.Map<ActivityDto, Activity>(activityDto);

            await _activityRepository.CreateActivityAsync(entity);

            return CreatedAtAction(nameof(GetById), new { petId = activityDto.Id }, activityDto);
        }

        [HttpGet("{petId}")]
        public async Task<IActionResult> GetById(int petId)
        {
            var activities = await _activityRepository.GetActivityByIdAsync(petId);

            if (activities == null)
            {
                return NotFound();
            }

            var activitiesDto = _mapper.Map<ActivityDto>(activities);
            return Ok(activitiesDto);
        }
    }
}
