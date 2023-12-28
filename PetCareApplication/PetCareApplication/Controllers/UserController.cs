using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareApplication.Data;
using PetCareApplication.Dtos;
using PetCareApplication.Repositories;
using PetCareApplication.Validators;

namespace PetCareApplication.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly UserValidator _validator;
        private readonly IMapper _mapper;


        public UserController(UserRepository userRepository, UserValidator userValidator, IMapper mapper)
        {
            _userRepository = userRepository;
            _validator = userValidator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            var result = _validator.Validate(userDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var entity = _mapper.Map<UserDto, User>(userDto);

            await _userRepository.CreateUserAsync(entity);

            return CreatedAtAction(nameof(GetById), new { userId = userDto.Id }, userDto);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int userId)
        {
            var user = await _userRepository.GetPetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet("/users/statistics/userId")]
        public async Task<IActionResult> GetUserStatistics(int userId)
        {
            var user = await _userRepository.GetPetByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }
            var pets = await _userRepository.GetUserPetAsync(userId);

            var statistics = new
            {
                Pets = _mapper.Map<List<PetDto>>(user.Pets)
            };
            return Ok(statistics);
        }

    }
}
