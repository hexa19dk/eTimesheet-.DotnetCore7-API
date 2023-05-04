using e_TimesheetNET7.Repositories.Interfaces;

namespace e_TimesheetNET7.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IDriverDb _driverDb;
        public DriverRepository(IDriverDb driverDb) 
        {
            _driverDb = driverDb;
        }

        public IDriverDb Drivers()
        {
            return _driverDb;
        }
    }
}
