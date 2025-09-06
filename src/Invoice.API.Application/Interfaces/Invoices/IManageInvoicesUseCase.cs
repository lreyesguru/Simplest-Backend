using Invoice.API.Domain;

namespace Invoice.API.Application;

public interface IManageInvoicesUseCase
{
    public Task<List<InvoiceEntitie>> Handle(int top);
}