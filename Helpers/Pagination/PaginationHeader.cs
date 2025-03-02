namespace web.Helpers.Pagination;
    public class PaginationHeader(int currentPage,int ItemPerPage, int totalItems, int totalPages)
    {
        public int CurrentPage { get; set; } = currentPage;
        public int ItemPerPage { get; set; } = ItemPerPage;
        public int TotalItems { get; set; } = totalItems;
        public int TotalPages { get; set; } = totalPages;
    }
