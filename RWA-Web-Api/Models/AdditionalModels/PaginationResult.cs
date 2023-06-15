namespace RWA_Web_Api.Models.AdditionalModels;

public class PaginationResult<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public ICollection<T>? Items { get; set; }
}