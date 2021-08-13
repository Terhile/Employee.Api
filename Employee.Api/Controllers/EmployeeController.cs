using Employee.Api.Domain.Models;
using Employee.Api.Domain.Request;
using Employee.Api.Services.Emp;
using Employee.Api.Services.Kafka;
using Employee.Api.Services.Logger;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IActivityLogger _logger;
        private readonly IMessageProducer _kafkaService;
        private readonly IValidator<RequestModel> _requestvalidator;
        private readonly IValidator<EmployeeModel> _employeevalidator;
        public EmployeeController(IEmployeeService employeeService, IActivityLogger logger, IMessageProducer kafkaService, IValidator<RequestModel> requestValidator, IValidator<EmployeeModel> employeevalidator)
        {
            _employeeService = employeeService;

            _logger = logger;
            _kafkaService = kafkaService;
            _requestvalidator = requestValidator;
            _employeevalidator = employeevalidator;
        }

        [HttpPost("employee_info")]
        public async Task<IActionResult> employeeInfo(RequestModel model)
        {
            var validationResult = _requestvalidator.Validate(model);
            if (!validationResult.IsValid)
                return BadRequest(error: validationResult.ToString(";"));
            ResponseModel response = new ResponseModel();
            try
            {
                _logger.LogMessage("getting employee information ");
                var result = _employeeService.Employees(model);

                return Ok(new ResponseModel { Data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                response.Data = ex.Message;
                response.Successful = false;
            }
            return BadRequest(response);
        }


        [HttpPost("publish")]
        public  IActionResult PublishEmployeeInfo([FromBody] EmployeeModel model)
        {
           
            try
            {
                var validationResult = _employeevalidator.Validate(model);

                if (!validationResult.IsValid)
                    return BadRequest(error: validationResult.ToString(";"));
                _logger.LogMessage("request recived");
                _kafkaService.WriteMessage(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
            }

            return Ok();
            
        }
    }
}
