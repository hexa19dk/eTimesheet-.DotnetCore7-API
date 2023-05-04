using System.Data.Odbc;

namespace e_TimesheetNET7.Config.Interfaces
{
    public interface IConnectionFactoryDb
    {
        public Task<OdbcConnection> CreateODBCConnectionAsync();
    }
}
