using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_TimesheetNET7.Models.Driver
{
    [Table("LMO_TrDriverPermit")]
    public class DriverGbLimoRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "NIP length can't be more than 10 character")]
        public string NIP { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "NoKontrak length can't be more than 10 character")]
        public string NoKontrak { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "NoItem length can't be more than 6 character")]
        public string NoItem { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "NoDetil length can't be more than 6 character")]
        public string NoDetil { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "NIPPengganti length can't be more than 10 character")]
        public string NIPPengganti { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "TanggalIzin length can't be more than 8 character")]
        public string TanggalIzin { get; set; }

        [Required]
        public int Status { get; set; }
    }

    public class DriverTimesheetRequest
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(10)]
        public string? nip { get; set; }

        [Required]
        [MaxLength(8)]
        public string? permission_date { get; set; }

        [Required]
        [MaxLength(10)]
        public string? contract_number { get; set; }

        [Required]
        [MaxLength(6)]
        public string? item_number { get; set; }

        [Required]
        [MaxLength(6)]
        public string? detail_number { get; set; }

        [Required]
        [MaxLength(10)]
        public string? replacement_driver_nip { get; set; }

        [Required]
        public int status { get; set; }
    }
}
