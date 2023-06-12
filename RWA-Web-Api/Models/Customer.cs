using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

public partial class Customer
{
    [Key]
    public int customer_id { get; set; }

    [StringLength(255)]
    public string? name { get; set; }

    [StringLength(20)]
    public string? phone_number { get; set; }

    [Column(TypeName = "text")]
    public string? address { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? created_at { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? updated_at { get; set; }

    [InverseProperty("customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
