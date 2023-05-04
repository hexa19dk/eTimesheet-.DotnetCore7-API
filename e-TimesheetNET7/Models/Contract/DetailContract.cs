using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_TimesheetNET7.Models.Contract
{
    public class DetailContract
    {
        [Key]
        [Column(Order = 0)]
        public string? NoKontrak { get; set; }
        [Key]
        [Column(Order = 1)]
        public string? NoItem { get; set; }
        public string? TglMulai { get; set; }
        public string? TglSelesai { get; set; }
        public string? KdPool { get; set; }
        public string? Material { get; set; }
        public int Qty { get; set; }

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
    }
}
