using Simplest.Backend.API.Domain;

namespace Simplest.Backend.API.Application;

public interface IManageInvoicesUseCase
{
    public Task<ManageInvoiceResponse<InvoiceEntitie>> Handle(int top);
}