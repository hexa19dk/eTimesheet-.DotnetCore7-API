using e_TimesheetNET7.Models.Driver;
using e_TimesheetNET7.Repositories.Interfaces;

namespace e_TimesheetNET7.Usecase
{
    public interface IDriverUsecase
    {
        Task<DriverItem> GetDriver(string nip);
        Task<bool> PostDriverPermit(DriverGbLimoRequest request);
    }

    public class DriverUsecase : IDriverUsecase
    {
        private readonly IDriverRepository _driverRepo;
        public DriverUsecase(IDriverRepository driverRepo)
        {
            _driverRepo = driverRepo;
        }

        public async Task<DriverItem> GetDriver(string nip)
        {
            try
            {
                var result = await _driverRepo.Drivers().GetDriver(nip);
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
    }
}
