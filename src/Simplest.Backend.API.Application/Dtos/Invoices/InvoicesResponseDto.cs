namespace Simplest.Backend.API.Application;
using Simplest.Backend.API.Domain;
public class InvoicesResponseDto
{
    List<InvoiceEntitie> invoices;
    public InvoicesResponseDto(List<InvoiceEntitie> invoices)
    {
        this.invoices = invoices;
    }
}