using Microsoft.AspNetCore.Mvc;
using SearchUniversity.DataContext.Models;
using SearchUniversity.Reponsitory;
using SearchUniversity.Reponsitory.Interfaces;
using System.Text.RegularExpressions;

namespace SearchUniversity.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser accRepo;

        public UsersController(IUser repo)
        {
            accRepo = repo;
        }

        [HttpPost("SignUp")]
        public async Task<MethodResult> SignUp(SignUpModel signUpModel)
        {
            string result1 = "";
            string validatePhone = @"^0\d{9}$";
            string validateEmail = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$";
            if (!Regex.IsMatch(signUpModel.PhoneNumber, validatePhone) || !Regex.IsMatch(signUpModel.Email, validateEmail))
            {
                result1 = "Số điện thoại hoặc email không hợp lệ";
                return MethodResult.ResultWithError(result1, 400, "Error", 0);
            }
            if (!Regex.IsMatch(signUpModel.Password, passwordPattern))
            {
                result1 = "Mật khẩu phải có ít nhất 8 ký tự, một chữ cái viết hoa, một chữ cái viết thường và một ký tự đặc biệt";
                return MethodResult.ResultWithError(result1, 400, "Error", 0);
            }
            if (signUpModel.Password != signUpModel.ConfirmPassword)
            {
                result1 = "Vui lòng điền hai mật khẩu giống nhau";
                return MethodResult.ResultWithError(result1, 400, "Error", 0);
            }
            var result = await accRepo.SignUpAsync(signUpModel);
            if (result == null)
            {
                result1 = "Lỗi";
                return MethodResult.ResultWithError(result1, 400, "Error", 0);
            }
            result1 = "Đăng ký thành công";
            return MethodResult.ResultWithSuccess(result1, 200, "Successfull", 0);
        }

        [HttpPost("SignIn")]
        public async Task<Custommessage> SignIn(SignInModel signInModel)
        {
            var result = await accRepo.SignInAsync(signInModel);
            var result1 = new Custommessage();
            var user = await accRepo.GetByIdAsync(signInModel.Email);
            if (user == null) return new Custommessage();
            if (string.IsNullOrEmpty(result))
            {
                throw new Exception();
            }
            if (result == "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ Admin để mở" || result == "Tên đăng nhập hoặc mật khẩu không đúng")
            {
                result1.Status = "Error";
            }
            else
            {
                result1.Status = "Thành công";
            }
            result1.Token = result;
            result1.Email = user.Email;
            result1.Id = user.Id;
            result1.Name = user.FirstName + " " + user.LastName;
            result1.Avatar = user.Avatar;
            result1.FirstName = user.FirstName;
            result1.LastName = user.LastName;
            result1.Password = user.PassWordHas;
            result1.Role = user.Role;
            return result1;
        }
    }
}
