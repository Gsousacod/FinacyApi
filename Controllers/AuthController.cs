
using FinacyApi.Data;
using FinacyApi.Dtos;
using FinacyApi.Model;
using FinacyApi.Services;
using FinacyApi.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace FinacyApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly TokensService _tokensService;
        private readonly FinancyDbContext _context;

        public AuthController(TokensService tokensService , FinancyDbContext context)
        {
            _tokensService = tokensService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromForm] LoginDto loginDto)
        {

            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);


            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = _tokensService.GenerateToken(user.Id.ToString(), user.Email, user.Role);
           
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserViewModel>> RegisterUser([FromForm] UserRegisterDto model)
        {
            if (!ModelState.IsValid)
                        return BadRequest(ModelState);

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // Hash da senha
                SalaryMonthly = model.SalaryMonthly,
                Role = "Client", // Definindo a role automaticamente
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                SalaryMonthly = user.SalaryMonthly,

            };

            return CreatedAtAction(nameof(RegisterUser), new { id = user.Id }, userViewModel);
        }

        

    }
}