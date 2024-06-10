using SearchUniversity.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.Reponsitory.Interfaces
{
    public interface IArea 
    {
        public Task<string> AddAreaAsync(Area area, string token);
        public Task<List<Area>> GetAllAreasAsync();
        public Task<Area> GetAreaByIdAsync(Guid Id);
        public Task<string> UpdateAreaAsync(Area area, string token);
        public Task<string> DeleteAreaAsync(Guid Id);
    }
}
