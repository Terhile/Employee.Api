using Employee.Api.Domain.Models;
using FluentValidation;

namespace Employee.Api.Validators
{
    public class EmployeeModelValidator : AbstractValidator<EmployeeModel>
    {

        public EmployeeModelValidator()
        {
            
            RuleFor(RequestModel => RequestModel.EmployeeName).NotNull().WithMessage("Please enter employee name");
            RuleFor(RequestModel => RequestModel.EmployeeNumber).NotNull().WithMessage("Please enter employee number"); 
            RuleFor(RequestModel => RequestModel.HourlyRate).GreaterThan(0).WithMessage("Invalid hourly rate");
            RuleFor(RequestModel => RequestModel.HourlyRate).Must((instance,val,context) => { return instance.TotalPay == instance.HourlyRate * instance.HoursWorked; }).WithMessage("Invalid total pay");
        }
    }
}
