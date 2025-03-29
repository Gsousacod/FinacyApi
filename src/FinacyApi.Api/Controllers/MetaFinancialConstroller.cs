using FinacyApi.ViewModels;
using FinacyApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinacyApi.Model;

namespace FinacyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetaFinancialConstroller : ControllerBase
    {
        private readonly FinancyDbContext _content;

        public MetaFinancialConstroller(FinancyDbContext context)
        {
            _content = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetaFinacialViewModel>>> GetMetaFinancials()
        {
            var metaFinancials = await _content.MetaFinancials.Select(m=>
                new MetaFinacialViewModel
                {
                    Id = m.Id,
                    TotalValue = m.TotalValue,
                    UserId = m.UserId,
                    Date = m.Date,
                    Description = m.Description,
                    Year = m.Year,
                    MonthlyValue = m.MonthlyValue 
            })
            .ToListAsync();
            return Ok(metaFinancials);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MetaFinacialViewModel>> GetMetaFinancial(int id)
        {
            var metaFinancial = await _content.MetaFinancials.FindAsync(id);
            if (metaFinancial == null)
                return NotFound();
            return Ok(new MetaFinacialViewModel
            {
                Id = metaFinancial.Id,
                TotalValue = metaFinancial.TotalValue,
                UserId = metaFinancial.UserId,
                Date = metaFinancial.Date,
                Description = metaFinancial.Description,
                Year = metaFinancial.Year,
                MonthlyValue = metaFinancial.MonthlyValue
            });
        }

        [HttpPost]
        public async Task<ActionResult<MetaFinacialViewModel>> CreateMetaFinancial([FromBody] MetaFinacialViewModel metaFinancialVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var metaFinancial = new MetaFinancial
            {
                Id = metaFinancialVM.Id,
                TotalValue = metaFinancialVM.TotalValue,
                User = _content.Users.Find(metaFinancialVM.UserId),
                UserId = metaFinancialVM.UserId,
                Date = metaFinancialVM.Date,
                Description = metaFinancialVM.Description,
                Year = metaFinancialVM.Year,
                MonthlyValue = metaFinancialVM.MonthlyValue
            };

            _content.MetaFinancials.Add(metaFinancial);
            await _content.SaveChangesAsync();
            return Ok(metaFinancial);
        }

        [HttpPut]
        public async Task<ActionResult<MetaFinacialViewModel>> UpdateMetaFinancial([FromBody] MetaFinacialViewModel metaFinancialVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var metaFinancial = await _content.MetaFinancials.FindAsync(metaFinancialVM.Id);
            if (metaFinancial == null)
                return NotFound();

            metaFinancial.TotalValue = metaFinancialVM.TotalValue;
            metaFinancial.UserId = metaFinancialVM.UserId;
            metaFinancial.Date = metaFinancialVM.Date;
            metaFinancial.Description = metaFinancialVM.Description;
            metaFinancial.Year = metaFinancialVM.Year;
            metaFinancial.MonthlyValue = metaFinancialVM.MonthlyValue;

            _content.MetaFinancials.Update(metaFinancial);
            await _content.SaveChangesAsync();
            return Ok(metaFinancial);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<MetaFinacialViewModel>> DeleteMetaFinancial(int id)
        {
            var metaFinancial = await _content.MetaFinancials.FindAsync(id);
            if (metaFinancial == null)
                return NotFound();
            _content.MetaFinancials.Remove(metaFinancial);
            await _content.SaveChangesAsync();
            return Ok(metaFinancial);
        }

        

    }
}