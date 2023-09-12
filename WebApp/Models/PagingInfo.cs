namespace WebApp.Models
{
    public class PagingInfo
    {
        public int PageNumber { get; }

        public int TotalPages { get; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public PagingInfo(int pageCount, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(pageCount / (double)pageSize);
        }
    }
}
