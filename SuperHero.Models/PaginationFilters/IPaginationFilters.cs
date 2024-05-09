namespace HeroMSVC.Models.PaginationFilters
{
    public interface IPaginationFilters
    {
        string Search { get; set; }
        string OrderBy { get; set; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
