using Invoice.API.Domain;
using Microsoft.Data.SqlClient;
// using Microsoft.
namespace Invoice.API.Infrastructure;

public class InvoiceRepository : IInvoiceRepository<string>
{
    private readonly SqlConnection _conn;

    public InvoiceRepository(SqlConnection conn)
    {
        _conn = conn;
    }

    public Task<string> getInvoices()
    {
        // var invoices = _conn.
        return Task.FromResult("");
    }
}
