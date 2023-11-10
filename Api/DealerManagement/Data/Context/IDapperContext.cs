using Microsoft.Data.SqlClient;
using System.Data;

namespace Data.Context
{
    public interface IDapperContext : IDisposable
    {
        IDbConnection Connection { get; }
        string ConnectionString { get; }

        SqlConnection GetOpenConnection();
    }
}
