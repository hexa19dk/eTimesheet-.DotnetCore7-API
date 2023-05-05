using Dapper;
using e_TimesheetNET7.Config.Interfaces;
using e_TimesheetNET7.Models.Contract;
using e_TimesheetNET7.Models.Timesheet;
using e_TimesheetNET7.Repositories.Interfaces;
using System.Diagnostics.Contracts;
using System.Reflection.PortableExecutable;

namespace e_TimesheetNET7.Repositories.SQL
{
    public class TimesheetDb : ITimesheetDb
    {
        private readonly IConnectionFactoryDb _connect;
        public TimesheetDb(IConnectionFactoryDb connect)
        {
            _connect = connect;
        }

        public async Task<HeaderContract> GetHeaderContract(string contractNo)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            var query = "select * from SAP_MTKontrakHeader where NoKontrak=?";
            var param = new DynamicParameters();
            param.Add("NoKontrak", contractNo);
            var header = await conn.QueryFirstOrDefaultAsync<HeaderContract>(query, param);

            return header;
        }

        public async Task<DetailContract> GetDetailContract(string contractNo, string itemNo)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            var query = "select * from SAP_MTKontrakDetail where NoKontrak=? and NoItem=?";
            var param = new DynamicParameters();
            param.Add("NoKontrak", contractNo);
            param.Add("NoItem", itemNo);
            var detail = await conn.QueryFirstOrDefaultAsync<DetailContract>(query, param);

            return detail;
        }

        public async Task<DetailDetailContract> GetDetail2Contract(string contractNo, string itemNo, string detailDetail)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            var query = "SELECT * FROM SAP_MTKontrakDetailDetail WHERE NoKontrak=? and NoItem=? and DetailDetail=?";
            var param = new DynamicParameters();
            param.Add("NoKontrak", contractNo);
            param.Add("NoItem", itemNo);
            param.Add("DetailDetail", detailDetail);
            var detail2 = await conn.QueryFirstOrDefaultAsync<DetailDetailContract>(query, param);

            return detail2;
        }

        public async Task<HeaderTimesheet> GetTimesheetHeader(string internalTsNo, string tahun)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            var query = "SELECT * FROM SAP_TRTimeSheetHeader WHERE InternalTSNo=? AND Tahun=?";
            var param = new DynamicParameters();
            param.Add("InternalTSNo", internalTsNo);
            param.Add("Tahun", tahun);
            var tsHeader = await conn.QueryFirstOrDefaultAsync<HeaderTimesheet>(query, param);

            return tsHeader;
        }

        public async Task<DetailTimesheet> GetDetailTimesheet(string internalTsNo, string tahun)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            var query = "SELECT * FROM SAP_TRTimeSheetDetail WHERE InternalTSNo=? AND Tahun=?";
            var param = new DynamicParameters();
            param.Add("InternalTSNo", internalTsNo);
            param.Add("Tahun", tahun);
            var detailTs = await conn.QueryFirstOrDefaultAsync<DetailTimesheet>(query, param);

            return detailTs;
        }

        public async Task<TimesheetData> GetTimeSheetData(string internalTsNo, string tahun)
        {
            var headerTs = await GetTimesheetHeader(internalTsNo, tahun);
            var detailTs = await GetDetailTimesheet(internalTsNo, tahun);

            var tsData = new TimesheetData
            {
                Header = headerTs,
                Detail = detailTs
            };

            return tsData;
        }

        public async Task<bool> InsertTimesheet(TimesheetData tsData)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            
            using (var transaction = conn.BeginTransaction())
            {
                transaction.Commit();

                var insertHeader = "insert into SAP_TrTimeSheetHeader(InternalTsNo, Tahun, AppCode, NIP, Pool, PeriodFrom, PeriodTo, JenisPengemudi, JamKerja, NoKontrak, NoItem, DetailDetail, NoCustomer, NoLambung, KelasKendaraan, KmAwal, KmAkhir, KategoriKomisi, NoTimesheet, FlagDeletion, Status, NIPReplaced, NoEquipment, CreationDate, CreationTime, CreationUser, LastUpdateDate, LastUpdateTime, LastUpdateUser, TglKirim, FlagKirim, TglRelease, RefInternalTSNo, RefTahun, AsalTS, KodeArea) into values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                var paramHdr = new DynamicParameters();
                paramHdr.Add("InternalTsNo", tsData.Header?.InternalTsNo);
                paramHdr.Add("Tahun", tsData.Header?.Tahun);
                paramHdr.Add("AppCode", tsData.Header?.AppCode);
                paramHdr.Add("NIP", tsData.Header?.NIP);
                paramHdr.Add("Pool", tsData.Header?.Pool);
                paramHdr.Add("PeriodForm", tsData.Header?.PeriodFrom);
                paramHdr.Add("PeriodTo", tsData?.Header?.PeriodTo);
                paramHdr.Add("JenisPengemudi", tsData?.Header?.JenisPengemudi);
                paramHdr.Add("JamKerja", tsData.Header?.JamKerja);
                paramHdr.Add("NoKontrak", tsData.Header?.NoKontrak);
                paramHdr.Add("NoItem", tsData.Header?.NoItem);
                paramHdr.Add("DetailDetail", tsData.Header?.DetailDetail);
                paramHdr.Add("NoCustomer", tsData.Header?.NoCustomer);
                paramHdr.Add("NoLambung", tsData.Header?.NoLambung);
                paramHdr.Add("KelasKendaraan", tsData.Header?.KelasKendaraan);
                paramHdr.Add("KmAwal", tsData.Header?.KmAwal);
                paramHdr.Add("KmAkhir", tsData.Header?.KmAkhir);
                paramHdr.Add("KategoriKomisi", tsData.Header?.KategoriKomisi);
                paramHdr.Add("NoTimesheet", tsData.Header?.NoTimesheet);
                paramHdr.Add("FlagDeletion", tsData.Header?.FlagDeletion);
                paramHdr.Add("Status", tsData.Header?.Status);
                paramHdr.Add("NIPReplaced", tsData.Header?.NIPReplaced);
                paramHdr.Add("NoEquipment", tsData.Header?.NoEquipment);
                paramHdr.Add("CreationDate", tsData.Header?.CreationDate);
                paramHdr.Add("CreationUser", tsData.Header?.CreationUser);
                paramHdr.Add("LastUpdateDate", tsData.Header?.LastUpdateDate);
                paramHdr.Add("LastUpdateTime", tsData.Header?.LastUpdateTime);
                paramHdr.Add("LastUpdateUser", tsData.Header?.LastUpdateUser);
                paramHdr.Add("TglKirim", tsData.Header?.TglKirim);
                paramHdr.Add("FlagKirim", tsData.Header?.FlagKirim);
                paramHdr.Add("TglRelease", tsData.Header?.TglRelease);
                paramHdr.Add("RefInternalTSNo", tsData.Header?.RefInternalTSNo);
                paramHdr.Add("RefTahun", tsData.Header?.RefTahun);
                paramHdr.Add("AsalTS", tsData.Header?.AsalTS);
                paramHdr.Add("KodeArea", tsData.Header?.KodeArea);

                var resultHdr = await conn.ExecuteAsync(insertHeader, paramHdr);

                var insertDetail = "insert into SAP_TrTimeSheetDetail(InternalTsNo, Tahun, AppCode, Pool, TglMulai, JamMulai, JamSelesai, UsageType, NoSIO, NoSO, NoLambung, NoEquipment, KodeHarga, FlagExtraHariRaya, HariExtraInap, PersenKomisi, PersenDongkrak, CreationDate, CreationTime, CreationUser, LastUpdateDate,LastUpdateTime, LastUpdateUser, Hari, NoPolisiLama, NIP, KodeArea) values(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                var paramDtl = new DynamicParameters();
                paramDtl.Add("InternalTsNo", tsData.Detail.InternalTsNo);
                paramDtl.Add("Tahun", tsData.Detail.Tahun);
                paramDtl.Add("AppCode", tsData.Detail.AppCode);
                paramDtl.Add("Pool", tsData.Detail.Pool);
                paramDtl.Add("TglMulai", tsData.Detail.TglMulai);
                paramDtl.Add("JamMulai", tsData.Detail.JamMulai);
                paramDtl.Add("JamSelesai", tsData.Detail.JamSelesai);
                paramDtl.Add("UsageType", tsData.Detail.UsageType);
                paramDtl.Add("NoSIO", tsData.Detail.NoSIO);
                paramDtl.Add("NoSO", tsData.Detail.NoSO);
                paramDtl.Add("NoLambung", tsData.Detail.NoLambung);
                paramDtl.Add("NoEquipment", tsData.Detail.NoEquipment);
                paramDtl.Add("KodeHarga", tsData.Detail.KodeHarga);
                paramDtl.Add("FlagExtraHariRaya", tsData.Detail.FlagExtraHariRaya);
                paramDtl.Add("HariExtraInap", tsData.Detail.HariExtraInap);
                paramDtl.Add("PersenKomisi", tsData.Detail.PersenKomisi);
                paramDtl.Add("PersenDongkrak", tsData.Detail.PersenDongkrak);
                paramDtl.Add("CreationDate", tsData.Detail.CreationDate);
                paramDtl.Add("CreationTime", tsData.Detail.CreationTime);
                paramDtl.Add("CreationUser", tsData.Detail.CreationUser);
                paramDtl.Add("LastUpdateDate", tsData.Detail.LastUpdateDate);
                paramDtl.Add("LastUpdateTime", tsData.Detail.LastUpdateTime);
                paramDtl.Add("Hari", tsData.Detail.Hari);
                paramDtl.Add("NoPolisiLama", tsData.Detail.NoPolisiLama);
                paramDtl.Add("NIP", tsData.Detail.NIP);
                paramDtl.Add("KodeArea", tsData.Detail.KodeArea);

                var resultDtl = await conn.ExecuteAsync(insertDetail, paramDtl);

                return true;
            }           

        }
    }
}
