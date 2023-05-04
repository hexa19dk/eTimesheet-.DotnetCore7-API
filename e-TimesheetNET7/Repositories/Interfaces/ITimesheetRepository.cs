using e_TimesheetNET7.Models;

namespace e_TimesheetNET7.Repositories.Interfaces
{
    public interface ITimesheetRepository
    {
        ITimesheetDb Timesheets();
    }

    public delegate ITimesheetRepository TimesheetContractResolver(ServiceType serviceType);
}
