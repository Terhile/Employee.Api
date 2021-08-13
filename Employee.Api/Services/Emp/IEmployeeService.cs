using Employee.Api.Domain.Models;
using Employee.Api.Domain.Queries;
using Employee.Api.Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Services.Emp
{
    public interface IEmployeeService
    {
        int Save(EmployeeModel employee);
        List<EmployeeModel> Employees(RequestModel request);
    }
}
