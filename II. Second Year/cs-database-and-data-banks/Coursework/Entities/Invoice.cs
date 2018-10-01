using System.Collections.Generic;

namespace Coursework.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
    }
}
