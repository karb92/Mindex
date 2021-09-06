using System;
namespace challenge.Models
{
    public class EmployeeCompensationDTO
    {
        public Employee employee;
        public double Salary { get; set; }
        public DateTime EffectiveDate { get; set; }

        public EmployeeCompensationDTO()
        {
        }

        public static EmployeeCompensationDTO MapFrom(EmployeeCompensation empCompensation, Employee employee)
        {
            return new EmployeeCompensationDTO()
            {
                employee = employee,
                Salary = empCompensation.Salary,
                EffectiveDate = empCompensation.EffectiveDate
            };
        }
    }
}
