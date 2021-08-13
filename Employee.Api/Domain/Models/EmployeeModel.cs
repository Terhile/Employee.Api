using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Api.Domain.Models
{
    [Table("EmployeeInfo")]
    public class EmployeeModel
    {

        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public decimal HourlyRate { get; set; }
        public int HoursWorked { get; set; }
        public decimal TotalPay { get; set; }

    }
}
