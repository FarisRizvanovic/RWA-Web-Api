using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Table("Product")]
[Index("category_id", Name = "category_id")]
public partial class Product
{
    [Key]
    public int product_id { get; set; }

    [StringLength(255)]
    public string? title { get; set; }

    public bool? isNew { get; set; }

    [Precision(10, 2)]
    public decimal? oldPrice { get; set; }

    [Precision(10, 2)]
    public decimal? price { get; set; }

    [Column(TypeName = "text")]
    public string? description { get; set; }

    public int? category_id { get; set; }

    [StringLength(255)]
    public string? image { get; set; }

    [Precision(3, 2)]
    public decimal? rating { get; set; }

    [InverseProperty("product")]
    public virtual ICollection<FeaturedProduct> FeaturedProducts { get; set; } = new List<FeaturedProduct>();

    [ForeignKey("category_id")]
    [InverseProperty("Products")]
    public virtual Category? category { get; set; }
}
