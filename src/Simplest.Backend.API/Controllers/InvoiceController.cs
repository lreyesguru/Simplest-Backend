using Simplest.Backend.API.Application;
using Microsoft.AspNetCore.Mvc;

namespace Simplest.Backend.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class InvoiceController: ControllerBase
    {
        private readonly IInvoiceService<InvoicesResponseDto> invoiceService;
        public InvoiceController(
            IInvoiceService<InvoicesResponseDto> invoiceService
        )
        {
            this.invoiceService = invoiceService;
        }

        [HttpGet("invoices")]
        public async Task<IActionResult> getInvoices([FromQuery] InvoiceProcessType type, [FromQuery] int pages = 1, [FromQuery] int rows = 15)
        {
            var companyId = (int)HttpContext.Items["companyId"];

            var invoices = await this.invoiceService.getInvoices(type, pages, rows, companyId);

            return Ok(ResponseDto<string>.Ok("invoices"));
        }
    }
}