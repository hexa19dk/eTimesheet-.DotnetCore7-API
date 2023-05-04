using e_TimesheetNET7.Models.Contract;

namespace e_TimesheetNET7.Repositories.Interfaces
{
    public interface IContractDb
    {
        Task<HeaderContract> GetContractHeader(string contractNo);
        Task<IEnumerable<DetailContract>> GetContractItem(string contractNo);
        Task<IEnumerable<DetailDetailContract>> GetContractDetail(string contractNo);
        Task<DataContract> GetContractData(string contractNo);
    }
}
