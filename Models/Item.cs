using MYChamp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MYChamp.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ItemName { get; set; }

        [StringLength(250)]
        public string ItemDescription { get; set; }

        [Range(0, 1000000)]
        public decimal Price { get; set; }
    }
}
public class Invoice
{
    [Column("id")]
    public int Id { get; set; }
    [Column("customername")]
    public string CustomerName { get; set; }
    [Column("location")]
    public string Location { get; set; }

    [Column("itemspurchased")]
    public string Itemspurchased { get; set; }

    [Column("itemscosts")]
    public string Itemscosts { get; set; }
    [Column("companygstnumber")]
    public string CompanyGSTNumber { get; set; }
    [Column("invoicenumber")]
    public string InvoiceNumber { get; set; }
    [Column("date")]
    public DateTime Date { get; set; }

    public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    [Column("subtotal")]
    public decimal SubTotal { get; set; }
    [Column("discount")]
    public decimal Discount { get; set; }
    [Column("referraldiscount")]
    public decimal? ReferralDiscount { get; set; }
    [Column("totalprice")]
    public decimal TotalPrice { get; set; }
    [Column("gst")]
    public decimal GST { get; set; }
    [Column("grandtotal")]
    public decimal GrandTotal { get; set; }
    [Column("issettled")]
    public bool IsSettled { get; set; }
    [Column("paymentdate")]
    public DateTime? PaymentDate { get; set; }
    [Column("paymentmode")]
    public string? PaymentMode { get; set; }
    //public string ItemIds { get; set; }
}
// Models/InvoiceItem.cs
public class InvoiceItem
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public decimal Price { get; set; }
}

// Models/GST.cs
public class GST
{
    public int Id { get; set; }
    public decimal Percentage { get; set; }
}