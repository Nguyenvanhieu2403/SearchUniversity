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
        public int Year { get; set; }
        public Guid IdUniversity { get; set; }
        public Guid IdDepartments { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class BenchmarkAdmin : Benchmark
    {
        public string NameUnivesity { get; set; }
        public string NameDepartment { get; set; }
    }

    public class BenchmarkDisplay 
    {
        public Guid Id { get; set; }
        public float Point1 { get; set; }
        public float Point2 { get; set; }
        public string Departments { get; set; }

    }
}
