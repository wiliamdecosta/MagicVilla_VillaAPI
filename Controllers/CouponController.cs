using JustclickCoreModules.Responses;
using MagicVilla_DB.Models.Responses;
using MagicVilla_DB.Services.Proxies;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_DB.Controllers
{
    [Route("api/v1/coupon")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly CouponProxyService couponProxyService;
        public CouponController(CouponProxyService _couponProxyService) 
        { 
            couponProxyService = _couponProxyService;
        }


        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<List<Coupon>>>> GetCoupons()
        {
            BaseResponse<List<Coupon>> result = await couponProxyService.GetCoupons();
            return Ok(result);
        }
    }
}
