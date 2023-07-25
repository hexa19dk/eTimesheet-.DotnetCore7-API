using e_TimesheetNET7.Models;
using e_TimesheetNET7.Repositories.SQL;

namespace e_TimesheetNET7.Repositories.Interfaces
{
    public interface IDriverRepository
    {
        IDriverDb Drivers();
    }

    public delegate IDriverRepository DriverServiceResolver(ServiceType serviceType);
}
