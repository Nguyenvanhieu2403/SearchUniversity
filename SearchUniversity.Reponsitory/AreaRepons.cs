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

namespace SearchUniversity.Reponsitory
{
    public class AreaRepons : IArea 
    {
        private readonly ConnectToSql _connectToSql;

        public AreaRepons(ConnectToSql connectToSql)
        {
            _connectToSql = connectToSql;
        }

        public async Task<string> AddAreaAsync(Area area, string token)
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
                    command.CommandText = "AddArea";
                    Guid IdArea = Guid.NewGuid();
                    command.Parameters.AddWithValue("@Id", IdArea);
                    command.Parameters.AddWithValue("@Name", area.Name);
                    command.Parameters.AddWithValue("@Description", area.Description);
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

        public Task<List<Area>> GetAllAreasAsync()
        {
            try
            {
                using var connect = _connectToSql.CreateConnection();
                using SqlCommand command = new();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllAreas";
                command.Connection = (SqlConnection)connect;
                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Area> areas = new List<Area>();
                while (reader.Read())
                {
                    Area area = new Area();
                    if (!reader.IsDBNull(0))
                    {
                        area.Id = reader.GetGuid(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        area.Name = reader.GetString(1);
                    }
                    if (!reader.IsDBNull(2))
                    {
                        area.Description = reader.GetString(2);
                    }
                    if (!reader.IsDBNull(3))
                    {
                        area.CreateBy = reader.GetGuid(3);
                    }
                    if (!reader.IsDBNull(4))
                    {
                        area.CreateDate = reader.GetDateTime(4);
                    }
                    if (!reader.IsDBNull(5))
                    {
                        area.ModifiedBy = reader.GetGuid(5);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        area.ModifiedDate = reader.GetDateTime(6);
                    }
                    areas.Add(area);
                }
                return Task.FromResult(areas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Area> GetAreaByIdAsync(Guid Id)
        {
            try
            {
                using var connect = _connectToSql.CreateConnection();
                using SqlCommand command = new();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAreaById";
                command.Parameters.AddWithValue("@Id", Id);
                command.Connection = (SqlConnection)connect;
                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                Area area = new Area();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        area.Id = reader.GetGuid(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        area.Name = reader.GetString(1);
                    }
                    if (!reader.IsDBNull(2))
                    {
                        area.Description = reader.GetString(2);
                    }
                    if (!reader.IsDBNull(3))
                    {
                        area.CreateBy = reader.GetGuid(3);
                    }
                    if (!reader.IsDBNull(4))
                    {
                        area.CreateDate = reader.GetDateTime(4);
                    }
                    if (!reader.IsDBNull(5))
                    {
                        area.ModifiedBy = reader.GetGuid(5);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        area.ModifiedDate = reader.GetDateTime(6);
                    }
                }
                return Task.FromResult(area);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateAreaAsync(Area area, string token)
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
                    command.CommandText = "UpdateArea";
                    command.Parameters.AddWithValue("@Id", area.Id);
                    command.Parameters.AddWithValue("@Name", area.Name);
                    command.Parameters.AddWithValue("@Description", area.Description);
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

        public async Task<string> DeleteAreaAsync(Guid Id)
        {
            string result = null;
            try
            {
                using (var connect = _connectToSql.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    var command1 = connect.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteArea";
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
    }
}
