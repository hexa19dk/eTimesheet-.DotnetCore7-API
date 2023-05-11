using Dapper;
using e_TimesheetNET7.Config.Interfaces;
using e_TimesheetNET7.Models.Contract;
using e_TimesheetNET7.Models.Timesheet;
using e_TimesheetNET7.Repositories.Interfaces;
using System.Linq;
using System.Reflection.Emit;
using System.Transactions;

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

        public async Task<List<DetailTimesheet>> GetDetailTimesheet(string internalTsNo, string tahun)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            var query = "SELECT * FROM SAP_TRTimeSheetDetail WHERE InternalTSNo=? AND Tahun=?";
            var param = new DynamicParameters();
            param.Add("InternalTSNo", internalTsNo);
            param.Add("Tahun", tahun);
            var detailTs = await conn.QueryFirstOrDefaultAsync<List<DetailTimesheet>>(query, param);

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
            //var newId = Guid.NewGuid().ToString();

            var header = await GetHeaderContract(tsData.Header.NoKontrak);
            var detail = await GetDetailContract(tsData.Header.NoKontrak, tsData.Header.NoItem);
            var detail2 = await GetDetail2Contract(tsData.Header.NoKontrak, tsData.Header.NoItem, tsData.Header.DetailDetail);

            
            var query = "SELECT COUNT(*) Count FROM SAP_TrTimeSheetHeader AS tsHeader " +
                "INNER JOIN SAP_TrTimeSheetDetail tsDetail ON tsHeader.InternalTsNo = tsDetail.InternalTsNo " +
                "WHERE tsHeader.NIP=? AND tsDetail.Tahun=? AND tsDetail.TglMulai=? ";

            var param = new DynamicParameters();
            param.Add("NIP", tsData.Header.NIP);
            foreach(var data in tsData.Detail)
            {
                param.Add("Tahun", data.Tahun);
                param.Add("TglMulai", data.TglMulai);
            }            
            var isExistDetail = await conn.QueryFirstOrDefaultAsync<int>(query, param);

            if(isExistDetail == 0)
            {
                if (header != null && detail != null && detail2 != null)
                {
                    var transaction = await conn.BeginTransactionAsync();
                    
                    try
                    {
                        #region TS Header Trasnsaction

                        var insertHeader = @"INSERT INTO [SAP_TrTimeSheetHeader] ([InternalTSNo],[Tahun],[AppCode],[NIP],[Pool],[PeriodFrom],[PeriodTo],[JenisPengemudi],[JamKerja],[NoKontrak],[NoItem],[DetailDetail],[NoCustomer],[NoLambung],[KelasKendaraan],[KmAwal],[KmAkhir],[KategoriKomisi],[NoTimeSheet],[FlagDeletion],[Status],[NIPReplaced],[NoEquipment],[CreationDate],[CreationTime],[CreationUser],[LastUpdateDate],[LastUpdateTime],[LastUpdateUser],[tglkirim],[flagkirim],[TglRelease],[RefInternalTSNo],[RefTahun],[AsalTS],[KodeArea]) 
                          VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                        var paramHdr = new DynamicParameters();
                        paramHdr.Add("InternalTSNo", tsData.Header?.InternalTSNo);
                        paramHdr.Add("Tahun", tsData.Header?.Tahun);
                        paramHdr.Add("AppCode", tsData.Header?.AppCode);
                        paramHdr.Add("NIP", tsData.Header?.NIP);
                        paramHdr.Add("Pool", tsData.Header?.Pool);
                        paramHdr.Add("PeriodFrom", tsData.Header?.PeriodFrom);
                        paramHdr.Add("PeriodTo", tsData.Header?.PeriodTo);
                        paramHdr.Add("JenisPengemudi", tsData.Header?.JenisPengemudi);
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
                        paramHdr.Add("NoTimeSheet", tsData.Header?.NoTimesheet);
                        paramHdr.Add("FlagDeletion", tsData.Header?.FlagDeletion);
                        paramHdr.Add("Status", tsData.Header?.Status);
                        paramHdr.Add("NIPReplaced", tsData.Header?.NIPReplaced);
                        paramHdr.Add("NoEquipment", tsData.Header?.NoEquipment);
                        paramHdr.Add("CreationDate", tsData.Header?.CreationDate);
                        paramHdr.Add("CreationTime", tsData.Header?.CreationTime);
                        paramHdr.Add("CreationUser", tsData.Header?.CreationUser);
                        paramHdr.Add("LastUpdateDate", tsData.Header?.LastUpdateDate);
                        paramHdr.Add("LastUpdateTime", tsData.Header?.LastUpdateTime);
                        paramHdr.Add("LastUpdateUser", tsData.Header?.LastUpdateUser);
                        paramHdr.Add("tglkirim", tsData.Header?.tglkirim);
                        paramHdr.Add("flagkirim", tsData.Header?.flagkirim);
                        paramHdr.Add("TglRelease", tsData.Header?.TglRelease);
                        paramHdr.Add("RefInternalTSNo", tsData.Header?.RefInternalTSNo);
                        paramHdr.Add("RefTahun", tsData.Header?.RefTahun);
                        paramHdr.Add("AsalTS", tsData.Header?.AsalTS);
                        //paramHdr.Add("rowguid", tsData.Header.rowguid);
                        paramHdr.Add("KodeArea", tsData.Header?.KodeArea);              
                        var resultHdr = await conn.ExecuteAsync(insertHeader, paramHdr, transaction);
                        //transaction.Commit();

                        #endregion


                        #region TS Detail Transcation

                        foreach (var item in tsData.Detail)
                        {
                            var insertDetail = @"INSERT INTO SAP_TrTimeSheetDetail ([InternalTSNo],[Tahun],[AppCode],[Pool],[TglMulai],[JamMulai],[JamSelesai],[UsageType],[NoSIO],[NoSO],[NoLambung],[NoEquipment],[KodeHarga],[FlagExtraHariRaya],[HariExtraInap],[PersenKomisi],[PersenDongkrak],[CreationDate],[CreationTime],[CreationUser],[LastUpdateDate],[LastUpdateTime],[LastUpdateUser],[Hari],[NoPolisiLama],[NIP],[KodeArea]) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

                            var paramDtl = new DynamicParameters();
                            paramDtl.Add("InternalTsNo", item.InternalTsNo);
                            paramDtl.Add("Tahun", item.Tahun);
                            paramDtl.Add("AppCode", item.AppCode);
                            paramDtl.Add("Pool", item.Pool);
                            paramDtl.Add("TglMulai", item.TglMulai);
                            paramDtl.Add("JamMulai", item.JamMulai);
                            paramDtl.Add("JamSelesai", item.JamSelesai);
                            paramDtl.Add("UsageType", item.UsageType);
                            paramDtl.Add("NoSIO", item.NoSIO);
                            paramDtl.Add("NoSO", item.NoSO);
                            paramDtl.Add("NoLambung", item.NoLambung);
                            paramDtl.Add("NoEquipment", item.NoEquipment);
                            paramDtl.Add("KodeHarga", item.KodeHarga);
                            paramDtl.Add("FlagExtraHariRaya", item.FlagExtraHariRaya);
                            paramDtl.Add("HariExtraInap", item.HariExtraInap);
                            paramDtl.Add("PersenKomisi", item.PersenKomisi);
                            paramDtl.Add("PersenDongkrak", item.PersenDongkrak);
                            paramDtl.Add("CreationDate", item.CreationDate);
                            paramDtl.Add("CreationTime", item.CreationTime);
                            paramDtl.Add("CreationUser", item.CreationUser);
                            paramDtl.Add("LastUpdateDate", item.LastUpdateDate);
                            paramDtl.Add("LastUpdateTime", item.LastUpdateTime);
                            paramDtl.Add("LastUpdateUser", item.LastUpdateUser);
                            paramDtl.Add("Hari", item.Hari);
                            paramDtl.Add("NoPolisiLama", item.NoPolisiLama);
                            paramDtl.Add("NIP", item.NIP);
                            //paramDtl.Add("rowguid", item.rowguid);
                            paramDtl.Add("KodeArea", item.KodeArea);
                            var resultDtl = await conn.ExecuteAsync(insertDetail, paramDtl, transaction);
                        }

                        #endregion

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception("Some error occured" + ex.Message);
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
