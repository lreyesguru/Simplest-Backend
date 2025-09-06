namespace Invoice.API.Domain;

public interface IInvoiceRepository<T>
{
    public Task<List<InvoiceEntitie>> getInvoices(int top);
}
