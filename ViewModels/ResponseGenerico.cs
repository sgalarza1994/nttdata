
namespace ViewModel
{
    public class ResponseGenerico
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
    public class ResponseGenerico<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Result { get; set; }

    }
}
