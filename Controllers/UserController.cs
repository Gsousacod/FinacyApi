
using System.Threading.Tasks;
using FinacyApi.Data;
using FinacyApi.Model;
using FinacyApi.Services;
using FinacyApi.Services.Interface;
using FinacyApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinacyApi.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        private readonly FinancyDbContext _context;
        private readonly IGenerateReport _generateReport;
        public UserController(FinancyDbContext context, IGenerateReport generateReport)
        {
            _generateReport = generateReport;
            _context = context;
        }

        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var users = await _context.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                SalaryMonthly = u.SalaryMonthly,
                DataCreated = u.DataCreated,
            })
            .ToListAsync();
           

            return Ok(users);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserViewModel>> Create([FromForm] UserViewModel userVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User{
                Name = userVm.Name,
                Email = userVm.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userVm.Password),
                Role = userVm.Role,
                SalaryMonthly = userVm.SalaryMonthly,
                DataCreated = userVm.DataCreated
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromBody] User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByData(int id)
        {
            try
            {
                var pdfReport = await _generateReport.GenerateReportAsync(id); // Aguarda a execução
                
                return File(pdfReport, "application/pdf", "Relatorio_Financeiro.pdf");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}