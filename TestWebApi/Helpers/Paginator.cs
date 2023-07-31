using TestWebApi.DataTransferObjects;

namespace TestWebApi.Helpers;

public static class Paginator
{
    public static PaginatorResponseDTO<IEnumerable<T>> Pagination<T>(
        this IQueryable<T> queryable, int pageSize, int pageNumber)
    {
        var count = queryable.Count();
        var pageResult = new PaginatorResponseDTO<IEnumerable<T>>
        {
            PageSize = pageSize is > 100 or < 1 ? 10 : pageSize,
            CurrentPage = pageNumber > 1 ? pageNumber : 1,
            TotalItems = count,
        };

        pageResult.NumberOfPages = count % pageResult.PageSize != 0
            ? (count / pageResult.PageSize) + 1
            : count / pageResult.PageSize;

        pageResult.PageItems =  queryable.Skip((pageResult.CurrentPage - 1) * pageResult.PageSize)
            .Take(pageResult.PageSize)
            .ToList();

        return pageResult;
    }
}