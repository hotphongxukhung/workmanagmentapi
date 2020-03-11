using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkManagementAPI.Global;
using WorkManagementAPI.Models;
using WorkManagementAPI.ViewModels;

namespace WorkManagementAPI.Controllers
{
    [Route("/Home")]
    public class HomeController : Controller
    {
        private WorkManagementContext _context;
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper, WorkManagementContext context) 
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpPost("")]
        public IActionResult Login([FromBody] UserViewModels model)
        {
            try
            {
                model.Password = FunctionPlus.GetMD5HashString(model.Password);

                var user = _context.Users.FirstOrDefault(q => q.Username == model.Username && q.Password == model.Password);

                if(user !=null)
                {
                    var userModel = _mapper.Map<UserViewModels>(user);
                    return Ok(new ResponseModels { Success = true , Data = userModel});
                }
                else
                {
                    return Ok(new ResponseModels { Success = false, Message = "Invalid username or password" });
                }
                
            }
            catch(Exception e)
            {
                return StatusCode(500, new ResponseModels { Success = false, Message = "" });
            }
        }
    }
}