using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_TimesheetNET7.Models.Timesheet
{
    [Table("SAP_TrTimeSheetHeader")]
    public class HeaderTimesheet
    {
        [Key]
        [Column(Order = 0)]
        public string? InternalTsNo { get; set; }
        [Key]
        [Column(Order = 1)]
        public string? Tahun { get; set; }
        public string? AppCode { get; set; }
        public string? NIP { get; set; }
        public string? Pool { get; set; }
        public string? PeriodFrom { get; set; }
        public string? PeriodTo { get; set; }
        public string? JenisPengemudi { get; set; }
        public string? JamKerja { get; set; }
        public string? NoKontrak { get; set; }
        public string? NoItem { get; set; }
        public string? DetailDetail { get; set; }
        public string? NoCustomer { get; set; }
        public string? NoLambung { get; set; }
        public string? KelasKendaraan { get; set; }
        public double? KmAwal { get; set; }
        public double? KmAkhir { get; set; }
        public string? KategoriKomisi { get; set; }
        public string? NoTimesheet { get; set; }
        public string? FlagDeletion { get; set; }
        public string? Status { get; set; }
        public string? NIPReplaced { get; set; }
        public string? NoEquipment { get; set; }
        public string? CreationDate { get; set; }
        public string? CreationTime { get; set; }
        public string? CreationUser { get; set; }
        public string? LastUpdateDate { get; set; }
        public string? LastUpdateTime { get; set; }
        public string? LastUpdateUser { get; set; }
        public string? TglKirim { get; set; }
        public string? FlagKirim { get; set; }
        public string? TglRelease { get; set; }
        public string? RefInternalTSNo { get; set; }
        public string? RefTahun { get; set; }
        public string? AsalTS { get; set; }
        public string? KodeArea { get; set; }
    }
}
