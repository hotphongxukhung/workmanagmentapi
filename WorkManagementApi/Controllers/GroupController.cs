using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkManagementApi.Models;
using WorkManagementApi.ViewModels;

namespace WorkManagementApi.Controllers
{
    [Route("Group")]
    public class GroupController : Controller
    {
        private WorkManagementContext context;
        private readonly IMapper _mapper;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public GroupController(IMapper mapper, WorkManagementContext context)
        {
            this.context = context;
            this._mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetAllGroup()
        {
            try
            {
                var result = _mapper.Map<List<GroupViewModels>>(context.Groups.Select(q => new Groups{ GroupId = q.GroupId, GroupName = q.GroupName }).ToList());
                return Ok(new ResponseModels { Data = result, Success = true });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }

        
    }
}