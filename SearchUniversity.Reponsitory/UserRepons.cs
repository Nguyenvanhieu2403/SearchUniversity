using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SearchUniversity.DataContext.ConnectSql;
using SearchUniversity.DataContext.Models;
using SearchUniversity.Reponsitory.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.Reponsitory
{
    public class UserRepons : IUser
    {
        private readonly ConnectToSql _connectToSql;
        private readonly IConfiguration _configuration;

        public UserRepons(ConnectToSql connectToSql, IConfiguration configuration)
        {
            _connectToSql = connectToSql;
            _configuration = configuration;
        }

        public async Task<string> SignInAsync(SignInModel signInModel)
        {
            try
            {
                var salt = PasswordManager.GenerateSalt(); // Tạo chuỗi salt mới cho mật khẩu
                var hashedPassword = PasswordManager.HashPassword(signInModel.Password, salt);
                string query = "select count(*) from dbo.Users where Email = @Email and PassWordHas = @PassWordHas";
                var parameter = new DynamicParameters();
                parameter.Add("Email", signInModel.Email, DbType.String);
                parameter.Add("PassWordHas", hashedPassword, DbType.String);

                using (var connect = _connectToSql.CreateConnection())
                {
                    var count = await connect.ExecuteScalarAsync<int>(query, parameter);
                    if (count != 0)
                    {
                        var userQuery = "SELECT * FROM dbo.Users WHERE Email = @Email";
                        var users = await connect.QueryFirstOrDefaultAsync<User>(userQuery, parameter);
                        if (users.UsedState == 0)
                        {
                            return "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ Admin để mở";
                        }
                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, signInModel.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim("UserName", (users.FirstName + " " + users.LastName)),
                            new Claim("Email", users.Email),
                            new Claim("UserId", users.Id.ToString()),
                            new Claim(ClaimTypes.Role, users.Role),
                            new Claim ("PhoneNumber", users.PhoneNumber.ToString()),
                            new Claim ("Gender", users.Gender.ToString()),
                            new Claim ("DateOfBirth", users.DateOfBirth.ToString()),
                            new Claim("Avatar", users.Avatar),

                        };
                        var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:ISK"]));
                        var token = new JwtSecurityToken(
                                issuer: _configuration["JWT:ValidIssuer"],
                                audience: _configuration["JWT:ValidAudience"],
                                expires: DateTime.Now.AddMinutes(30),
                                claims: authClaims,
                                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
                        );

                        return new JwtSecurityTokenHandler().WriteToken(token);
                    }
                    else
                    {
                        return "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SignUpAsync(SignUpModel signUpModel)
        {
            try
            {
                using var connect = _connectToSql.CreateConnection();
                var salt = PasswordManager.GenerateSalt(); // Tạo chuỗi salt mới cho mật khẩu
                var hashedPassword = PasswordManager.HashPassword(signUpModel.Password, salt); // Mã hóa mật khẩu

                var parameter = new DynamicParameters();
                parameter.Add("FirstName", signUpModel.FirstName, DbType.String);
                parameter.Add("LastName", signUpModel.LastName, DbType.String);
                parameter.Add("Email", signUpModel.Email, DbType.String);
                parameter.Add("Password", hashedPassword, DbType.String); // Lưu mật khẩu đã mã hóa
                parameter.Add("PhoneNumber", signUpModel.PhoneNumber, DbType.Int64);
                parameter.Add("Address", signUpModel.Address, DbType.String);
                parameter.Add("Roles", "Admin", DbType.String);
                parameter.Add("Gender", 1, DbType.Int32);
                parameter.Add("DateOfBirth", DateTime.Now, DbType.Date);
                parameter.Add("Avata", "", DbType.String);

                var result = await connect.QueryFirstOrDefaultAsync<string>("SignUpUser", parameter, commandType: CommandType.StoredProcedure);

                if (result == "SignUp Success")
                {
                    return "SignUp Success";
                }
                else
                {
                    return "SignUp Error";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetByIdAsync(string email)
        {
            using var connect = _connectToSql.CreateConnection();
            var searchEmail = await connect.QueryFirstOrDefaultAsync<User>("GetByEmail", new { Email = email }, commandType: CommandType.StoredProcedure);
            return searchEmail;
        }
    }
}
