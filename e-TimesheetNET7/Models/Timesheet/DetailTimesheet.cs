using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_TimesheetNET7.Models.Timesheet
{
    [Table("SAP_TrTimeSheetDetail")]
    public class DetailTimesheet
    {
        [Key]
        [Column(Order = 0)]
        public string? InternalTsNo { get; set; }
        [Key]
        [Column(Order = 1)]
        public string? Tahun { get; set; }
        public string? AppCode { get; set; }
        public string? Pool { get; set; }
        [Key]
        [Column(Order = 2)]
        public string? TglMulai { get; set; }
        public string? JamMulai { get; set; }
        public string? JamSelesai { get; set; }
        public string? UsageType { get; set; }
        public string? NoSIO { get; set; }
        public string? NoSO { get; set; }
        public string? NoLambung { get; set; }
        public string? NoEquipment { get; set; }
        public string? KodeHarga { get; set; }
        public string? FlagExtraHariRaya { get; set; }
        public string? HariExtraInap { get; set; }
        public int? PersenKomisi { get; set; }
        public int? PersenDongkrak { get; set; }
        public string? CreationDate { get; set; }
        public string? CreationTime { get; set; }
        public string? CreationUser { get; set; }
        public string? LastUpdateDate { get; set; }
        public string? LastUpdateTime { get; set; }
        public string? LastUpdateUser { get; set; }
        public string? Hari { get; set; }
        public string? NoPolisiLama { get; set; }
        public string? NIP { get; set; }
        //public string? rowguid { get; set; }
        public string? KodeArea { get; set; }
    }
}
