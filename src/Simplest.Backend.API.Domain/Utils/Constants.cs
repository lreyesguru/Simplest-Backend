using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum InvoiceProcessType
{
    Processed = 1,
    NotProcessed = 0
}