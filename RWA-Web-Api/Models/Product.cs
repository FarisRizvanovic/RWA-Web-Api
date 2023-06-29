using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Index("category_id", Name = "category_id")]
public partial class Product
{
    [Key]
    public int product_id { get; set; }

    public int? category_id { get; set; }

    [StringLength(255)]
    public string title { get; set; } = null!;

    public bool? isNew { get; set; }

    [Column(TypeName = "text")]
    public string? description { get; set; }

    [Precision(3, 2)]
    public decimal? rating { get; set; }

    public int? stock { get; set; }

    [Precision(10, 2)]
    public decimal? oldPrice { get; set; }

    [Precision(10, 2)]
    public decimal? price { get; set; }

    [StringLength(255)]
    public string? image { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? created_at { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? updated_at { get; set; }

    [InverseProperty("product")]
    public virtual ICollection<FeaturedProduct> FeaturedProducts { get; set; } = new List<FeaturedProduct>();

    [InverseProperty("product")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("category_id")]
    [InverseProperty("Products")]
    public virtual Category? category { get; set; }
}
