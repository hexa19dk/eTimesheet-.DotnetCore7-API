using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_TimesheetNET7.Models.Contract
{
    [Table("SAP_MtKontrakDetailDetail")]
    public class DetailDetailContract
    {
        [Key]
        [Column(Order = 0)]
        public string? NoKontrak { get; set; }
        [Key]
        [Column(Order = 1)]
        public string? NoItem { get; set; }
        [Key]
        [Column(Order = 2)]
        public string? DetailDetail { get; set; }
        public string? NoCounter { get; set; }
        public string? NoEquipment { get; set; }
        public int Qty { get; set; }
        public string? Remarks { get; set; }
        public string? User_Name { get; set; }
        public string? User_Address { get; set; }
        public string? User_Phone { get; set; }
        public string? Driver_NIP { get; set; }
        public string? Driver_Name { get; set; }
        public string? BackToPool { get; set; }
        public string? Inc_Driver { get; set; }
        public string? Inc_Fuel { get; set; }
        public string? Usage_BBM { get; set; }
        public string? Inc_Parking { get; set; }
        public string? Inc_Toll { get; set; }
        [Column("No_Hours", TypeName = "varchar")]
        public int no_hours { get; set; }
        public string? Mon_Time { get; set; }
        public string? Tue_Time { get; set; }
        public string? Wed_Time { get; set; }
        public string? Thu_Time { get; set; }
        public string? Fri_Time { get; set; }
        public string? Sat_Time { get; set; }
        public string? Sun_Time { get; set; }
        public string? Mon { get; set; }
        public string? Tue { get; set; }
        public string? Wed { get; set; }
        public string? Thu { get; set; }
        public string? Fri { get; set; }
        public string? Sat { get; set; }
        public string? Sun { get; set; }
        public string? Komisi { get; set; }
        public string? UsageType { get; set; }
        public string? UsageText { get; set; }
        public string? ReasonRejection { get; set; }
        public string? BillToParty { get; set; }
        public string? CreationDate { get; set; }
        public string? CreationTime { get; set; }
        public string? CreationUser { get; set; }
        public string? LastUpdateDate { get; set; }
        public string? LastUpdateTime { get; set; }
        public string? LastUpdateUser { get; set; }
        public string? NoPolisi { get; set; }
        public string? NoPolisiPengganti { get; set; }
        public string? TglKeluarPool { get; set; }
        public string? JamKeluarPool { get; set; }
        public string? KdRute { get; set; }

        private string? _tglKirim;
        public string? TglKirim
        {
            get { return this._tglKirim; }
            set
            {
                this._tglKirim = value;
                if (string.IsNullOrEmpty(this._tglKirim))
                    this._tglKirim = "";
            }
        }

        private string? _jamKirim;
        public string? JamKirim
        {
            get
            { return this._jamKirim; }
            set
            {
                this._jamKirim = value;
                if (string.IsNullOrEmpty(this._jamKirim))
                    this._jamKirim = "";
            }
        }

        public string? TwoDriver { get; set; }
    }

    public class _detailDetailContract
    {
        public string? NoKontrak { get; set; }
        public string? NoItem { get; set; }
        public string? DetailDetail { get; set; }
        public string? NoCounter { get; set; }
        public string? NoEquipment { get; set; }
        public int Qty { get; set; }
        public string? Remarks { get; set; }
        public string? User_Name { get; set; }
        public string? User_Address { get; set; }
        public string? User_Phone { get; set; }
        public string? Driver_NIP { get; set; }
        public string? Driver_Name { get; set; }
        public string? BackToPool { get; set; }
        public string? Inc_Driver { get; set; }
        public string? Inc_Fuel { get; set; }
        public string? Usage_BBM { get; set; }
        public string? Inc_Parking { get; set; }
        public string? Inc_Toll { get; set; }
        public int No_Hours { get; set; }
        public string? Mon_Time { get; set; }
        public string? Tue_Time { get; set; }
        public string? Wed_Time { get; set; }
        public string? Thu_Time { get; set; }
        public string? Fri_Time { get; set; }
        public string? Sat_Time { get; set; }
        public string? Sun_Time { get; set; }
        public string? Mon { get; set; }
        public string? Tue { get; set; }
        public string? Wed { get; set; }
        public string? Thu { get; set; }
        public string? Fri { get; set; }
        public string? Sat { get; set; }
        public string? Sun { get; set; }
        public string? Komisi { get; set; }
        public string? UsageType { get; set; }
        public string? UsageText { get; set; }
        public string? ReasonRejection { get; set; }
        public string? BillToParty { get; set; }
        public string? CreationDate { get; set; }
        public string? CreationTime { get; set; }
        public string? CreationUser { get; set; }
        public string? LastUpdateDate { get; set; }
        public string? LastUpdateTime { get; set; }
        public string? LastUpdateUser { get; set; }
        public string? NoPolisi { get; set; }
        public string? NoPolisiPengganti { get; set; }
        public string? TglKeluarPool { get; set; }
        public string? JamKeluarPool { get; set; }
        public string? KdRute { get; set; }

        private string? _tglKirim;
        public string? TglKirim
        {
            get { return this._tglKirim; }
            set
            {
                this._tglKirim = value;
                if (string.IsNullOrEmpty(this._tglKirim))
                    this._tglKirim = "";
            }
        }

        private string? _jamKirim;
        public string? JamKirim
        {
            get
            { return this._jamKirim; }
            set
            {
                this._jamKirim = value;
                if (string.IsNullOrEmpty(this._jamKirim))
                    this._jamKirim = "";
            }
        }

        public string? TwoDriver { get; set; }
    }
}
