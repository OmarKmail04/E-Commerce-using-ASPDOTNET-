using KEShop_Api_N_Tier_Art.BLL.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace KEShop_Api_N_Tier_Art.PL.Areas.Admin.Controller
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet("PdfProduct")]
        public IResult GetPdfProductReport() {

            //_reportService.GenerateProductReports();
            // use any method to create a document, e.g.: injected service
            var document = _reportService.CreateDocument();

            // generate PDF file and return it as a response
            var pdf = document.GeneratePdf();
            return Results.File(pdf, "application/pdf", "omproducts.pdf");
        } 
    }
}
