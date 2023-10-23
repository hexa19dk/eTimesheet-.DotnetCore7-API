namespace e_TimesheetNET7.Models.Driver
{
    public class DriverItem
    {
        public string NIP { get; set; }
        public string KdGolongan2 { get; set; }
        public string NoHp { get; set; }
    }

    public class DriverRequest
    {
        public string nip { get; set; }
        public string group_code { get; set; }
        public string mobile_phone_number { get; set; }
    }
}
