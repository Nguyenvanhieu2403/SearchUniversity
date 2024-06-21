using Microsoft.AspNetCore.Mvc;
using SearchUniversity.DataContext.Models;
using SearchUniversity.Reponsitory.Interfaces;

namespace SearchUniversity.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepons _universityRepons;
        
        public UniversityController(IUniversityRepons universityRepons)
        {
            _universityRepons = universityRepons;
        }

        [HttpGet]
        public async Task<MethodResult> GetAllUniversity()
        {
            try
            {
                var result = await _universityRepons.GetAllUniversityAsync();
                return MethodResult.ResultWithSuccess(result.Item1, 200, "Successfull", result.Item2);
            }
            catch (Exception ex)
            {
                return MethodResult.ResultWithError(ex.Message, 400, "Error", 0);
            }
        }

        [HttpGet("SearchUniversity")]
        public async Task<MethodResult> SearchUniversity(string search)
        {
            try
            {
                var result = await _universityRepons.SearchUniversityAsync(search);
                return MethodResult.ResultWithSuccess(result.Item1, 200, "Successfull", result.Item2);
            }
            catch (Exception ex)
            {
                return MethodResult.ResultWithError(ex.Message, 400, "Error", 0);
            }
        }

        [HttpGet("gethUniversityById")]
        public async Task<MethodResult> gethUniversityById(Guid Id)
        {
            try
            {
                var result = await _universityRepons.gethUniversityByIdAsync(Id);
                return MethodResult.ResultWithSuccess(result.Item1, 200, "Successfull", result.Item2);
            }
            catch (Exception ex)
            {
                return MethodResult.ResultWithError(ex.Message, 400, "Error", 0);
            }
        }

        [HttpPost("AddUniversity")]
        public async Task<MethodResult> AddUniversity(University university, string token)
        {
            try
            {
                var result = await _universityRepons.AddUniversityAsync(university, token);
                if (result == "Trường này đã tồn tại")
                {
                    return MethodResult.ResultWithError(result, 409, "Error", 0);
                }
                if (result == "Xóa trường đại học không thành công")
                {
                    return MethodResult.ResultWithError(result, 400, "Error", 0);
                }
                if (result == "Token không hợp lệ")
                {
                    return MethodResult.ResultWithError(result, 401, "Error", 0);
                }
                return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
            }
            catch (Exception ex)
            {
                return MethodResult.ResultWithError(ex.Message, 400, "Error", 0);
            }
        }

        [HttpGet("SearchArea")]
        public async Task<MethodResult> GetUniversityArea(Guid Id)
        {
            try
            {
                var result = await _universityRepons.GetUniversityArea(Id);
                return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
            }
            catch (Exception ex)
            {
                return MethodResult.ResultWithError(ex.Message, 400, "Error", 0);
            }
        }

        [HttpDelete]
        public async Task<MethodResult> DeleteAdmissionsMethod(Guid Id)
        {
            var result = await _universityRepons.DeleteUniversityAsync(Id);
            if (result == "Xóa trường học không thành công")
            {
                return MethodResult.ResultWithError(result, 404, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpPut]
        public async Task<MethodResult> UpdateArea(University university, string Token)
        {
            var result = await _universityRepons.UpdateUniversityAsync(university, Token);
            if (result == "Trường này không tồn tại")
            {
                return MethodResult.ResultWithError(result, 404, "Error", 0);
            }
            if (result == "Token không hợp lệ")
            {
                return MethodResult.ResultWithError(result, 401, "Error", 0);
            }
            if (result == "Cập nhật trường thất bại")
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("SearchUniversityMajor")]
        public async Task<MethodResult> SearchUniversityMajor(string search)
        {
            try
            {
                var result = await _universityRepons.SearchUniversityMajorAsync(search);
                return MethodResult.ResultWithSuccess(result.Item1, 200, "Successfull", result.Item2);
            }
            catch (Exception ex)
            {
                return MethodResult.ResultWithError(ex.Message, 400, "Error", 0);
            }
        }

    }
}
