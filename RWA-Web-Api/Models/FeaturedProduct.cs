using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Index("product_id", Name = "product_id")]
public partial class FeaturedProduct
{
    [Key]
    public int featured_id { get; set; }

    public int? product_id { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? created_at { get; set; }

    [ForeignKey("product_id")]
    [InverseProperty("FeaturedProducts")]
    public virtual Product? product { get; set; }
}
