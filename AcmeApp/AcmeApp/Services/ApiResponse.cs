using System.Net;

namespace AcmeApp.Services
{
    public record ApiResponse<T>
    {
        public HttpStatusCode Status { set; get; }
        public T Result { set; get; }
        public string Message { set; get; }
    }
}