﻿using e_TimesheetNET7.Models.Contract;
using e_TimesheetNET7.Models.Timesheet;

namespace e_TimesheetNET7.Repositories.Interfaces
{
    public interface ITimesheetDb
    {
        Task<HeaderContract> GetHeaderContract(string contractNo);
        Task<DetailContract> GetDetailContract(string contractNo, string itemNo);
        Task<DetailDetailContract> GetDetail2Contract(string contractNo, string itemNo, string detailDetail);
        Task<bool> InsertTimesheet(TimesheetData tsData);
    }
}
