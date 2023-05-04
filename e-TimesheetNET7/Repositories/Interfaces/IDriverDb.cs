using e_TimesheetNET7.Models.Contract;
using e_TimesheetNET7.Models.Driver;

namespace e_TimesheetNET7.Repositories.Interfaces
{
    public interface IDriverDb
    {
        Task<HeaderContract> GetHeader(string noContract);
        Task<DetailContract> GetDetail(string noContract);
        Task<DetailDetailContract> GetDetail2(string noContract);
        Task<bool> InsertPermit(DriverPermit driverPermit);
    }
}
