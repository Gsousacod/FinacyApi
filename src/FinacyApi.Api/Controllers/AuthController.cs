
using System.Security.Claims;
using FinacyApi.Data;
using FinacyApi.Dtos;
using FinacyApi.Model;
using FinacyApi.Services;
using FinacyApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FinacyApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly TokensService _tokensService;
        private readonly FinancyDbContext _context;

        private readonly LoggingService _loggingService;



        public AuthController(TokensService tokensService, FinancyDbContext context, LoggingService loggingService)
        {
            _tokensService = tokensService;
            _context = context;
            _loggingService = loggingService;

        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginDto loginDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                {
                    _loggingService.LogWarning($"Login attempt with incomplete data. Email: {loginDto.Email}");
                    return BadRequest(new { message = "Email and password are required" });
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
                var userAcess = new UserAcessViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    SalaryMonthly = user.SalaryMonthly,
                    DataCreated = user.DataCreated,
                };


                if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    _loggingService.LogWarning($"Login attempt failed for {loginDto.Email} at {DateTime.UtcNow}");
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                var jwt = _tokensService.GenerateToken(user.Id.ToString(), user.Email, user.Role);

                _loggingService.LogLoginAttempt(user.Email, true);

                return Ok(new { jwt, userAcess, message = "Login successful" });
            }
            catch (Exception error)
            {
                _loggingService.LogError(1, $"Error authenticating user {loginDto.Email}", error);
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }



        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> RegisterUser([FromForm] UserRegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    SalaryMonthly = model.SalaryMonthly,
                    Role = "Client",
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    DataCreated = user.DataCreated,
                    SalaryMonthly = user.SalaryMonthly,

                };


                return CreatedAtAction(nameof(RegisterUser), new { id = user.Id }, userViewModel);

            }
            catch (Exception ex)
            {
                _loggingService.LogError(2, "Error registering user", ex);
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult<dynamic> GetUserInfo()
        {
            try
            {

                var user = HttpContext.User;

                if (user == null)
                {
                    return Unauthorized(new { message = "User not authenticated" });
                }


                var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                var userEmail = user.FindFirstValue(ClaimTypes.Email);
                var userRole = user.FindFirstValue(ClaimTypes.Role);


                return Ok(new
                {
                    Id = userId,
                    Email = userEmail,
                    Role = userRole
                });
            }
            catch (Exception ex)
            {
                _loggingService.LogError(3, "Error retrieving user information", ex);
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }

    }
}