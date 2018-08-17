namespace Coursework.Entities
{
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int TrackId { get; set; }
        public Track Track { get; set; }
    }
}
