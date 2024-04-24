using JustclickCoreModules.HttpClients;
using JustclickCoreModules.Responses;
using JustclickCoreModules.Utils;
using MagicVilla_DB.Models.Responses;

namespace MagicVilla_DB.Services.Proxies
{
    public class CouponProxyService
    {
        private readonly IHttpClientService<List<Coupon>> httpClient;
        private string _COUPON_SERVICE;
        public CouponProxyService(HttpClientService<List<Coupon>> _httpClient)
        {
            httpClient = _httpClient;
            _COUPON_SERVICE = EVUtil.GetValue("COUPON_SERVICE");
        }

        public async Task<BaseResponse<List<Coupon>>> GetCoupons()
        {
            HttpClientRequest request = new HttpClientRequest();
            request.ApiType = ApiType.GET;
            request.RequestUrl = _COUPON_SERVICE + "/api/v1/proxies/town/all";
            BaseResponse<List<Coupon>> result = await httpClient.SendAsync(request);

            return result;
        }
    }
}
