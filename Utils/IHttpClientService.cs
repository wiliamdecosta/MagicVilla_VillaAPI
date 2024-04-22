using JustclickCoreModules.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_DB.Utils
{
    public interface IHttpClientService<T>
    {
        Task<BaseResponse<T>>? SendAsync(HttpClientRequest request);
    }
}
