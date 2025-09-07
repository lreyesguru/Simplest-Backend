using Simplest.Backend.API.Domain;

namespace Simplest.Backend.API.Application;

public class InvoiceService : IInvoiceService<InvoicesResponseDto>
{
    private readonly IInvoiceRepository<InvoiceEntitie> invoiceRepository;
    public InvoiceService(
        IInvoiceRepository<InvoiceEntitie> invoiceRepository
    )
    {
        this.invoiceRepository = invoiceRepository;
    }

    public async Task<InvoicesResponseDto> getInvoices(InvoiceProcessType type, int rows, int pages, int companyId)
    {
        var result = await this.invoiceRepository.getInvoices(type, rows, pages, companyId);

        if (result is null)
        {
            throw new Exception("No invoices fetched.");
        }

        var invoices = new InvoicesResponseDto(result);

        return invoices;
    }

}
