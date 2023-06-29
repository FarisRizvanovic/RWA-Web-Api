using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

[Table("WebsiteContent")]
public partial class WebsiteContent
{
    [Key]
    public int content_id { get; set; }

    [StringLength(255)]
    public string? cover_image { get; set; }

    [Column(TypeName = "text")]
    public string? home_description { get; set; }

    [StringLength(20)]
    public string? phone_number { get; set; }

    [StringLength(255)]
    public string? location { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? created_at { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? updated_at { get; set; }
}
