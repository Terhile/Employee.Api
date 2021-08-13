using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Api.Domain.Request
{
    public class RequestModel
    {

        public int StartIndex { get; set; }
        public int ItemsPerPage { get; set; }
        public string OrderBy { get;   set; }

    }
}
