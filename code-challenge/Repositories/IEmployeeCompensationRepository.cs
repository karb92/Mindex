using System;
using challenge.Models;

namespace challenge.Repositories
{
    public interface IEmployeeCompensationRepository
    {
        bool CreateEmployeeCompensation(EmployeeCompensation eComponsation);

        EmployeeCompensation GetEmployeeCompensation(string empId);
    }
}
