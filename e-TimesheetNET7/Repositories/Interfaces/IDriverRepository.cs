using e_TimesheetNET7.Models;

namespace e_TimesheetNET7.Repositories.Interfaces
{
    public interface IDriverRepository
    {
        IDriverDb Drivers();
    }

    public delegate IDriverRepository DriverServiceResolver(ServiceType serviceType);
}
