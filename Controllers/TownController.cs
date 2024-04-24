using AutoMapper;
using JustclickCoreModules.Filters;
using JustclickCoreModules.Requests;
using JustclickCoreModules.Responses;
using MagicVilla_DB.Data;
using MagicVilla_DB.Data.Stores;
using MagicVilla_DB.Models.Requests;
using MagicVilla_DB.Services;
using MagicVilla_DB.Utils.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_DB.Controllers
{
    [Route("api/v1/town")]
    [ApiController]
    public class TownController : ControllerBase
    {
        private readonly TownService _townService;

        public TownController(TownService townService)
        {
            _townService = townService;
        }

        [HttpPost("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BaseResponse<List<Town>>> GetTowns([FromBody] SearchRequest searchRequest)
        {

            Paginated<Town> paginatedItem = _townService.FetchAll(searchRequest);
            var responseData = BaseResponse<List<Town>>.Builder()
                .Code(StatusCodes.Status200OK)
                .Message("FETCH_ALL_TOWN_LIST")
                .Data(paginatedItem.Data.ToList())
                .Page(new PageResponse() {
                    Total = paginatedItem.TotalCount,
                    Size = paginatedItem.PageSize,
                    TotalPage = paginatedItem.TotalPages,
                    Current = paginatedItem.PageNumber,
                })
                .Build();

            return Ok(responseData);
        }

        [HttpGet("{id:Guid}", Name = "GetTown")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BaseResponse<Villa>> GetTown(Guid id)
        {
            Town town = _townService.FetchOne(id);

            var responseData = BaseResponse<Town>.Builder()
                .Code(StatusCodes.Status200OK)
                .Message("FETCH_ONE_TOWN")
                .Data(town)
                .Build();

            return Ok(responseData);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BaseResponse<Town>> CreateTown([FromBody] TownRequest townRequest)
        {
            var town = _townService.Create(townRequest);
            var responseData = BaseResponse<Town>.Builder()
             .Code(StatusCodes.Status200OK)
             .Message("TOWN_CREATED")
             .Data(town)
             .Build();

            return Ok(responseData);

        }

        [HttpPost("delete", Name = "DeleteTown")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BaseResponse<List<string>>> DeleteTown([FromBody] DeleteRequest ids)
        {
            List<string> deletedIds = _townService.Delete(ids);

            var responseData = BaseResponse<List<string>>.Builder()
                .Code(StatusCodes.Status200OK)
                .Message("VILLA_DELETED")
                .Data(deletedIds)
                .Build();

            return Ok(responseData);
        }

    }
}
