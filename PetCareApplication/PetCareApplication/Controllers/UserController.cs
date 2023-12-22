using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareApplication.Data;
using PetCareApplication.Dtos;
using PetCareApplication.Validators;

namespace PetCareApplication.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : Controller
    {
        private readonly PetCareDbContext _context;
        private readonly UserValidator _validator;
        private readonly IMapper _mapper;


        public UserController(PetCareDbContext petCareDbContext, UserValidator userValidator, IMapper mapper)
        {
            _context = petCareDbContext;
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

            _context.User.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { userId = userDto.Id }, userDto);
        }

        [HttpGet("{userId}")]
        public IActionResult GetById(int userId)
        {
            var user = _context.User.Where(x => x.Id == userId).FirstOrDefault();
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
            var user = await _context.User
                .Include(p=> p.Pets)
                .FirstOrDefaultAsync(user => user.Id == userId);

            if (user == null)
            {
                return NotFound();
            }
            var statistics = new
            {
                Pets = _mapper.Map<List<PetDto>>(user.Pets)
            };
            return Ok(statistics);
        }

    }
}
