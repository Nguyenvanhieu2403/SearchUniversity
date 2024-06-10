using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.DataContext.Models
{
    public class SchoolYear
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
