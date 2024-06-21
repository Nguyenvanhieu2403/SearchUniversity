using Dapper;
using SearchUniversity.DataContext.ConnectSql;
using SearchUniversity.DataContext.Models;
using SearchUniversity.Reponsitory.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.Reponsitory
{
    public class UniversityRepons : IUniversityRepons
    {
        private readonly ConnectToSql _connectToSql;

        public UniversityRepons(ConnectToSql connectToSql)
        {
            _connectToSql = connectToSql;
        }

        public async Task<(List<University>, int)> GetAllUniversityAsync()
        {
            try
            {
                using (var connect = _connectToSql.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@totalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var result = await connect.QueryAsync<University>(
                        "GetAllUniversity",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    int totalRecords = parameters.Get<int>("@totalRecords");
                    return (result.ToList(), totalRecords);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> AddUniversityAsync(University university, string token)
        {
            string result = "";
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
                    var parameters = new DynamicParameters();
                    SqlCommand command = new SqlCommand();
                    var command1 = connect.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AddUniversity";
                    Guid IdArea = Guid.NewGuid();
                    command.Parameters.AddWithValue("@Id", IdArea);
                    command.Parameters.AddWithValue("@Name", university.Name);
                    command.Parameters.AddWithValue("@Code", university.Code);
                    command.Parameters.AddWithValue("@Type", university.Type);
                    command.Parameters.AddWithValue("@TrainingSystem", university.TrainingSystem);
                    command.Parameters.AddWithValue("@Address", university.Address);
                    command.Parameters.AddWithValue("@PhoneNumber", university.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", university.Email);
                    command.Parameters.AddWithValue("@Website", university.Website);
                    command.Parameters.AddWithValue("@IdArea", university.IdArea);
                    command.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                    command.Parameters.AddWithValue("@IdUser", UserId);

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

        public async Task<(List<University>, int)> SearchUniversityAsync(string search)
        {
            try
            {
                using (var connect = _connectToSql.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@search", search);
                    parameters.Add("@totalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var result = await connect.QueryAsync<University>(
                        "SearchUniversity",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    int totalRecords = parameters.Get<int>("@totalRecords");
                    return (result.ToList(), totalRecords);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(List<University>, int)> gethUniversityByIdAsync(Guid Id)
        {
            try
            {
                using (var connect = _connectToSql.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", Id);
                    parameters.Add("@totalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var result = await connect.QueryAsync<University>(
                        "GetUniversityById",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    int totalRecords = parameters.Get<int>("@totalRecords");
                    return (result.ToList(), totalRecords);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<University>> GetUniversityArea(Guid IdArea)
        {
            try
            {
                using var connection = (SqlConnection)_connectToSql.CreateConnection();
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@IdArea", IdArea);
                var result = await connection.QueryAsync<University>("GetUniversityArea", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<string> DeleteUniversityAsync(Guid Id)
        {
            string result = null;
            try
            {
                using (var connect = _connectToSql.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    var command1 = connect.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteUniversity";
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

        public async Task<string> UpdateUniversityAsync(University university, string token)
        {
            string result = "";
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
                    var parameters = new DynamicParameters();
                    SqlCommand command = new SqlCommand();
                    var command1 = connect.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateUniversity";
                    command.Parameters.AddWithValue("@Id", university.Id);
                    command.Parameters.AddWithValue("@Name", university.Name);
                    command.Parameters.AddWithValue("@Code", university.Code);
                    command.Parameters.AddWithValue("@Type", university.Type);
                    command.Parameters.AddWithValue("@TrainingSystem", university.TrainingSystem);
                    command.Parameters.AddWithValue("@Address", university.Address);
                    command.Parameters.AddWithValue("@PhoneNumber", university.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", university.Email);
                    command.Parameters.AddWithValue("@Website", university.Website);
                    command.Parameters.AddWithValue("@IdArea", university.IdArea);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@IdUser", UserId);

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

        public async Task<(List<University>, int)> SearchUniversityMajorAsync(string search)
        {
            try
            {
                using (var connect = _connectToSql.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@search", search);
                    parameters.Add("@totalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    var result = await connect.QueryAsync<University>(
                        "SearchUniversityMajor",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    int totalRecords = parameters.Get<int>("@totalRecords");
                    return (result.ToList(), totalRecords);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
