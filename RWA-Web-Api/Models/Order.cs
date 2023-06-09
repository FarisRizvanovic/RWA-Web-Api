using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Index("user_id", Name = "user_id")]
public partial class Order
{
    [Key]
    public int order_id { get; set; }

    public int? user_id { get; set; }

    public DateOnly? order_date { get; set; }

    [Precision(10, 2)]
    public decimal? total_amount { get; set; }

    [ForeignKey("user_id")]
    [InverseProperty("Orders")]
    public virtual UserAccount? user { get; set; }
}
