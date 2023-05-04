using e_TimesheetNET7.Models.Contract;

namespace e_TimesheetNET7.Usecase.Interfaces
{
    public interface IContractUsecase
    {
        Task<DataContract> GetContract(string contractNo);
        Task<HeaderContract> GetHeader(string contractNo);
    }
}
