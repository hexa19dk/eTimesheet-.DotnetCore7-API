namespace e_TimesheetNET7.Models.Contract
{
    public class DataContract
    {
        public HeaderContract? Header { get; set; }
        public IEnumerable<DetailContract>? Detail { get; set; }
        public IEnumerable<DetailDetailContract>? DetailDetail { get; set; }
    }

    public class ContractNoRequest
    {
        public string ContractNo { get; set; }
    }
}
