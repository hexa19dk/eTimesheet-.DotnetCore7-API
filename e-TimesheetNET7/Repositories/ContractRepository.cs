using e_TimesheetNET7.Repositories.Interfaces;
using e_TimesheetNET7.Repositories.SQL;

namespace e_TimesheetNET7.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly IContractDb _contract;
        public ContractRepository(IContractDb contract) 
        {
            _contract = contract;
        }

        public IContractDb Contracts()
        {
            return _contract;
        }
    }
}
