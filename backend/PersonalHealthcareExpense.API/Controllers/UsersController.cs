using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalHealthcareExpense.API.DTOs;
using PersonalHealthcareExpense.API.DTOs.User;
using PersonalHealthcareExpense.API.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PersonalHealthcareExpense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(
            IUserRepository repository,
            IUserService userService,
            IMapper mapper)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.GetAllUsersAsync();

            var result = _mapper.Map<IEnumerable<UserDTO>>(users);

            return Ok(result);
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _userService.RegisterUserAsync(dto);

            if (!success)
                return BadRequest("Email already exists.");

            return Ok("User registered successfully.");
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _userService.LoginAsync(dto);

            if (token == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new
            {
                Token = token
            });
        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var name = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            return Ok(new
            {
                UserId = userId,
                Name = name,
                Email = email
            });
        }
    }
}