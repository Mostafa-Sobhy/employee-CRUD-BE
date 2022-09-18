namespace WebAPIs.DTOs
{
    public class APIResponse<T>
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public T data { get; set; }

    }
}
