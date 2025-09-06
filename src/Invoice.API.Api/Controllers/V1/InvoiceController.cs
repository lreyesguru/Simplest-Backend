using Invoice.API.Application;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class InvoiceController : ControllerBase
    {
        private readonly IManageInvoicesUseCase<string> _manageInvoicesUseCase;
        public InvoiceController(
            IManageInvoicesUseCase<string> manageInvoicesUseCase
        )
        {
            _manageInvoicesUseCase = manageInvoicesUseCase;
        }

        [HttpGet("invoices")]
        public async Task<IActionResult> getInvoices()
        {
            var invoices = await _manageInvoicesUseCase.Handle();
            return Ok(new { invoices });
        }
    }
}