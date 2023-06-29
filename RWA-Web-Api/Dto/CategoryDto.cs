﻿namespace RWA_Web_Api.Models;

public class CategoryDto
{
    public int category_id { get; set; }
    public string name { get; set; } = null!;
    public string? description { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
}