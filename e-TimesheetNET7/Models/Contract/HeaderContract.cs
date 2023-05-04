using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_TimesheetNET7.Models.Contract
{
    [Table("SAP_MtKontrakHeader")]
    public class HeaderContract
    {
        public string? TipeKontrak { get; set; }
        [Key]
        public string? NoKontrak { get; set; }
        public string? NoMaster { get; set; }
        public string? SalesOrg { get; set; }
        public string? Distribution { get; set; }
        public string? TglMulai { get; set; }
        public string? TglSelesai { get; set; }
        public string? SalesName { get; set; }
        public string? SalesCompany { get; set; }
        public string? ContactPerson { get; set; }
        public string? NoCustomer { get; set; }
        public string? Status { get; set; }
        public string? Block { get; set; }
        public string? RefKontrak { get; set; }
        public string? CreationDate { get; set; }
        public string? CreationTime { get; set; }
        public string? CreationUser { get; set; }
        public string? LastUpdateDate { get; set; }
        public string? LastUpdateTime { get; set; }
        public string? LastUpdateUser { get; set; }

        private string? _tglKirim;
        public string? TglKirim
        {
            get { return _tglKirim; }
            set
            {
                _tglKirim = value;
                if (string.IsNullOrEmpty(_tglKirim))
                    _tglKirim = "";
            }
        }

        private string? _jamKirim;
        public string? JamKirim
        {
            get
            { return _jamKirim; }
            set
            {
                _jamKirim = value;
                if (string.IsNullOrEmpty(_jamKirim))
                    _jamKirim = "";
            }
        }
    }
}
