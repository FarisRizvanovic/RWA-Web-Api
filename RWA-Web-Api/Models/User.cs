using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RWA_Web_Api.Models;

public partial class User
{
    [Key]
    public int id { get; set; }

    [StringLength(255)]
    public string username { get; set; } = null!;

    [StringLength(255)]
    public string first_name { get; set; } = null!;

    [StringLength(255)]
    public string last_name { get; set; } = null!;

    [StringLength(255)]
    public string password_salt { get; set; } = null!;

    [StringLength(255)]
    public string password_hash { get; set; } = null!;

    [Column(TypeName = "enum('admin','user')")]
    public string role { get; set; } = null!;
}
