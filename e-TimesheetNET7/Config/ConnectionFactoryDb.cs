using e_TimesheetNET7.Config.Interfaces;
using System.Data.Odbc;

namespace e_TimesheetNET7.Config
{
    public class ConnectionFactoryDb : IConnectionFactoryDb
    {
        private readonly string _odbcConnectionString;
        public ConnectionFactoryDb(string ConnectionString)
        {
            _odbcConnectionString = ConnectionString;
        }

        public async Task<OdbcConnection> CreateODBCConnectionAsync()
        {
            var connection = new OdbcConnection(_odbcConnectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
