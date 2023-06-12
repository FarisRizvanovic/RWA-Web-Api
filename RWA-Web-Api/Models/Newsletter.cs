using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Table("Newsletter")]
public partial class Newsletter
{
    [Key]
    public int email_id { get; set; }

    [StringLength(255)]
    public string? email { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? created_at { get; set; }
}
