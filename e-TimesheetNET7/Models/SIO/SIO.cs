using System.ComponentModel.DataAnnotations;

namespace e_TimesheetNET7.Models.SIO
{
    public class SIO
    {
        [Required]
        public string sio_year { get; set; }
        [Required]
        public string sio_no { get; set; }
        [Required]
        public string sio_pool { get; set; }
        [Required]
        public int sio_sts { get; set; }
        [Required]
        public string sio_area { get; set; }
        [Required]
        public string driver_nip { get; set; }
        public string driver_name { get; set; }
        public string driver_phone { get; set; }
        [Required]
        public string vehicle_code { get; set; }
        [Required]
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int start_km { get; set; }
        public int finish_km { get; set; }
        public float liter_bbm { get; set; }
        public string kontrak_bbm { get; set; }
        public string pool_destination { get; set; }
        public bool showed { get; set; }
        [Required]
        public string type_sio { get; set; }
        public string description { get; set; }
    }
}
