namespace ImoveisConnect.Application.Core
{
    public class PageAndSort
    {
        private readonly int _pageNumberDefault = 1;
        private readonly int _pageSizeDefault = 10;

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortEntityExp { get; set; }
        /// <summary>
        /// OBJECT OR BOOL
        /// </summary>
        public string? SortEntityExpType { get; set; } = "OBJECT";
        public string? SortDirection { get; set; }

        public PageAndSort()
        {
            PageNumber = _pageNumberDefault;
            PageSize = _pageSizeDefault;
        }

        public PageAndSort(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < _pageNumberDefault ? _pageNumberDefault : pageNumber;
            PageSize = pageSize > _pageSizeDefault ? _pageSizeDefault : pageSize;
        }

        public void Validar()
        {
            PageNumber = PageNumber < _pageNumberDefault ? _pageNumberDefault : PageNumber;
            PageSize = PageSize <= 0 ? _pageSizeDefault : PageSize;
        }
    }
}
