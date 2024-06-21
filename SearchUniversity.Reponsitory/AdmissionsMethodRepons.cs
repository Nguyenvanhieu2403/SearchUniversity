using SearchUniversity.DataContext.ConnectSql;
using SearchUniversity.DataContext.Models;
using SearchUniversity.Reponsitory.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SearchUniversity.Reponsitory
{
    public class AdmissionxsMethodRepons : IAdmissionsMethod
    {
        private readonly ConnectToSql _connectToSql;

        public AdmissionxsMethodRepons(ConnectToSql connectToSql)
        {
            _connectToSql = connectToSql;
        }

        public async Task<string> AddAdmissionsMethodAsync(AdmissionsMethod admissionsMethod, string token)
        {
            string result = null;
            try
            {
                using (var connect = _connectToSql.CreateConnection())
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var decodedToken = tokenHandler.ReadJwtToken(token);
                    var userIdClaim = decodedToken.Claims.FirstOrDefault(c => c.Type == "UserId");
                    if (userIdClaim == null)
                    {
                        result = "Token không hợp lệ";
                        return result;
                    }
                    Guid UserId = Guid.Parse(userIdClaim.Value);
                    SqlCommand command = new SqlCommand();
                    var command1 = connect.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AddAdmissionsMethod";
                    Guid IdAdmissionsMethod = Guid.NewGuid();
                    command.Parameters.AddWithValue("@Id", IdAdmissionsMethod);
                    command.Parameters.AddWithValue("@Name", admissionsMethod.Name);
                    command.Parameters.AddWithValue("@IdUniversity", admissionsMethod.IdUniversity);
                    command.Parameters.AddWithValue("@CreateBy", UserId);
                    command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ModifiedBy", UserId);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    command.Connection = (SqlConnection)connect;

                    // Add the @Result parameter for the stored procedure (output parameter).
                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.NVarChar, -1);
                    resultParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultParam);
                    connect.Open(); // Open the connection before executing the command.
                    await command.ExecuteNonQueryAsync();
                    result = resultParam.Value.ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> DeleteAdmissionsMethodAsync(Guid Id)
        {
            string result = null;
            try
            {
                using (var connect = _connectToSql.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    var command1 = connect.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteAdmissionsMethod";
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Connection = (SqlConnection)connect;

                    // Add the @Result parameter for the stored procedure (output parameter).
                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.NVarChar, -1);
                    resultParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultParam);
                    connect.Open(); // Open the connection before executing the command.
                    await command.ExecuteNonQueryAsync();
                    result = resultParam.Value.ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AdmissionsMethod> GetAdmissionsMethodByIdAsync(Guid Id)
        {
            try
            {
                using var connect = _connectToSql.CreateConnection();
                using SqlCommand command = new();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAdmissionsMethodById";
                command.Parameters.AddWithValue("@Id", Id);
                command.Connection = (SqlConnection)connect;
                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                AdmissionsMethod admissionsMethod = new AdmissionsMethod();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        admissionsMethod.Id = reader.GetGuid(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        admissionsMethod.Name = reader.GetString(1);
                    }
                    if (!reader.IsDBNull(2))
                    {
                        admissionsMethod.IdUniversity = reader.GetGuid(2);
                    }
                    if (!reader.IsDBNull(3))
                    {
                        admissionsMethod.CreateBy = reader.GetGuid(3);
                    }
                    if (!reader.IsDBNull(4))
                    {
                        admissionsMethod.CreateDate = reader.GetDateTime(4);
                    }
                    if (!reader.IsDBNull(5))
                    {
                        admissionsMethod.ModifiedBy = reader.GetGuid(5);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        admissionsMethod.ModifiedDate = reader.GetDateTime(6);
                    }
                }
                return admissionsMethod;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AdmissionsMethod>> GetAdmissionsMethodByIdUniversityAsync(Guid IdUniversity)
        {
        try
        {
            using var connect = _connectToSql.CreateConnection();
            using SqlCommand command = new();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetAdmissionsMethodByIdUniversity";
            command.Parameters.AddWithValue("@IdUniversity", IdUniversity);
            command.Connection = (SqlConnection)connect;
            connect.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<AdmissionsMethod> admissionsMethods = new List<AdmissionsMethod>();
            while (reader.Read())
            {
                AdmissionsMethod admissionsMethod = new AdmissionsMethod();
                if (!reader.IsDBNull(0))
                {
                    admissionsMethod.Id = reader.GetGuid(0);
                }
                if (!reader.IsDBNull(1))
                {
                    admissionsMethod.Name = reader.GetString(1);
                }
                if (!reader.IsDBNull(2))
                {
                    admissionsMethod.IdUniversity = reader.GetGuid(2);
                }
                if (!reader.IsDBNull(3))
                {
                    admissionsMethod.CreateBy = reader.GetGuid(3);
                }
                if (!reader.IsDBNull(4))
                {
                    admissionsMethod.CreateDate = reader.GetDateTime(4);
                }
                if (!reader.IsDBNull(5))
                {
                    admissionsMethod.ModifiedBy = reader.GetGuid(5);
                }
                if (!reader.IsDBNull(6))
                {
                    admissionsMethod.ModifiedDate = reader.GetDateTime(6);
                }
                admissionsMethods.Add(admissionsMethod);
            }
            return admissionsMethods;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        }

        public async Task<List<AdmissionsMethod>> GetAllAdmissionsMethodsAdminAsync()
        {
            try
            {
                using var connection = (SqlConnection)_connectToSql.CreateConnection();
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                var result = await connection.QueryAsync<AdmissionsMethod>("GetAllBenchmarkForAdmin", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch
            {
                throw new Exception();
            }
        }

        public Task<List<AdmissionsMethod>> GetAllAdmissionsMethodsAsync()
        {
            try
            {
                using var connect = _connectToSql.CreateConnection();
                using SqlCommand command = new();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllAdmissionsMethod";
                command.Connection = (SqlConnection)connect;
                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<AdmissionsMethod> admissionsMethods = new List<AdmissionsMethod>();
                while (reader.Read())
                {
                    AdmissionsMethod admissionsMethod = new AdmissionsMethod();
                    if (!reader.IsDBNull(0))
                    {
                        admissionsMethod.Id = reader.GetGuid(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        admissionsMethod.Name = reader.GetString(1);
                    }
                    if (!reader.IsDBNull(2))
                    {
                        admissionsMethod.IdUniversity = reader.GetGuid(2);
                    }
                    if (!reader.IsDBNull(3))
                    {
                        admissionsMethod.CreateBy = reader.GetGuid(3);
                    }
                    if (!reader.IsDBNull(4))
                    {
                        admissionsMethod.CreateDate = reader.GetDateTime(4);
                    }
                    if (!reader.IsDBNull(5))
                    {
                        admissionsMethod.ModifiedBy = reader.GetGuid(5);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        admissionsMethod.ModifiedDate = reader.GetDateTime(6);
                    }
                    admissionsMethods.Add(admissionsMethod);
                }
                return Task.FromResult(admissionsMethods);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateAdmissionsMethodAsync(AdmissionsMethod admissionsMethod, string token)
        {
            string result = null;
            try
            {
                using (var connect = _connectToSql.CreateConnection())
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var decodedToken = tokenHandler.ReadJwtToken(token);
                    var userIdClaim = decodedToken.Claims.FirstOrDefault(c => c.Type == "UserId");
                    if (userIdClaim == null)
                    {
                        result = "Token không hợp lệ";
                        return result;
                    }
                    Guid UserId = Guid.Parse(userIdClaim.Value);
                    SqlCommand command = new SqlCommand();
                    var command1 = connect.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateAdmissionsMethod";
                    command.Parameters.AddWithValue("@Id", admissionsMethod.Id);
                    command.Parameters.AddWithValue("@Name", admissionsMethod.Name);
                    command.Parameters.AddWithValue("@IdUniversity", admissionsMethod.IdUniversity);
                    command.Parameters.AddWithValue("@ModifiedBy", UserId);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    command.Connection = (SqlConnection)connect;

                    // Add the @Result parameter for the stored procedure (output parameter).
                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.NVarChar, -1);
                    resultParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(resultParam);
                    connect.Open(); // Open the connection before executing the command.
                    await command.ExecuteNonQueryAsync();
                    result = resultParam.Value.ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
