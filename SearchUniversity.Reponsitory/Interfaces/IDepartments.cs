using SearchUniversity.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.Reponsitory.Interfaces
{
    public interface IDepartments
    {
        public Task<string> AddDepartmentsAsync(Departments departments, string token);
        public Task<List<Departments>> GetAllDepartmentssAsync();
        public Task<List<Departments>> GetDepartmentssOnlyAsync(Guid IdUniversity);
        public Task<Departments> GetDepartmentsByIdAsync(Guid Id);
        public Task<List<Departments>> GetDepartmentsByIdUniversityAsync(Guid IdUniversity);
        public Task<string> UpdateDepartmentsAsync(Departments departments, string token);
        public Task<string> DeleteDepartmentsAsync(Guid Id);
        public Task<List<string>> GetAllDepartmentMajorAsync();
    }
}
