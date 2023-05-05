using Dapper;
using e_TimesheetNET7.Config.Interfaces;
using e_TimesheetNET7.Models.Contract;
using e_TimesheetNET7.Models.Timesheet;
using e_TimesheetNET7.Repositories.Interfaces;
using System.Diagnostics.Contracts;
using System.Reflection.PortableExecutable;

namespace e_TimesheetNET7.Repositories.SQL
{
    public class TimesheetDb : ITimesheetDb
    {
        private readonly IConnectionFactoryDb _connect;
        public TimesheetDb(IConnectionFactoryDb connect)
        {
            _connect = connect;
        }

        public Task<DetailDetailContract> GetDetail2Contract(string contractNo, string itemNo, string detailDetail)
        {
            throw new NotImplementedException();
        }

        public Task<DetailContract> GetDetailContract(string contractNo, string itemNo)
        {
            throw new NotImplementedException();
        }

        public Task<HeaderContract> GetHeaderContract(string contractNo)
        {
            throw new NotImplementedException();
        }

        public async Task<HeaderTimesheet> GetTimesheetHeader(string internalTsNo, string tahun)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            var query = "SELECT * FROM SAP_TRTimeSheetHeader WHERE InternalTSNo=? AND Tahun=?";
            var param = new DynamicParameters();
            param.Add("InternalTSNo", internalTsNo);
            param.Add("Tahun", tahun);
            var tsHeader = await conn.QueryFirstOrDefaultAsync<HeaderTimesheet>(query, param);

            return tsHeader;
        }

        public async Task<DetailTimesheet> GetDetailTimesheet(string internalTsNo, string tahun)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            var query = "SELECT * FROM SAP_TRTimeSheetDetail WHERE InternalTSNo=? AND Tahun=?";
            var param = new DynamicParameters();
            param.Add("InternalTSNo", internalTsNo);
            param.Add("Tahun", tahun);
            var detailTs = await conn.QueryFirstOrDefaultAsync<DetailTimesheet>(query, param);

            return detailTs;
        }

        public async Task<TimesheetData> GetTimeSheetData(string internalTsNo, string tahun)
        {
            var headerTs = await GetTimesheetHeader(internalTsNo, tahun);
            var detailTs = await GetDetailTimesheet(internalTsNo, tahun);

            var tsData = new TimesheetData
            {
                Header = headerTs,
                Detail = detailTs
            };

            return tsData;
        }

        public Task<bool> InsertTimesheet(TimesheetData tsData)
        {
            throw new NotImplementedException();
        }
    }
}
