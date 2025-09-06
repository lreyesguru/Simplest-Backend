namespace Invoice.API.Domain;

public interface IInvoiceRepository<T>
{
    public Task<T> getInvoices();
}
