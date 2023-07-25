using e_TimesheetNET7.Models;
using e_TimesheetNET7.Repositories.SQL;

namespace e_TimesheetNET7.Repositories.Interfaces
{
    public interface ITimesheetRepository
    {
        ITimesheetDb Timesheets();
    }

    public delegate ITimesheetRepository TimesheetContractResolver(ServiceType serviceType);
}
