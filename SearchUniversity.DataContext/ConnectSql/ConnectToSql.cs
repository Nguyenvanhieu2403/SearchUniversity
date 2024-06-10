using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.DataContext.ConnectSql
{
    public class ConnectToSql
    {
        private readonly IConfiguration _configuration;

        public string? ConnectString { get; }

        public ConnectToSql(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectString = _configuration.GetConnectionString("connectString");
        }
        public IDbConnection CreateConnection() => new SqlConnection(ConnectString);
    }
}
