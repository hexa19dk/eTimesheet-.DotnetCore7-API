namespace e_TimesheetNET7.Models.Timesheet
{
    public class TimesheetData
    {
        public HeaderTimesheet? Header { get; set; }
        public IEnumerable<DetailTimesheet>? Detail { get; set; }
    }
}
