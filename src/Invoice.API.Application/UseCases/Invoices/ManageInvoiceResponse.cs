using Invoice.API.Domain;

namespace Invoice.API.Application;

public class ManageInvoiceResponse<T>
{
    public List<InvoiceEntitie> invoices { get; set; }
    public ManageInvoiceResponse(List<InvoiceEntitie> invoices)
    {
        this.invoices = invoices;
    }
}
