using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinacyApi.Services.Interface
{
    public interface IGenerateReport
    {
   
        Task<byte[]> GeneratePdfReport(ReportDto report);
        Task<ReportDto> GetDataById(int Id);
         Task<byte[]> GenerateReportAsync(int Id);
        // Task<byte[]> GenerateReportAsync(int Id, DateTime month);
        // Task<byte[]> GenerateReportGeneralAsync(int Id, DateTime month);

    }
}