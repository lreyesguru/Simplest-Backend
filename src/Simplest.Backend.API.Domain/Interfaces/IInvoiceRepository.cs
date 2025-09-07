namespace Simplest.Backend.API.Domain;

public interface IInvoiceRepository<T>
{
    public Task<List<T>> getInvoices(InvoiceProcessType type, int rows, int pages, int companyId);
}
