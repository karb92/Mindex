using System;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
    public class EmployeeCompensation
    {
        [Key]
        public string Id { get; set; }
        public double Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
