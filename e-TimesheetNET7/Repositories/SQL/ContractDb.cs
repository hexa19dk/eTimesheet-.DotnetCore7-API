using Dapper;
using e_TimesheetNET7.Config;
using e_TimesheetNET7.Models.Contract;
using Newtonsoft.Json;

namespace e_TimesheetNET7.Repositories.SQL
{
    public interface IContractDb
    {
        Task<HeaderContract> GetContractHeader(string contractNo);
        Task<IEnumerable<DetailContract>> GetContractItem(string contractNo);
        Task<IEnumerable<DetailDetailContract>> GetContractDetail(string contractNo);
        Task<DataContract> GetContractData(string contractNo);
    }

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
                DetailDetail = _lDetail,
            };

            return contract;
        }
    }
}
