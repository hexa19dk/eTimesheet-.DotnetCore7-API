using e_TimesheetNET7.Models.Driver;
using e_TimesheetNET7.Repositories.Interfaces;

namespace e_TimesheetNET7.Usecase
{
    public interface IDriverUsecase
    {
        Task<DriverItem> GetDriver(string nip, string kdPool);
        Task<bool> PostDriverPermit(DriverGbLimoRequest request);
        //Task<bool> UpdateDriverPermit(DriverTimesheetRequest request);
        Task<bool> UpdateDriverPermit(DriverGbLimoRequest request);
    }

    public class DriverUsecase : IDriverUsecase
    {
        private readonly IDriverRepository _driverRepo;
        public DriverUsecase(IDriverRepository driverRepo)
        {
            _driverRepo = driverRepo;
        }

        public async Task<DriverItem> GetDriver(string nip, string kdPool)
        {
            try
            {
                var result = await _driverRepo.Drivers().GetDriver(nip, kdPool);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Get data driver failed. " + ex.Message);
            }
        }

        public async Task<bool> PostDriverPermit(DriverGbLimoRequest request)
        {
            try
            {
                var result = await _driverRepo.Drivers().InsertDriverPermit(request);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert driver permit data failed. Error: " + ex.Message);
            }
        }

        public async Task<bool> UpdateDriverPermit(DriverGbLimoRequest request)
        {
            try
            {
                var result = await _driverRepo.Drivers().UpdateDriverPermit(request);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed update driver permit, Error: " + ex.Message);
            }
        }
    }
}
