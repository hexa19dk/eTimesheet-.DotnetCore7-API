using e_TimesheetNET7.Models;
using e_TimesheetNET7.Repositories.SQL;

namespace e_TimesheetNET7.Repositories.Interfaces
{
    public interface IContractRepository
    {
        IContractDb Contracts();
    }

    public delegate IContractRepository ContractServiceResolver(ServiceType serviceType);
}
