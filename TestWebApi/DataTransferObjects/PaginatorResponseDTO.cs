namespace TestWebApi.DataTransferObjects;

public class PaginatorResponseDTO<T>
{
    public T? PageItems { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int NumberOfPages { get; set; }
    public int TotalItems { get; set; }
}