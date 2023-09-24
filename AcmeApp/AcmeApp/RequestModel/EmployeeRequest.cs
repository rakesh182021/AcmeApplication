using System;

namespace AcmeApp.RequestModel
{
    public class EmployeeRequest
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime EmployeeDate { get; set; }
        public DateTime TerminatedDate { get; set; }
    }
}