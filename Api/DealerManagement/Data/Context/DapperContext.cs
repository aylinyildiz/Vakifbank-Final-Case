using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class DapperContext : IDapperContext
    {
        private string _connectionString;
        private IConfiguration config;
        private IDbConnection _connection;

        public DapperContext(IConfiguration config)
        {
            this.config = config;
            _connectionString = config.GetConnectionString("MsSqlConnection");
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {

                    _connection = new SqlConnection(_connectionString);
                }

                if (_connection.State == ConnectionState.Closed)
                {

                    _connection.Open();
                }
                return _connection;
            }
        }
        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public void Dispose()
        {
            if(_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public SqlConnection GetOpenConnection()
        {
           var connection = new SqlConnection(_connectionString);

            connection.Open();
            return connection;
        }
    }
}
