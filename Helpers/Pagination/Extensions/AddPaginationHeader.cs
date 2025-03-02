using System.Text.Json;

namespace web.Helpers.Pagination.Extensions;
public static class HttpExtensions
{
    public static void AddPaginationResponseHeader<T>(this HttpResponse response, PagedList<T> data)
    {
        var count = data.Count();
        var totalPages = (int)Math.Ceiling(count / (double)data.PageSize);
        var paginationHeader = new PaginationHeader(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, jsonOptions));
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}
