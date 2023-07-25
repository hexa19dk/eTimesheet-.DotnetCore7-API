namespace e_TimesheetNET7.Models.SIO
{
    public class SIOLimo
    {
        public string SioYear { get; set; }
        public string SioNo { get; set; }
        public string SioPool { get; set; }
        public int SioSts { get; set; }
        public string DriverCode { get; set; }
        public string DriverName { get; set; }
        public string VehicleCode { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public int StartKm { get; set; }
        public int FinishKm { get; set; }
        public int LiterBBM { get; set; }
        public string PoolDestination { get; set; }
        public bool Showed { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string TipeSIO { get; set; }
        public string Keterangan { get; set; }
        public string Area { get; set; }
        public string SioAppCode { get; set; }
        public string NoEquipment { get; set; }
    }

}
