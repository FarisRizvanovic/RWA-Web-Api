namespace RWA_Web_Api.Models;

public class UserDto
{
    public int id { get; set; }
    public string username { get; set; } = null!;
    public string first_name { get; set; } = null!;
    public string last_name { get; set; } = null!;
    public string role { get; set; } = null!;
}
