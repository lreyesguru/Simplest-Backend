using Invoice.API.Domain;

namespace Invoice.API.Application;

public interface IManageInvoicesUseCase
{
    public Task<ManageInvoiceResponse<InvoiceEntitie>> Handle(int top);
}