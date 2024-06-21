using SearchUniversity.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.Reponsitory.Interfaces
{
    public interface IUniversityRepons
    {
        public Task<(List<University>, int)> GetAllUniversityAsync();
        public Task<(List<University>, int)> SearchUniversityAsync(string search);
        public Task<(List<University>, int)> SearchUniversityMajorAsync(string search);
        public Task<(List<University>, int)> gethUniversityByIdAsync(Guid Id);
        public Task<List<University>> GetUniversityArea(Guid IdArea);
        public Task<string> AddUniversityAsync(University university, string token);
        public Task<string> DeleteUniversityAsync(Guid Id);
        public Task<string> UpdateUniversityAsync(University university, string token);
    }
}
