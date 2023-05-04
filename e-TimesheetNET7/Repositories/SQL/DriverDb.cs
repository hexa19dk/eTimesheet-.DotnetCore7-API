using e_TimesheetNET7.Config.Interfaces;
using e_TimesheetNET7.Models.Contract;
using e_TimesheetNET7.Models.Driver;
using e_TimesheetNET7.Repositories.Interfaces;

namespace e_TimesheetNET7.Repositories.SQL
{
    public class DriverDb : IDriverDb
    {
        private readonly IConnectionFactoryDb _connect;
        public DriverDb(IConnectionFactoryDb connect)
        {
            _connect = connect;
        }

        public async Task<HeaderContract> GetHeader(string noContract)
        {
            //var conn = await _connect.CreateODBCConnectionAsync();
            //var query = "select * from SAP_MTKontrakHeader where NoKontrak like ? ";
            throw new NotImplementedException();
        }

        public Task<DetailContract> GetDetail(string noContract)
        {
            throw new NotImplementedException();
        }

        public Task<DetailDetailContract> GetDetail2(string noContract)
        {
            throw new NotImplementedException();
        }        

        public Task<bool> InsertPermit(DriverPermit driverPermit)
        {
            throw new NotImplementedException();
        }
    }
}
