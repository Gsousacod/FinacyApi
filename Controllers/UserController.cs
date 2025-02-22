
using System.Threading.Tasks;
using FinacyApi.Data;
using FinacyApi.Model;
using FinacyApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinacyApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        private readonly FinancyDbContext _context;
        public UserController(FinancyDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var users = await _context.Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                SalaryMonthly = u.SalaryMonthly,
                DataCreated = u.DataCreated,
                FinancialDistribution = u.CalculateFinancialDistribution()
            })
            .ToListAsync();
           

            return Ok(users);

        }

        [HttpPost]

        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        public IActionResult Update([FromBody] User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(user);
        }


    }
}