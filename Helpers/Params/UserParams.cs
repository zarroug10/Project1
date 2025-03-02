using web.Helpers.Pagination;

namespace web.Helpers.Params
{
    public class UserParams :PaginationParams
    {
        public string? Search { get; set; }
    }
}
