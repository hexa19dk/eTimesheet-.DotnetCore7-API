using e_TimesheetNET7.Models.Timesheet;

namespace e_TimesheetNET7.Usecase.Interfaces
{
    public interface ITimesheetUsecase
    {
        Task<TimesheetData> GetTimesheetData(string internalTsNo, string tahun);
        Task<bool> PostTimesheet(TimesheetData tsData);
    }
}
