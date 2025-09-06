using Invoice.API.Domain;
using Dapper;
using Microsoft.Data.SqlClient;
// using Microsoft.
namespace Invoice.API.Infrastructure;

public class InvoiceRepository : IInvoiceRepository<InvoiceEntitie>
{
    private readonly SqlConnection _conn;

    public InvoiceRepository(SqlConnection conn)
    {
        _conn = conn;
    }

    public async Task<List<InvoiceEntitie>> getInvoices(int top)
    {
        var command = $"select top {top} * from invoices";

        var result = await _conn.QueryAsync<InvoiceEntitie>(command);

        return result.ToList();
    }
}
