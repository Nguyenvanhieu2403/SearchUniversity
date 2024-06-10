using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.DataContext.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public string Email { get; set; }
        public string PassWordHas { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public int UsedState { get; set; }
        public string Role { get; set; }
    }
}
