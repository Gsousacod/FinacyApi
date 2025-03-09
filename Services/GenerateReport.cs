using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using FinacyApi.Services.Interface;
using FinacyApi.Data;
using Microsoft.EntityFrameworkCore;
using FinacyApi.Model;

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
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Margin(30);
                        page.Header()
                            .AlignCenter()
                            .Text("Relatório Financeiro")
                            .FontSize(20)
                            .SemiBold();

                        page.Content().Column(col =>
                        {
                            col.Item().Text("\nReceitas:").FontSize(16).SemiBold();
                            foreach (var revenue in report.Revenues)
                            {
                                col.Item().Text($"- {revenue.Description}: R$ {revenue.value} (Data: {revenue.ReceiptDate.ToShortDateString()})");
                            }

                            col.Item().Text("\nDespesas:").FontSize(16).SemiBold();
                            foreach (var expense in report.Expenses)
                            {
                                col.Item().Text($"- {expense.Category}: R$ {expense.Value} (Data: {expense.ExpenseDate.ToShortDateString()})");
                            }

                            col.Item().Text("\nMetas Financeiras:").FontSize(16).SemiBold();
                            foreach (var meta in report.MetaFinancial)
                            {
                                col.Item().Text($"- {meta.Description}: R$ {meta.TotalValue}");
                            }
                        });

                        page.Footer()
                            .AlignRight()
                            .Text($"Gerado em: {System.DateTime.Now.ToShortDateString()}")
                            .FontSize(10);
                    });
                });

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

            var report = new ReportDto
            {
                Expenses = expenses,
                Revenues = revenues,
                MetaFinancial = metaFinancial
            };

            return report;
        }




       
    }

}