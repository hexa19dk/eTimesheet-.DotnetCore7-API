using e_TimesheetNET7.Config.Interfaces;
using e_TimesheetNET7.Models.Contract;
using e_TimesheetNET7.Models.Timesheet;
using e_TimesheetNET7.Repositories.Interfaces;

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

        public Task<bool> InsertTimesheet(TimesheetData tsData)
        {
            throw new NotImplementedException();
        }
    }
}
