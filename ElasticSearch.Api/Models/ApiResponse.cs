using System.Net;

namespace ElasticSearch.Api.Models
{
    public class ApiResponse
    {
        public List<string> Errors { get; set; } = new();
        public HttpStatusCode Status { get; set; }

        public static ApiResponse<T> Success<T>(T data)
        {
            return new ApiResponse<T> { Data = data, Status = HttpStatusCode.OK, Errors = new() };
        }

        public static ApiResponse Failure(List<string>? errors = null)
        {
            return new ApiResponse { Errors = errors ?? new(), Status = HttpStatusCode.InternalServerError };
        }
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }
    }
}
