using Employee.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Domain.Queries
{
    public class QueryResult
    {
        public List<EmployeeModel> Items { get; set; } = new List<EmployeeModel>();
        public int TotalItems { get; set; } = 0;
    }
}
