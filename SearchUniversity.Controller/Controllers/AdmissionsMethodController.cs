using Microsoft.AspNetCore.Mvc;
using SearchUniversity.DataContext.Models;
using SearchUniversity.Reponsitory.Interfaces;

namespace SearchUniversity.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionsMethodController : ControllerBase
    {
        private readonly IAdmissionsMethod _admissionsMethod;

        public AdmissionsMethodController(IAdmissionsMethod admissionsMethod)
        {
            _admissionsMethod = admissionsMethod;
        }

        [HttpPost("AddAdmissionsMethod")]
        public async Task<MethodResult> AddAdmissionsMethod(AdmissionsMethod admissionsMethod, string Token)
        {
            var result = await _admissionsMethod.AddAdmissionsMethodAsync(admissionsMethod, Token);
            if (result == "Phương thức tuyển sinh này đã tồn tại")
            {
                return MethodResult.ResultWithError(result, 409, "Error", 0);
            }
            if (result == "Thêm phương thức tuyển sinh thất bại")
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
        public async Task<MethodResult> GetAllAdmissionsMethods()
        {
            var result = await _admissionsMethod.GetAllAdmissionsMethodsAsync();
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("GetById")]
        public async Task<MethodResult> GetAdmissionsMethodById(Guid Id)
        {
            var result = await _admissionsMethod.GetAdmissionsMethodByIdAsync(Id);
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("GetByIdUniversity")]
        public async Task<MethodResult> GetAdmissionsMethodByIdUniversity(Guid Id)
        {
            var result = await _admissionsMethod.GetAdmissionsMethodByIdUniversityAsync(Id);
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpPut("UpdateAdmissionsMethod")]
        public async Task<MethodResult> UpdateAdmissionsMethod(AdmissionsMethod admissionsMethod, string Token)
        {
            var result = await _admissionsMethod.UpdateAdmissionsMethodAsync(admissionsMethod, Token);
            if (result == "Sửa Phương thức tuyển sinh không thành công")
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            if (result == "Token không hợp lệ")
            {
                return MethodResult.ResultWithError(result, 401, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpDelete("DeleteAdmissionsMethod")]
        public async Task<MethodResult> DeleteAdmissionsMethod(Guid Id)
        {
            var result = await _admissionsMethod.DeleteAdmissionsMethodAsync(Id);
            if (result == "Xóa Phương thức tuyển sinh không thành công")    
            {
                return MethodResult.ResultWithError(result, 404, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }
    }
}
