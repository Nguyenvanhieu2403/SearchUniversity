using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.DataContext.Models
{
    public class Benchmark
    {
        public Guid Id { get; set; }
        public float Point { get; set; }
        public Guid IdUniversity { get; set; }
        public Guid IdDepartment { get; set; }
        public Guid IdSchoolYear { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
