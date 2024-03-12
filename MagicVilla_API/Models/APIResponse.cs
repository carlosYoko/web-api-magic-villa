using System.Net;

namespace MagicVilla_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }

        public bool IsSuccess { get; set; } = true;

        public List<string>? ErrorMessages { get; set; }

        public object? Result { get; set; }
    }
}