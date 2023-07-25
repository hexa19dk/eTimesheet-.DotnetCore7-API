using e_TimesheetNET7.Models.Contract;
using e_TimesheetNET7.Repositories.Interfaces;

namespace e_TimesheetNET7.Usecase
{
    public interface IContractUsecase
    {
        Task<DataContract> GetContract(string contractNo);
        Task<HeaderContract> GetHeader(string contractNo);
    }

    public class ContractUsecase : IContractUsecase
    {
        private readonly IContractRepository _contractRepo;
        public ContractUsecase(IContractRepository contractRepo) 
        {
            _contractRepo = contractRepo;
        }

        public async Task<DataContract> GetContract(string contractNo)
        {
            try
            {
                var result = await _contractRepo.Contracts().GetContractData(contractNo);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("Get data contract failed. " + ex.Message);
            }
        }

        public async Task<HeaderContract> GetHeader(string contractNo)
        {
            try
            {
                var result = await _contractRepo.Contracts().GetContractHeader(contractNo);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("Get header failed. " + ex.Message);
            }
        }
    }
}
