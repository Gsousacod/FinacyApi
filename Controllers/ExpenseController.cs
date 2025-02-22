
using FinacyApi.Data;
using FinacyApi.Model;
using FinacyApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinacyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly FinancyDbContext _context;

        public ExpenseController(FinancyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseViewModel>>> GetExpense()
        {
            var expenses = await _context.Expenses.Select(e=>new ExpenseViewModel{
                Id = e.Id,
                Amount = e.value,
                Date = e.ExpenseDate,
                Description = e.Description,
                UserId = e.UserId,
                Category = e.Category
            }).ToListAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseViewModel>> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
                return NotFound();
            return Ok(new ExpenseViewModel
            {
                Id = expense.Id,
                Amount = expense.value,
                Date = expense.ExpenseDate,
                Description = expense.Description,
                UserId = expense.UserId,
                Category = expense.Category
            });
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseViewModel>> CreateExpense([FromBody] ExpenseViewModel expenseVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var expense = new Expense
            {
                value = expenseVM.Amount,
                ExpenseDate = expenseVM.Date,
                Description = expenseVM.Description,
                UserId = expenseVM.UserId,
                Category = expenseVM.Category
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        [HttpPut]
        public async Task<ActionResult<ExpenseViewModel>> UpdateExpense([FromBody] ExpenseViewModel expenseVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var expense = await _context.Expenses.FindAsync(expenseVM.Id);
            if (expense == null)
                return NotFound();

            expense.value = expenseVM.Amount;
            expense.ExpenseDate = expenseVM.Date;
            expense.Description = expenseVM.Description;
            expense.UserId = expenseVM.UserId;
            expense.Category = expenseVM.Category;

            await _context.SaveChangesAsync();

            return Ok(expense);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ExpenseViewModel>> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
                return NotFound();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return Ok(expense);
        }
    }
}