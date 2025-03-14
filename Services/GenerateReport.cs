using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using FinacyApi.Services.Interface;
using FinacyApi.Data;
using Microsoft.EntityFrameworkCore;


namespace FinacyApi.Services
{
    public class GenerateReport : IGenerateReport
    {
        private readonly FinancyDbContext _context;
        public GenerateReport(FinancyDbContext context)
        {
            _context = context;
         
        }
        public Task<byte[]> GenerateReportAsync(int Id)
        {
            ReportDto report = GetDataById(Id).Result;
            return GeneratePdfReport(report);
        }
     
        public async Task<byte[]> GeneratePdfReport(ReportDto report)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            
            return await Task.Run(() =>
            {
                var document = new InvoiceDocument(report);
                return document.GeneratePdf();
            });
        }

                


        public async Task<ReportDto> GetDataById(int Id){
            List<ExpenseDto> expenses = await _context.Expenses
                .Where(e => e.UserId == Id)
                .Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Value = e.value,
                    Category = e.Category,
                    ExpenseDate = e.ExpenseDate,
                    Description = e.Description
                })
                .ToListAsync();

            List<RevenueDto> revenues = await _context.Revenues
                .Where(i => i.UserId == Id)
                .Select(r => new RevenueDto
                {
                    Id = r.Id,
                    value = r.value,
                    Description = r.Description,
                    ReceiptDate = r.ReceiptDate
                    
                })
                .ToListAsync();

            List<MetaFinancialDto> metaFinancial = await _context.MetaFinancials
                .Where(g => g.UserId == Id)
                .Select(m => new MetaFinancialDto
                {
                    Id = m.Id,
                    Description = m.Description,
                    TotalValue = m.TotalValue
                })
                .ToListAsync();
            
            var user = _context.Users.Find(Id);

            var report = new ReportDto
            {
                Expenses = expenses,
                Revenues = revenues,
                MetaFinancial = metaFinancial,
                User = user
            };

            return report;
        }




       
    }

}