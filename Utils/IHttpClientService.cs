using MagicVilla_DB.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_DB.Utils
{
    public enum ApiType {
        GET,
        POST,
        PUT,
        DELETE
    }


    public class HttpClientRequest
    {
        public ApiType ApiType { get; set; }
        public string RequestUrl { get; set; }
        public object Data {get; set; }

    }

    public interface IHttpClientService<T>
    {
        Task<BaseResponse<T>>? SendAsync(HttpClientRequest request);
    }
}
