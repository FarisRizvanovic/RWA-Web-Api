using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Table("UserAccount")]
public partial class UserAccount
{
    [Key]
    public int user_id { get; set; }

    [StringLength(255)]
    public string? username { get; set; }

    [StringLength(255)]
    public string? password { get; set; }

    [StringLength(255)]
    public string? role { get; set; }

    [InverseProperty("user")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("user")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
