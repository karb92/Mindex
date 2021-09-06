using System;
namespace challenge.Models
{
    public class ReportingStructure
    {

        public int numberOfReports { get; set; }

        public Employee employee;

        public ReportingStructure()
        {
            employee = new Employee();
        }

    }
}
