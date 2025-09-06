namespace Invoice.API.Application;

public interface IManageInvoicesUseCase<T>
{
    public Task<T> Handle();
}