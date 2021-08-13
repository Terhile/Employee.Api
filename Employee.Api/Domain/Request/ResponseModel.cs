using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Domain.Request
{
    public class ResponseModel
    {
        public bool Successful { get; set; } = true;
        public object Data { get; set; } 
    }
}
