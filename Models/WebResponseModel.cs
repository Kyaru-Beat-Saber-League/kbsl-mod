namespace KBSL_MOD.Models
{
    public class WebResponseModel<T>
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}