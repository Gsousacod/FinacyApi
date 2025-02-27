using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinacyApi.Data;
using FinacyApi.Dtos;
using FinacyApi.Model;
using FinacyApi.Services;
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

        public AuthController(TokensService tokensService , FinancyDbContext context)
        {
            _tokensService = tokensService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Authenticate(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = _tokensService.GenerateToken(user.Id.ToString(), user.Email);
           
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

    }
}