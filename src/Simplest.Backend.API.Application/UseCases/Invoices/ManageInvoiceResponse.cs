using Simplest.Backend.API.Domain;

namespace Simplest.Backend.API.Application;

public class ManageInvoiceResponse<T>
{
    public List<InvoiceEntitie> invoices { get; set; }
    public ManageInvoiceResponse(List<InvoiceEntitie> invoices)
    {
        this.invoices = invoices;
    }
}
