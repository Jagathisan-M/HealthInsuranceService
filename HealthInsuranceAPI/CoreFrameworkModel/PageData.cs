namespace HealthInsuranceAPI.CoreFrameworkModel
{
    public class PageData<T>
    {
        public IEnumerable<T> DataCollection { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
