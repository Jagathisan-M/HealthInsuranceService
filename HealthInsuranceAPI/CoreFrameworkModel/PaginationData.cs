namespace HealthInsuranceAPI.CoreFrameworkModel
{
    public class PaginationData<T>
    {
        public int TotalCount;
        public IEnumerable<T> Data { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
