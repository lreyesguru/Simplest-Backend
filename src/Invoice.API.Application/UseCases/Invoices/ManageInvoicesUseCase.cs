using Invoice.API.Domain;

namespace Invoice.API.Application;

public class ManageInvoicesUseCase : IManageInvoicesUseCase<string>
{
    private readonly IInvoiceRepository<string> _invoiceRepository;
    public ManageInvoicesUseCase(
        IInvoiceRepository<string> invoiceRepository
    )
    {
        _invoiceRepository = invoiceRepository;
    }

    public Task<string> Handle()
    {
        return Task.FromResult("Hola Mundo");
    }
}
