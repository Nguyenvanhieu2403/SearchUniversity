using SearchUniversity.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.Reponsitory.Interfaces
{
    public  interface IUser
    {
        public Task<string> SignUpAsync(SignUpModel signUpModel);
        public Task<string> SignInAsync(SignInModel signInModel);
        public Task<User> GetByIdAsync(string email);
    }
}
