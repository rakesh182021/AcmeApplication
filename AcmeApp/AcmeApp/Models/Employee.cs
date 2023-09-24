using System;

namespace AcmeApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime EmployeeDate { get; set; }
        public DateTime TerminatedDate { get; set; }

        public virtual Person Person { get; set; }
    }
}
