namespace HealthInsuranceAPI.CoreFrameworkModel
{
    public class PageData<T>
    {
        public IEnumerable<T> DataCollection;
        public T Data;
        public string Message;
    }
}
