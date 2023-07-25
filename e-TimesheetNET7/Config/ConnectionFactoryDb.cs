using System.Data.Odbc;

namespace e_TimesheetNET7.Config
{
    public interface IConnectionFactoryDb
    {
        public Task<OdbcConnection> CreateODBCConnectionAsync();
        public Task<OdbcConnection> GbLimoConnection();
    }
    
    public class ConnectionFactoryDb : IConnectionFactoryDb
    {
        private readonly string _odbcConnectionString;
        private readonly string _gbLimoConnString;
        public ConnectionFactoryDb(string ConnectionString, string gbLimoConnString)
        {
            _odbcConnectionString = ConnectionString;
            _gbLimoConnString = gbLimoConnString;
        }

        public async Task<OdbcConnection> CreateODBCConnectionAsync()
        {
            var connection = new OdbcConnection(_odbcConnectionString);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<OdbcConnection> GbLimoConnection()
        {
            var connection = new OdbcConnection(_gbLimoConnString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
