using Invoice.API.Application;
using Invoice.API.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class InvoiceController : ControllerBase
    {
        private readonly IManageInvoicesUseCase manageInvoicesUseCase;
        public InvoiceController(
            IManageInvoicesUseCase manageInvoicesUseCase
        )
        {
            this.manageInvoicesUseCase = manageInvoicesUseCase;
        }

        [HttpGet("invoices")]
        public async Task<IActionResult> getInvoices([FromQuery] int limit = 10)
        {
            var invoices = await this.manageInvoicesUseCase.Handle(limit);

            return Ok(ResponseDto<ManageInvoiceResponse<InvoiceEntitie>>.Ok(invoices));
        }
    }
}