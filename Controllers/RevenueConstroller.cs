
using FinacyApi.Data;
using FinacyApi.Model;
using FinacyApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RevenueController : ControllerBase
{
    private readonly FinancyDbContext _context;

    public RevenueController(FinancyDbContext context)
    {
        _context = context;
    }

    // GET: api/revenue
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RevenueViewModel>>> GetRevenues()
    {
        var revenues = await _context.Revenues
            .Select(r => new RevenueViewModel
            {
                Id = r.Id,
                Amount = r.value,
                Date = r.ReceiptDate,
                Description = r.Description,
                UserId = r.UserId
            })
            .ToListAsync();

        return Ok(revenues);
    }

    // GET: api/revenue/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RevenueViewModel>> GetRevenue(int id)
    {
        var revenue = await _context.Revenues.FindAsync(id);
        if (revenue == null)
            return NotFound();

        return Ok(new RevenueViewModel
        {
            Id = revenue.Id,
            Amount = revenue.value,
            Date = revenue.ReceiptDate,
            Description = revenue.Description,
            UserId = revenue.UserId
        });
    }

    // POST: api/revenue
    [HttpPost]
    public async Task<ActionResult<Revenue>> CreateRevenue([FromBody] RevenueViewModel revenueVM)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var revenue = new Revenue
        {
            value = revenueVM.Amount,
            ReceiptDate = revenueVM.Date,
            Description = revenueVM.Description,
            UserId = revenueVM.UserId
        };

        _context.Revenues.Add(revenue);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRevenue), new { id = revenue.Id }, revenue);
    }

    // PUT: api/revenue/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRevenue(int id, [FromBody] RevenueViewModel revenueVM)
    {
        if (id != revenueVM.Id)
            return BadRequest("ID mismatch");

        var revenue = await _context.Revenues.FindAsync(id);
        if (revenue == null)
            return NotFound();

        revenue.value = revenueVM.Amount;
        revenue.ReceiptDate = revenueVM.Date;
        revenue.Description = revenueVM.Description;
        revenue.UserId = revenueVM.UserId;

        _context.Entry(revenue).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/revenue/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRevenue(int id)
    {
        var revenue = await _context.Revenues.FindAsync(id);
        if (revenue == null)
            return NotFound();

        _context.Revenues.Remove(revenue);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}