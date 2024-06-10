using SearchUniversity.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.Reponsitory.Interfaces
{
    public interface IAdmissionsMethod
    {
        public Task<string> AddAdmissionsMethodAsync(AdmissionsMethod admissionsMethod, string token);
        public Task<List<AdmissionsMethod>> GetAllAdmissionsMethodsAsync();
        public Task<AdmissionsMethod> GetAdmissionsMethodByIdAsync(Guid Id);
        public Task<List<AdmissionsMethod>> GetAdmissionsMethodByIdUniversityAsync(Guid Id);
        public Task<string> UpdateAdmissionsMethodAsync(AdmissionsMethod admissionsMethod, string token);
        public Task<string> DeleteAdmissionsMethodAsync(Guid Id);
    }
}
