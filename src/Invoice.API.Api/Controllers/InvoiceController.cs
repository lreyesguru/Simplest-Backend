using Invoice.API.Application;
using Invoice.API.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class InvoiceController : ControllerBase
    {
        private readonly IManageInvoicesUseCase _manageInvoicesUseCase;
        public InvoiceController(
            IManageInvoicesUseCase manageInvoicesUseCase
        )
        {
            _manageInvoicesUseCase = manageInvoicesUseCase;
        }

        [HttpGet("invoices")]
        public async Task<IActionResult> getInvoices([FromQuery] int top = 10)
        {
            var invoices = await _manageInvoicesUseCase.Handle(top);
            return Ok(new { invoices });
        }
    }
}