﻿
using MagicVilla_DB.Data.Stores;
using MagicVilla_DB.Models.Response;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MagicVilla_DB.Utils
{
    //T adalah class response yang diharapkan
    public class HttpClientService<T> : IHttpClientService<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientService(IHttpClientFactory httpClientFactory) 
        { 
            _httpClientFactory = httpClientFactory; 
        }
        public async Task<BaseResponse<T>>? SendAsync(HttpClientRequest request)
        {
            HttpClient client = _httpClientFactory.CreateClient("ApiClient");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "applicaton/json");
            message.RequestUri = new Uri(request.RequestUrl);
            if(request.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;

            switch(request.ApiType)
            {
                case ApiType.GET:
                    message.Method = HttpMethod.Get;
                    break;
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            try
            {
                apiResponse = await client.SendAsync(message);
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return BaseResponse<T>.Builder()
                            .Code(StatusCodes.Status404NotFound)
                            .Message("Not Found")
                            .Build();

                    case HttpStatusCode.Forbidden:
                        return BaseResponse<T>.Builder()
                            .Code(StatusCodes.Status403Forbidden)
                            .Message("Forbidden")
                            .Build();

                    case HttpStatusCode.Unauthorized:
                        return BaseResponse<T>.Builder()
                            .Code(StatusCodes.Status401Unauthorized)
                            .Message("Unauthorized")
                            .Build();

                    case HttpStatusCode.InternalServerError:
                        return BaseResponse<T>.Builder()
                            .Code(StatusCodes.Status500InternalServerError)
                            .Message("Internal Server Error")
                            .Build();
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        BaseResponse<T> ? apiResponseDto = JsonConvert.DeserializeObject<BaseResponse<T>>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                return BaseResponse<T>.Builder()
                           .Code(StatusCodes.Status404NotFound)
                           .Message(ex.Message)
                           .Build();
            }

        }
    }
}
