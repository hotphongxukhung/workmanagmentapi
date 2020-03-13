using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkManagementApi.Models;
using WorkManagementApi.ViewModels;

namespace WorkManagementApi.Controllers
{
    [Route("Status")]
    public class StatusController : Controller
    {
        private WorkManagementContext context;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public StatusController(WorkManagementContext context)
        {
            this.context = context;
        }

        [HttpGet("")]
        public IActionResult GetStatusList()
        {
            try
            {
                var result = context.Status;
                return Ok(new ResponseModels { Data = result, Success = true });
            }
            catch(Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }
    }
}