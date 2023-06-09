using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Table("Customer")]
[Index("user_id", Name = "user_id")]
public partial class Customer
{
    [Key]
    public int customer_id { get; set; }

    public int? user_id { get; set; }

    [StringLength(255)]
    public string? first_name { get; set; }

    [StringLength(255)]
    public string? last_name { get; set; }

    [StringLength(255)]
    public string? email { get; set; }

    [StringLength(20)]
    public string? phone { get; set; }

    [StringLength(255)]
    public string? address { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("Customers")]
    public virtual UserAccount? user { get; set; }
}
