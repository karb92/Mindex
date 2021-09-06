using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeCompensationRepository _employeeCompensationRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger,
                     IEmployeeRepository employeeRepository,
                     IEmployeeCompensationRepository employeeCompensationRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeCompensationRepository = employeeCompensationRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            { 
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        public ReportingStructure GetEmployeeReportingDetails(string employeeId)
        {
            // check and get employee record
            Employee employee = this._employeeRepository.GetById(employeeId);
            if (employee == null)
                return null;
            //get initial direct reportees
            List<Employee> directReportEmployees = employee?.DirectReports.ToList();

            //get all subsequent reportees in the tree if exists
            if (directReportEmployees?.Count > 0)
            {
                for (int i = 0; i < directReportEmployees.Count; i++)
                    directReportEmployees.AddRange(directReportEmployees[i]?.DirectReports);
            }

            return new ReportingStructure()
            {
                employee = employee,
                numberOfReports = directReportEmployees.Count
            };
        }

        public EmployeeCompensation GetEmployeeCompensation(string empId)
        {
            return this._employeeCompensationRepository.GetEmployeeCompensation(empId);
        }

        public bool CreateEmployeeCompensation(EmployeeCompensation eCompensation)
        {
            bool result =  this._employeeCompensationRepository.CreateEmployeeCompensation(eCompensation);
            if (result)
            {
                this._employeeRepository.SaveAsync();
                return result;
            }
             return false;
        }
    }
}
