using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Index("customer_id", Name = "customer_id")]
public partial class Order
{
    [Key]
    public int order_id { get; set; }

    public int? customer_id { get; set; }

    [Precision(10, 2)]
    public decimal? total_amount { get; set; }

    [Column(TypeName = "enum('pending','processing','shipped','delivered','cancelled','returned','refunded','on hold','completed')")]
    public string status { get; set; } = null!;

    [Column(TypeName = "timestamp")]
    public DateTime? created_at { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? updated_at { get; set; }

    [InverseProperty("order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("customer_id")]
    [InverseProperty("Orders")]
    public virtual Customer? customer { get; set; }
}
