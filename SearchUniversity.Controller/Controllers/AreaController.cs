using Microsoft.AspNetCore.Mvc;
using SearchUniversity.DataContext.Models;
using SearchUniversity.Reponsitory.Interfaces;

namespace SearchUniversity.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IArea _area;

        public AreaController(IArea area)
        {
            _area = area;
        }

        [HttpPost("AddArea")]
        public async Task<MethodResult> AddArea(Area area, string Token)
        {
            var result = await _area.AddAreaAsync(area, Token);
            if(result == "Vùng này đã tồn tại")
            {
                   return MethodResult.ResultWithError(result, 409, "Error", 0);
            }
            if (result == "Thêm vùng thất bại")
            {
                   return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            if (result == "Token không hợp lệ")
            {
                   return MethodResult.ResultWithError(result, 401, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet]
        public async Task<MethodResult> GetAllAreas()
        {
            var result = await _area.GetAllAreasAsync();
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("GetById")]
        public async Task<MethodResult> GetAreaById(Guid Id)
        {
            var result = await _area.GetAreaByIdAsync(Id);
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpPut("UpdateArea")]
        public async Task<MethodResult> UpdateArea(Area area, string Token)
        {
            var result = await _area.UpdateAreaAsync(area, Token);
            if (result == "Vùng này không tồn tại")
            {
                return MethodResult.ResultWithError(result, 404, "Error", 0);
            }
            if (result == "Token không hợp lệ")
            {
                return MethodResult.ResultWithError(result, 401, "Error", 0);
            }
            if (result == "Cập nhật vùng thất bại")
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpDelete("DeleteArea")]
        public async Task<MethodResult> DeleteArea(Guid Id)
        {
            var result = await _area.DeleteAreaAsync(Id);
            if (result == "Xóa vùng thất bại")
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }
    }
}
