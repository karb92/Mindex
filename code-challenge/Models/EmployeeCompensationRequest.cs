using System;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
    public class EmployeeCompensationRequest
    {
        [Required]
        public string employeeId;
        [Required]
        public double Salary { get; set; }
        [Required]
        public DateTime EffectiveDate { get; set; }

        public static EmployeeCompensation MapTo(EmployeeCompensationRequest eCompensation)
        {
            return new EmployeeCompensation()
            {
                Id = eCompensation.employeeId,
                Salary = eCompensation.Salary,
                EffectiveDate = eCompensation.EffectiveDate
            };
        }
    }
}
