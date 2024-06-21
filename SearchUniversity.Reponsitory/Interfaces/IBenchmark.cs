using SearchUniversity.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.Reponsitory.Interfaces
{
    public interface IBenchmark
    {
        public Task<string> AddBenchmarkAsync(Benchmark benchmark, string token);
        public Task<List<Benchmark>> GetAllBenchmarksAsync();
        public Task<List<BenchmarkAdmin>> GetAllBenchmarksAdminAsync();
        public Task<Benchmark> GetBenchmarkByIdAsync(Guid Id);
        public Task<List<Benchmark>> GetBenchmarkByIdUniversityAsync(Guid Id);
        public Task<string> UpdateBenchmarkAsync(Benchmark benchmark, string token);
        public Task<string> DeleteBenchmarkAsync(Guid Id);
        public Task<List<BenchmarkDisplay>> GetAllBenchmarkDisplaysAsync(Guid IdUnversity, int Year);
    }
}
