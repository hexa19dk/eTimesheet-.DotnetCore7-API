using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_TimesheetNET7.Models.Driver
{
    [Table("LMO_TrDriverPermit")]
    public class DriverPermit
    {
        [Key]
        public int id { get; set; }
        public string? nip { get; set; }
        public string? permission_date { get; set; }
        public string? contract_number { get; set; }
        public string? item_number { get; set; }
        public string? detail_number { get; set; }
        public string? replacement_driver_nip { get; set; }
        public int status { get; set; }
    }
}
