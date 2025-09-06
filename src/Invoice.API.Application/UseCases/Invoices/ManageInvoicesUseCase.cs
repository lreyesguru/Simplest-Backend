using Invoice.API.Domain;

namespace Invoice.API.Application;

public class ManageInvoicesUseCase : IManageInvoicesUseCase
{
    private readonly IInvoiceRepository<InvoiceEntitie> _invoiceRepository;
    public ManageInvoicesUseCase(
        IInvoiceRepository<InvoiceEntitie> invoiceRepository
    )
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<List<InvoiceEntitie>> Handle(int top)
    {
        var invoice = await _invoiceRepository.getInvoices(top);

        return invoice;
    }
}
