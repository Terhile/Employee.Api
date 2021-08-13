using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Employee.Api.Persistence.Repositories.Dapper
{
    public class DBContext : IDBContext,IDisposable
    {
        private  IDbConnection connection;
        private string _connectionString = string.Empty;
        private bool _disposed = false;
        public DBContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                    if (connection != null)
                        connection.Close();
   
                }
            }
            _disposed = true;
        }
        public IDbConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
            }

            return connection;
        }
    }
}
