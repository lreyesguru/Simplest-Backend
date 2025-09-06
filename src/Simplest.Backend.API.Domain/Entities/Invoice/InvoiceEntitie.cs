namespace Simplest.Backend.API.Domain;

public class InvoiceEntitie
{
    public int InvoiceID { get; set; }
    public int CompanyID { get; set; }
    public int? BranchID { get; set; }
    public string? Provider { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? InvoiceDate { get; set; }
    public short IsProcessed { get; set; }
    public short? ToProcess { get; set; }
    public short IsError { get; set; }
    public string? ErrorCode { get; set; }
    public string? InvoicePath { get; set; }
    public string? AnalysisPath { get; set; }
    public string? InvoiceDetailPath { get; set; }
    public double? Margin { get; set; }
    public string? Notes { get; set; }
    public short? BatchCreated { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public short? Sync { get; set; }
    public DateTime? SyncDate { get; set; }
    public string? ErrorCodeAnalysis { get; set; }
    public string? LogicAppIdentifier { get; set; }
    public short? IsDigital { get; set; }
    public short? IsDeleted { get; set; }
    public bool? IsPaused { get; set; }
    public short? IsChecked { get; set; }
    public int? ReadyToProcess { get; set; }
    public short? SyncCost { get; set; }
    public string? VideoURL { get; set; }
    public int? DeletedReasonID { get; set; }
    public string? DeletedReasonText { get; set; }
    public DateTime? DeletedDate { get; set; }
    public short? QuickbookSync { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? ToProcessDate { get; set; }
    public short? SyncInventory { get; set; }
    public bool? PricingSync { get; set; }
    public double? GeneralDiscount { get; set; }
    public double? Charges { get; set; }
    public short? ReceivingReady { get; set; }
    public short? CostToProcess { get; set; }
    public double? CostMultiplier { get; set; }
}
