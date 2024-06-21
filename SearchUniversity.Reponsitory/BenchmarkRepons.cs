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
    public class BenchmarkRepons : IBenchmark
    {
        private readonly ConnectToSql _connectToSql;

        public BenchmarkRepons(ConnectToSql connectToSql)
        {
            _connectToSql = connectToSql;
        }

        public async Task<string> AddBenchmarkAsync(Benchmark benchmark, string token)
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
                    command.CommandText = "AddBenchmark";
                    Guid IdAdmissionsMethod = Guid.NewGuid();
                    command.Parameters.AddWithValue("@Id", IdAdmissionsMethod);
                    command.Parameters.AddWithValue("@Point", benchmark.Point);
                    command.Parameters.AddWithValue("@Year", benchmark.Year);
                    command.Parameters.AddWithValue("@IdDepartments", benchmark.IdDepartments);
                    command.Parameters.AddWithValue("@IdUniversity", benchmark.IdUniversity);
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

        public async Task<string> DeleteBenchmarkAsync(Guid Id)
        {

            try
            {
                using var connection = (SqlConnection)_connectToSql.CreateConnection();
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id);

                var affectedRows = await connection.ExecuteAsync("DeleteBenchmark", parameters, commandType: CommandType.StoredProcedure);

                if (affectedRows > 0)
                {
                    return "Delete successful";
                }
                else
                {
                    return "Delete failed";
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                throw new Exception("An error occurred while deleting the department.", ex);
            }
            
        }

        public async Task<List<BenchmarkDisplay>> GetAllBenchmarkDisplaysAsync(Guid IdUnversity, int Year)
        {
            try
            {
                using var connection = (SqlConnection)_connectToSql.CreateConnection();
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@IdUnversity", IdUnversity);
                parameters.Add("@Year", Year);
                var result = await connection.QueryAsync<BenchmarkDisplay>("GetAllBenchmarkDisplays", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<List<BenchmarkAdmin>> GetAllBenchmarksAdminAsync()
        {
            try
            {
                using var connection = (SqlConnection)_connectToSql.CreateConnection();
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                var result = await connection.QueryAsync<BenchmarkAdmin>("GetAllBenchmarkAdmin", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch
            {
                throw new Exception();
            }
        }

        public Task<List<Benchmark>> GetAllBenchmarksAsync()
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
                List<Benchmark> benchmarks = new List<Benchmark>();
                while (reader.Read())
                {
                    Benchmark benchmark = new Benchmark();
                    if (!reader.IsDBNull(0))
                    {
                        benchmark.Id = reader.GetGuid(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        benchmark.Point = reader.GetFloat(1);
                    }
                    if (!reader.IsDBNull(2))
                    {
                        benchmark.Year = reader.GetInt32(2);
                    }
                    if (!reader.IsDBNull(3))
                    {
                        benchmark.IdDepartments = reader.GetGuid(3);
                    }
                    if (!reader.IsDBNull(4))
                    {
                        benchmark.IdUniversity = reader.GetGuid(4);
                    }
                    if (!reader.IsDBNull(5))
                    {
                        benchmark.CreateBy = reader.GetGuid(5);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        benchmark.CreateDate = reader.GetDateTime(6);
                    }
                    if (!reader.IsDBNull(7))
                    {
                        benchmark.ModifiedBy = reader.GetGuid(7);
                    }
                    if (!reader.IsDBNull(8))
                    {
                        benchmark.ModifiedDate = reader.GetDateTime(8);
                    }
                    benchmarks.Add(benchmark);
                }
                return Task.FromResult(benchmarks);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Benchmark> GetBenchmarkByIdAsync(Guid Id)
        {

            try
            {
                using var connection = (SqlConnection)_connectToSql.CreateConnection();
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id);
                var result = await connection.QueryFirstOrDefaultAsync<Benchmark>("GetBenchmarkById", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<List<Benchmark>> GetBenchmarkByIdUniversityAsync(Guid Id)
        { 
            try
            {
                using var connection = (SqlConnection)_connectToSql.CreateConnection();
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@IdUniversity", Id);
                var result = await connection.QueryAsync<Benchmark>("GetAllBenchmarkByUniversity", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<string> UpdateBenchmarkAsync(Benchmark benchmark, string token)
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
                    command.CommandText = "UpdateBenchmark";
                    command.Parameters.AddWithValue("@Id", benchmark.Id);
                    command.Parameters.AddWithValue("@Point", benchmark.Point);
                    command.Parameters.AddWithValue("@Year", benchmark.Year);
                    command.Parameters.AddWithValue("@IdDepartments", benchmark.IdDepartments);
                    command.Parameters.AddWithValue("@IdUniversity", benchmark.IdUniversity);
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
