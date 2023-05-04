using Dapper;
using e_TimesheetNET7.Config.Interfaces;
using e_TimesheetNET7.Models.Contract;
using e_TimesheetNET7.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Collections;

namespace e_TimesheetNET7.Repositories.SQL
{
    public class ContractDb : IContractDb
    {
        private readonly IConnectionFactoryDb _connect;

        public ContractDb(IConnectionFactoryDb connect)
        {
            _connect = connect;
        }

        public async Task<HeaderContract> GetContractHeader(string contractNo)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            HeaderContract header = new HeaderContract();

            //var query = "SELECT * FROM SAP_MTKontrakHeader WHERE NoKontrak=" + contractNo;
            var query = @"select * from SAP_MTKontrakHeader where NoKontrak like ? ";
            var param = new DynamicParameters();
            param.Add("NoKontrak", $"%{contractNo}%");
            header = await conn.QueryFirstOrDefaultAsync<HeaderContract>(query, param);

            return header;
        }

        public async Task<IEnumerable<DetailContract>> GetContractItem(string contractNo)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            var query = "SELECT * FROM SAP_MTKontrakDetail WHERE NoKontrak LIKE ?";
            var param = new DynamicParameters();
            param.Add("NoKontrak", $"%{contractNo}%");
            var detail = await conn.QueryAsync<DetailContract>(query, param);

            return detail;
        }

        public async Task<IEnumerable<DetailDetailContract>> GetContractDetail(string contractNo)
        {
            var conn = await _connect.CreateODBCConnectionAsync();
            var query = "SELECT * FROM SAP_MTKontrakDetailDetail WHERE NoKontrak LIKE ?";
            var param = new DynamicParameters();
            param.Add("NoKontrak", $"%{contractNo}%");
            var item = await conn.QueryAsync<DetailDetailContract>(query, param);

            return item;
        }

        public async Task<DataContract> GetContractData(string contractNo)
        {
            var header = await GetContractHeader(contractNo);
            var item = await GetContractItem(contractNo);
            var detail = await GetContractDetail(contractNo);

            var _detail = JsonConvert.SerializeObject(detail);
            List<DetailDetailContract> _lDetail = JsonConvert.DeserializeObject<List<DetailDetailContract>>(_detail);

            var contract = new DataContract
            {
                Header = header,
                Detail = item,
                //DetailDetail = detail,
                DetailDetail = _lDetail,
            };

            return contract;
        }
    }
}
