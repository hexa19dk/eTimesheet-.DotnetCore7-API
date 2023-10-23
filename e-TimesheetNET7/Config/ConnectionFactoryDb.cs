using System.Data.Odbc;
using System.Formats.Asn1;

namespace e_TimesheetNET7.Config
{
    public interface IConnectionFactoryDb
    {
        public Task<OdbcConnection> CreateODBCConnectionAsync();
        public Task<OdbcConnection> GbLimoConnection();
        public Task<OdbcConnection> ServerXb01Connection();
        public Task<OdbcConnection> BbdServer07Connection();
        public Task<OdbcConnection> GetDriverIpConnection(string ipAddress);
    }
    
    public class ConnectionFactoryDb : IConnectionFactoryDb
    {
        private readonly string _odbcConnectionString;
        private readonly string _gbLimoConnString;
        private readonly string _serverXb01ConnString;
        private readonly string _bbdserver07ConnString;
        private readonly string _getDbDriverConnString;
        public ConnectionFactoryDb(string ConnectionString, string gbLimoConnString, string serverXb01ConnString, string bbdserver07ConnString, string getDbDriverConnString)
        {
            _odbcConnectionString = ConnectionString;
            _gbLimoConnString = gbLimoConnString;
            _serverXb01ConnString = serverXb01ConnString;
            _bbdserver07ConnString = bbdserver07ConnString;
            _getDbDriverConnString = getDbDriverConnString;      
            
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

        public async Task<OdbcConnection> ServerXb01Connection()
        {
            var connection = new OdbcConnection(_serverXb01ConnString);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<OdbcConnection> BbdServer07Connection()
        {
            try
            {
                var connection = new OdbcConnection(_bbdserver07ConnString);
                await connection.OpenAsync();
                return connection;
            }
            catch
            {
                throw;
            }            
        }

        public async Task<OdbcConnection> GetDriverIpConnection(string ipAddress)
        {
            var dbDriver = _getDbDriverConnString.Replace("{Ip Address}", ipAddress);
            var connection = new OdbcConnection(dbDriver);
            await connection.OpenAsync();
            return connection;
        }
    }
}
