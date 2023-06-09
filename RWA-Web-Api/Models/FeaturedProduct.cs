using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Table("FeaturedProduct")]
[Index("product_id", Name = "product_id")]
public partial class FeaturedProduct
{
    [Key]
    public int featured_product_id { get; set; }

    public int? product_id { get; set; }

    [StringLength(255)]
    public string? algorithm { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("FeaturedProducts")]
    public virtual Product? product { get; set; }
}
