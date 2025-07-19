namespace HealthInsuranceService.CoreFrameworkModel
{
    public class PaginationData<T>
    {
        public int TotalCount;
        public IEnumerable<T> Data;
        public int PageSize;
        public int PageNumber;
    }
}
