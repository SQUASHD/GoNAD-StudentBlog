namespace StudentBlogAPI.Model.DTOs;

public record PaginatedResultDto<T>(
    IEnumerable<T> Items,
    int CurrentPage,
    int PageSize,
    int TotalPages,
    int TotalItems
)
{
    public PaginatedResultDto(IEnumerable<T> items, int currentPage, int pageSize, int totalItems)
        : this(items, currentPage, pageSize, (int)Math.Ceiling(totalItems / (double)pageSize), totalItems)
    {
    }
}