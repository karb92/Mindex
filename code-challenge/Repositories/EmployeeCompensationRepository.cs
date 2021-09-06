using System;
using System.Linq;
using challenge.Data;
using challenge.Models;
using Microsoft.Extensions.Logging;

namespace challenge.Repositories
{
    public class EmployeeCompensationRepository: IEmployeeCompensationRepository
    {
        private readonly EmployeeContext _eContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeCompensationRepository(ILogger<IEmployeeRepository> logger, EmployeeContext eContext)
        {
            _eContext = eContext;
            _logger = logger;
        }

        public bool CreateEmployeeCompensation(EmployeeCompensation eComponsation)
        {

            try
            {
                _eContext.Compensations.Add(eComponsation);
                return true;
            }
            catch (Exception)
            {
                _logger.LogError("Failed to Create Employee Componsation");
                return false;
            }
        }

        public EmployeeCompensation GetEmployeeCompensation(string empId)
        {

            try
            {
                return _eContext.Compensations.SingleOrDefault(e => e.Id == empId);
                
            }
            catch (Exception)
            {
                _logger.LogError("Failed to Get Employee Componsation");
                return null;
            }
        }
    }
}
