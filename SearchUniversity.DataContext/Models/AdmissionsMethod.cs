using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.DataContext.Models
{
    public class AdmissionsMethod
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid IdUniversity { get; set; }

    }
}
