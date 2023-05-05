using e_TimesheetNET7.Models.Timesheet;
using e_TimesheetNET7.Repositories.Interfaces;
using e_TimesheetNET7.Usecase.Interfaces;

namespace e_TimesheetNET7.Usecase
{
    public class TimesheetUsecase : ITimesheetUsecase
    {
        private readonly ITimesheetRepository _tsRepo;
        public TimesheetUsecase(ITimesheetRepository tsRepo)
        {
            _tsRepo = tsRepo;
        }

        public async Task<TimesheetData> GetTimesheetData(string internalTsNo, string tahun)
        {
            try
            {
                var result = await _tsRepo.Timesheets().GetTimeSheetData(internalTsNo, tahun);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("Get data timehseet failed. " + ex.Message);
            }
        }

        public Task<TimesheetData> PostTimesheet(TimesheetData tsData)
        {
            throw new NotImplementedException();
        }
    }
}