using Dapper;
using Employee.Api.Domain.Models;
using Employee.Api.Domain.Queries;
using Employee.Api.Domain.Request;
using Employee.Api.Persistence.Repositories.Dapper;
using Employee.Api.Services.Kafka;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Services.Emp
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMessageProducer _kafkaService;
        private readonly IDBContext _dBContext;
        private readonly IDbConnection _context;
        public EmployeeService(IMessageProducer messageProducer, IDBContext dBContext)
        {
            _kafkaService = messageProducer;
            _dBContext = dBContext;
            _context = _dBContext.GetConnection();
        }
        public  List<EmployeeModel> Employees(RequestModel request)
        {
            //var query = $"SELECT  *  FROM Employee ORDER BY {request.OrderBy} OFFSET {request.StartIndex} ROWS FETCH NEXT {request.ItemsPerPage} ROWS ONLY; ";
            var query = $"SELECT  *  FROM Employee ORDER BY {request.OrderBy} OFFSET @offSet ROWS FETCH NEXT @numberOfRows ROWS ONLY; ";
            var result = _context.Query<EmployeeModel>(query, new { offSet = request.StartIndex, numberOfRows = request.ItemsPerPage }).ToList();
            //var result = _context.QueryAsync<int,List<EmployeeModel>,QueryResult>(query, (total,emps)=>
            //{
            //    return new QueryResult() { Items = emps, TotalItems = total };
            //}, splitOn : "EmployeeNumber").Result?.First();
            return result;
        }

        public async Task<int> Save(EmployeeModel employee)
        {
            var query  = $"IF EXISTS(SELECT * FROM Employee WHERE EmployeeNumber = @EmployeeNumber) UPDATE Employee SET EmployeeNumber = @EmployeeNumber, EmployeeName = @EmployeeName," +
                $" HourlyRate = @HourlyRate, TotalHours = @TotalHours, TotalPay = @pay ELSE " +
                $"INSERT INTO Employee(EmployeeNumber, EmployeeName, HourlyRate, TotalHours, TotalPay) Values(@EmployeeNumber,@EmployeeName, @HourlyRate, @TotalHours, @TotalPay); ";

            var affectedRows = await _context.ExecuteAsync(query, new { EmployeeNumber = employee.EmployeeNumber, EmployeeName = employee.EmployeeName, HourlyRate = employee.HourlyRate, TotalHours = employee.HoursWorked, TotalPay = employee.HoursWorked*employee.HourlyRate });

            return affectedRows;
        }
    }
}
