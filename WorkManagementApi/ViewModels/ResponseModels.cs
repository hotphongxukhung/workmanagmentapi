using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagementApi.ViewModels
{
    public class ResponseModels
    {
        public string Message { get; set; }

        public object Data { get; set; }

        public bool Success { get; set; }
    }
}
