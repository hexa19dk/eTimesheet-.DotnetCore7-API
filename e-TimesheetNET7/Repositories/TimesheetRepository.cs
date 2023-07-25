using e_TimesheetNET7.Repositories.Interfaces;
using e_TimesheetNET7.Repositories.SQL;

namespace e_TimesheetNET7.Repositories
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly ITimesheetDb _timesheetDb;
        public TimesheetRepository(ITimesheetDb timesheetDb)
        {
            _timesheetDb = timesheetDb;
        }

        public ITimesheetDb Timesheets()
        {
            return _timesheetDb;
        }
    }
}
