using Simplest.Backend.API.Domain;
using Dapper;
using Microsoft.Data.SqlClient;
// using Microsoft.
namespace Simplest.Backend.API.Infrastructure;

public class InvoiceRepository : IInvoiceRepository<InvoiceEntitie>
{
    private readonly SqlConnection _conn;

    public InvoiceRepository(SqlConnection conn)
    {
        _conn = conn;
    }

    public async Task<List<InvoiceEntitie>> getInvoices(InvoiceProcessType type, int rows, int pages, int companyId)
    {
        var _type = (int)type;

        var sql = @"
            SELECT CEILING(COUNT(*) * 1.0 / @rowsVal) AS total_pages
            FROM invoices
            WHERE IsProcessed = @type
            AND CompanyID = @companyId
            AND IsDeleted = 0;
        ";

        var resultTotalPages = await _conn.QuerySingleAsync<int>(sql, new { 
            type = (int)type,
            companyId = companyId,
            rowsVal = rows
        });

        var commandGetEnableInvoiceData = $@"
            WITH cte_error AS
            (
            SELECT i.*, (
                SELECT SUM(IsError)
                FROM invoice_detail id
                WHERE id.InvoiceID = i.InvoiceID
                AND id.IsDeleted = 0
            ) AS 'total_error'
            FROM invoices i
            WHERE i.IsDeleted=0
            )
            SELECT InvoiceID AS 'id',
            total_error AS 'errors'
            FROM cte_error
            WHERE IsProcessed = @type
            AND CompanyID = @company
            ORDER BY
            CASE 
                WHEN @type = 1 THEN ProcessedDate 
                ELSE NULL 
            END DESC,
            CASE 
                WHEN @type <> 1 THEN InvoiceID 
                ELSE NULL 
            END DESC
            OFFSET (@pageVal-1)*@rowsVal ROWS
            FETCH NEXT @rowsVal ROWS ONLY
        ";

        var resultGetEnableInvoices = await _conn.QueryAsync<int>(commandGetEnableInvoiceData);

        var commandGetInvoices = $@"
                WITH invoice_counts AS (
            SELECT 
                InvoiceID,
                COUNT(*) AS total_item,
                COUNT(CASE WHEN RawJSON IS NOT NULL THEN 1 END) AS total_item_processed,
                COUNT(CASE WHEN RawJSONReceiving IS NOT NULL THEN 1 END) AS total_item_processed_receiving,
                SUM(CASE WHEN IsError = 1 THEN 1 ELSE 0 END) AS total_errors
            FROM invoice_detail 
            WHERE 
                InvoiceID IN ({""}) 
            AND IsDeleted = 0
            GROUP BY InvoiceID
            ),
            robot_pending AS (
            SELECT 
                id.InvoiceID,
                COUNT(*) AS total_pending
            FROM robot_items_assigned ria
            INNER JOIN invoice_detail id ON ria.InvoiceDetailId = id.InvoiceDetail
            WHERE ria.DeletedAt IS NULL 
                AND ria.SyncTypeId = 2 
                AND ria.ItemStatusId = 3
                AND id.InvoiceID IN ({""})
                AND id.IsDeleted = 0 
                AND id.RawJSONReceiving IS NOT NULL
            GROUP BY id.InvoiceID
            )
            SELECT 
            i.InvoiceID AS 'id',
            i.Provider AS 'provider',
            i.InvoiceNumber AS 'number',
            i.InvoiceDate AS 'date',
            i.IsProcessed AS 'processed',
            i.CreatedAt AS 'upload_date',
            i.InvoicePath AS 'path',
            i.ProcessedDate AS 'processed_date',
            i.ToProcess AS 'to_process',
            i.Sync AS 'sync',
            i.Margin AS 'margin',
            i.ReadyToProcess AS 'ready_to_process',
            ISNULL(ic.total_item, 0) AS 'total_item',
            ISNULL(ic.total_item_processed, 0) AS 'total_item_processed',
            ISNULL(ic.total_item_processed_receiving, 0) AS 'total_item_processed_receiving',
            ISNULL(rp.total_pending, 0) AS 'total_item_processed_receiving_pending',
            ISNULL(ic.total_errors, 0) AS 'errors',
            b.Name AS 'branch',
            i.Notes AS 'notes',
            i.ScheduledDate AS 'scheduled_date'
            FROM invoices i
            LEFT JOIN branch b ON b.BranchID = i.BranchID
            LEFT JOIN invoice_counts ic ON ic.InvoiceID = i.InvoiceID
            LEFT JOIN robot_pending rp ON rp.InvoiceID = i.InvoiceID
            WHERE i.InvoiceID IN ({""})
            ORDER BY i.InvoiceID DESC
        ";

        var resultGetInvoices = await _conn.QueryAsync<InvoiceEntitie>(commandGetInvoices);

        return resultGetInvoices.ToList();
    }
}
