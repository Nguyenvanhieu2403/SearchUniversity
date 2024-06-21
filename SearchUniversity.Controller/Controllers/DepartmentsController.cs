using Microsoft.AspNetCore.Mvc;
using SearchUniversity.DataContext.Models;
using SearchUniversity.Reponsitory.Interfaces;

namespace SearchUniversity.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartments _departments;
        public DepartmentsController(IDepartments departments)
        {
            _departments = departments;
        }

        [HttpPost("AddDepartments")]
        public async Task<MethodResult> AddIDepartments(Departments departments, string Token)
        {
            var result = await _departments.AddDepartmentsAsync(departments, Token);
            if (result == "Ngành học này đã tồn tại")
            {
                return MethodResult.ResultWithError(result, 409, "Error", 0);
            }
            if (result == "Thêm ngành học thất bại")
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
        public async Task<MethodResult> GetAllDepartmentss()
        {
            var result = await _departments.GetAllDepartmentssAsync();
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("GetById")]
        public async Task<MethodResult> GetDepartmentsById(Guid Id)
        {
            var result = await _departments.GetDepartmentsByIdAsync(Id);
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("GetByIdUniversity")]
        public async Task<MethodResult> GetDepartmentsByIdUniversity(Guid Id)
        {
            var result = await _departments.GetDepartmentsByIdUniversityAsync(Id);
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpPut("UpdateAdmissionsMethod")]
        public async Task<MethodResult> UpdateDepartments(Departments departments, string Token)
        {
            var result = await _departments.UpdateDepartmentsAsync(departments, Token);
            if (result == "Sửa ngành học không thành công")
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
        public async Task<MethodResult> DeleteDepartments(Guid Id)
        {
            var result = await _departments.DeleteDepartmentsAsync(Id);
            if (result == "Delete failed")
            {
                return MethodResult.ResultWithError(result, 404, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("OnlyName")]
        public async Task<MethodResult> GetOnlyDepartment(Guid IdUniversity)
        {
            var result = await _departments.GetDepartmentssOnlyAsync(IdUniversity);
            if(result == null)
            {
                return MethodResult.ResultWithError(result,404, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Success", 0);
        }

        [HttpGet("GetAllDepartmentMajor")]
        public async Task<MethodResult> GetAllDepartmentMajor()
        {
            var result = await _departments.GetAllDepartmentMajorAsync();
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 404, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Success", 0);
        }

    }
}
