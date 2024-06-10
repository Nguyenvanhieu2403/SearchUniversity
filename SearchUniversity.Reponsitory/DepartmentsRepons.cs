﻿using SearchUniversity.DataContext.ConnectSql;
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
    public class DepartmentsRepons : IDepartments
    {
        private readonly ConnectToSql _connectToSql;

        public DepartmentsRepons(ConnectToSql connectToSql)
        {
            _connectToSql = connectToSql;
        }

        public async Task<string> AddDepartmentsAsync(Departments departments, string token)
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
                    command.CommandText = "AddDepartments";
                    Guid IdAdmissionsMethod = Guid.NewGuid();
                    command.Parameters.AddWithValue("@Id", IdAdmissionsMethod);
                    command.Parameters.AddWithValue("@Name", departments.Name);
                    command.Parameters.AddWithValue("@Code", departments.Code);
                    command.Parameters.AddWithValue("@AdmissionGroup", departments.AdmissionGroup);
                    command.Parameters.AddWithValue("@Tuition", departments.Tuition);
                    command.Parameters.AddWithValue("@IdUniversity", departments.IdUniversity);
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

        public async Task<string> DeleteDepartmentsAsync(Guid Id)
        {
            string result = null;
            try
            {
                using (var connect = _connectToSql.CreateConnection())
                {
                    SqlCommand command = new SqlCommand();
                    var command1 = connect.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteDepartments";
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

        public async Task<List<Departments>> GetAllDepartmentssAsync()
        {
            try
            {
                using var connect = _connectToSql.CreateConnection();
                using SqlCommand command = new();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllDepartments";
                command.Connection = (SqlConnection)connect;
                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Departments> departments = new List<Departments>();
                while (reader.Read())
                {
                    Departments department = new Departments();
                    if (!reader.IsDBNull(0))
                    {
                        department.Id = reader.GetGuid(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        department.Name = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        department.Code = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        department.AdmissionGroup = reader.GetString(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        department.Tuition = reader.GetDecimal(4);
                    }
                    if (!reader.IsDBNull(5))
                    {
                        department.IdUniversity = reader.GetGuid(5);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        department.CreateBy = reader.GetGuid(6);
                    }
                    if (!reader.IsDBNull(7))
                    {
                        department.CreateDate = reader.GetDateTime(7);
                    }
                    if (!reader.IsDBNull(8))
                    {
                        department.ModifiedBy = reader.GetGuid(8);
                    }
                    if (!reader.IsDBNull(9))
                    {
                        department.ModifiedDate = reader.GetDateTime(9);
                    }
                    departments.Add(department);
                }
                return departments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Departments> GetDepartmentsByIdAsync(Guid Id)
        {
            try
            {
                using var connect = _connectToSql.CreateConnection();
                using SqlCommand command = new();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetDepartmentsById";
                command.Parameters.AddWithValue("@Id", Id);
                command.Connection = (SqlConnection)connect;
                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                Departments department = new Departments();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        department.Id = reader.GetGuid(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        department.Name = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        department.Code = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        department.AdmissionGroup = reader.GetString(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        department.Tuition = reader.GetDecimal(4);
                    }
                    if (!reader.IsDBNull(5))
                    {
                        department.IdUniversity = reader.GetGuid(5);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        department.CreateBy = reader.GetGuid(6);
                    }
                    if (!reader.IsDBNull(7))
                    {
                        department.CreateDate = reader.GetDateTime(7);
                    }
                    if (!reader.IsDBNull(8))
                    {
                        department.ModifiedBy = reader.GetGuid(8);
                    }
                    if (!reader.IsDBNull(9))
                    {
                        department.ModifiedDate = reader.GetDateTime(9);
                    }
                }
                return department;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Departments>> GetDepartmentsByIdUniversityAsync(Guid IdUniversity)
        {
            try
            {
                using var connect = _connectToSql.CreateConnection();
                using SqlCommand command = new();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetDepartmentsByIdUniversity";
                command.Parameters.AddWithValue("@IdUniversity", IdUniversity);
                command.Connection = (SqlConnection)connect;
                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Departments> departments = new List<Departments>();
                while (reader.Read())
                {
                    Departments department = new Departments();
                    if (!reader.IsDBNull(0))
                    {
                        department.Id = reader.GetGuid(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        department.Name = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2))
                    {
                        department.Code = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3))
                    {
                        department.AdmissionGroup = reader.GetString(3);
                    }
                    if (reader.IsDBNull(4))
                    {
                        department.Tuition = reader.GetDecimal(4);
                    }
                    if (!reader.IsDBNull(5))
                    {
                        department.IdUniversity = reader.GetGuid(5);
                    }
                    if (!reader.IsDBNull(6))
                    {
                        department.CreateBy = reader.GetGuid(6);
                    }
                    if (!reader.IsDBNull(7))
                    {
                        department.CreateDate = reader.GetDateTime(7);
                    }
                    if (!reader.IsDBNull(8))
                    {
                        department.ModifiedBy = reader.GetGuid(8);
                    }
                    if (!reader.IsDBNull(9))
                    {
                        department.ModifiedDate = reader.GetDateTime(9);
                    }
                    departments.Add(department);
                }   
                return departments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> UpdateDepartmentsAsync(Departments departments, string token)
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
                    command.CommandText = "UpdateDepartments";
                    Guid IdAdmissionsMethod = Guid.NewGuid();
                    command.Parameters.AddWithValue("@Id", IdAdmissionsMethod);
                    command.Parameters.AddWithValue("@Name", departments.Name);
                    command.Parameters.AddWithValue("@Code", departments.Code);
                    command.Parameters.AddWithValue("@AdmissionGroup", departments.AdmissionGroup);
                    command.Parameters.AddWithValue("@Tuition", departments.Tuition);
                    command.Parameters.AddWithValue("@IdUniversity", departments.IdUniversity);
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
