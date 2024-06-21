using Microsoft.AspNetCore.Mvc;
using SearchUniversity.DataContext.Models;
using SearchUniversity.Reponsitory.Interfaces;

namespace SearchUniversity.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenchmarkController : ControllerBase
    {
        private readonly IBenchmark _benchmark;

        public BenchmarkController(IBenchmark benchmark)
        {
            _benchmark = benchmark;
        }

        [HttpPost("AddAdmissionsMethod")]
        public async Task<MethodResult> AddBenchmark(Benchmark benchmark, string Token)
        {
            var result = await _benchmark.AddBenchmarkAsync(benchmark, Token);
            if (result == "Điểm này đã tồn tại")
            {
                return MethodResult.ResultWithError(result, 409, "Error", 0);
            }
            if (result == "Thêm Điểm thất bại")
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
        public async Task<MethodResult> GetAllBenchmarks()
        {
            var result = await _benchmark.GetAllBenchmarksAsync();
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("GetById")]
        public async Task<MethodResult> GetBenchmarkById(Guid Id)
        {
            var result = await _benchmark.GetBenchmarkByIdAsync(Id);
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("GetByIdUniversity")]
        public async Task<MethodResult> GetBenchmarkByIdUniversity(Guid Id)
        {
            var result = await _benchmark.GetBenchmarkByIdUniversityAsync(Id);
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpPut("UpdateAdmissionsMethod")]
        public async Task<MethodResult> UpdateBenchmark(Benchmark benchmark, string Token)
        {
            var result = await _benchmark.UpdateBenchmarkAsync(benchmark, Token);
            if (result == "Sửa Điểm không thành công")
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
        public async Task<MethodResult> DeleteBenchmark(Guid Id)
        {
            var result = await _benchmark.DeleteBenchmarkAsync(Id);
            if (result == "Xóa Điểm không thành công")
            {
                return MethodResult.ResultWithError(result, 404, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("Display")]
        public async Task<MethodResult> GetBenchmarkDisplay(Guid IdUniversity, int Year)
        {
            var result = await _benchmark.GetAllBenchmarkDisplaysAsync(IdUniversity, Year);
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }

        [HttpGet("GettAllAdmin")]
        public async Task<MethodResult> GetAllBenchmarkAdmin()
        {
            var result = await _benchmark.GetAllBenchmarksAdminAsync();
            if (result == null)
            {
                return MethodResult.ResultWithError(result, 400, "Error", 0);
            }
            return MethodResult.ResultWithSuccess(result, 200, "Successfull", 0);
        }
    }
}
