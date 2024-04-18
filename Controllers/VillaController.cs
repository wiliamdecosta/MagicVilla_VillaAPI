
using JustclickCoreModules.Requests;
using JustclickCoreModules.Responses;
using MagicVilla_DB.Data.Stores;
using MagicVilla_DB.Models.Requests;
using MagicVilla_DB.Services;
using MagicVilla_DB.Utils.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MagicVilla_DB.Controllers
{
    [Route("api/v1/villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly VillaService _service;
        public VillaController(VillaService service)
        {
            _service = service;
        }

        [HttpPost("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BaseResponse<List<Villa>>> GetVillas([FromBody] SearchRequest request)
        {
            Paginated<Villa> paginatedItem = _service.FetchAll(request);

            var responseData = BaseResponse<List<Villa>>.Builder()
                .Code(StatusCodes.Status200OK)
                .Message("FETCH_ALL_VILLA_LIST")
                .Data(paginatedItem.Data.ToList())
                .Page(new PageResponse()
                {
                    Total = paginatedItem.TotalCount,
                    Size = paginatedItem.PageSize,
                    TotalPage = paginatedItem.TotalPages,
                    Current = paginatedItem.PageNumber,
                })
                .Build();

            return Ok(responseData);
        }


        [HttpGet("{id:Guid}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Villa> GetVilla(Guid id)
        {
            var villa = _service.FetchOne(id);
            var responseData = BaseResponse<Villa>.Builder()
                .Code(StatusCodes.Status200OK)
                .Message("FETCH_ONE_VILLA")
                .Data(villa)
                .Build();

            return Ok(responseData);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Villa> CreateVilla([FromBody] VillaRequest villaRequest)
        {
            var fetchVilla = _service.Create(villaRequest);
            var responseData = BaseResponse<Villa>.Builder()
                .Code(StatusCodes.Status200OK)
                .Message("VILLA_CREATED")
                .Data(fetchVilla)
                .Build();

            return Ok(responseData);
        }

        [HttpPut("{id:Guid}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Villa> UpdateVilla(Guid id, [FromBody] VillaRequest villaRequest)
        {
            var villa = _service.Update(id, villaRequest);
            var responseData = BaseResponse<Villa>.Builder()
                .Code(StatusCodes.Status200OK)
                .Message("VILLA_UPDATED")
                .Data(villa)
                .Build();

            return Ok(responseData);
        }

        [HttpPost("delete", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BaseResponse<List<string>>> DeleteVilla([FromBody] DeleteRequest ids)
        {
            var deletedIds = _service.Delete(ids);
            var responseData = BaseResponse<List<string>>.Builder()
                .Code(StatusCodes.Status200OK)
                .Message("VILLA_DELETED")
                .Data(deletedIds)
                .Build();

            return Ok(responseData);
        }

    }
}
