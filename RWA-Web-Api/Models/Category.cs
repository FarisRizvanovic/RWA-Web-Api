using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

public partial class Category
{
    [Key]
    public int category_id { get; set; }

    [StringLength(255)]
    public string name { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? description { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? created_at { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? updated_at { get; set; }

    [InverseProperty("category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
