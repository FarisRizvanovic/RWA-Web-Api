using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Index("order_id", Name = "order_id")]
[Index("product_id", Name = "product_id")]
public partial class OrderItem
{
    [Key]
    public int order_item_id { get; set; }

    public int? order_id { get; set; }

    public int? product_id { get; set; }

    public int? quantity { get; set; }

    [Precision(10, 2)]
    public decimal? price { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? created_at { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? updated_at { get; set; }

    [ForeignKey("order_id")]
    [InverseProperty("OrderItems")]
    public virtual Order? order { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("OrderItems")]
    public virtual Product? product { get; set; }
}
