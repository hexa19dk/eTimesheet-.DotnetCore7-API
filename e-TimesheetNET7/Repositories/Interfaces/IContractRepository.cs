using e_TimesheetNET7.Models;

namespace e_TimesheetNET7.Repositories.Interfaces
{
    public interface IContractRepository
    {
        IContractDb Contracts();
    }

    public delegate IContractRepository ContractServiceResolver(ServiceType serviceType);
}
