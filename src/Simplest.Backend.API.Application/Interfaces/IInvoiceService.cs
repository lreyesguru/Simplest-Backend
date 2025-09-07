namespace Simplest.Backend.API.Application;

public interface IInvoiceService<InvoicesResponseDto>
{
    public Task<InvoicesResponseDto> getInvoices(InvoiceProcessType type, int rows, int pages, int companyId);
}
