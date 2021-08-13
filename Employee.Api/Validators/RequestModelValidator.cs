using Employee.Api.Domain.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Validators
{
    public class RequestModelValidator : AbstractValidator<RequestModel>
    {
        public RequestModelValidator()
        {
            RuleFor(RequestModel => RequestModel.ItemsPerPage).NotNull().ExclusiveBetween(0, 101).WithMessage("Please enter a number between 1 - 100");
            RuleFor(RequestModel => RequestModel.StartIndex).NotNull().GreaterThan(0).WithMessage("Please enter a number greater or eqaul to 1"); ;
            RuleFor(RequestModel => RequestModel.OrderBy).NotNull().NotEqual("string").WithMessage("Order By column is required"); 

        }
    }
    
}
