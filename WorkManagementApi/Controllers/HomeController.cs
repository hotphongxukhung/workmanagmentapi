using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkManagementApi.Global;
using WorkManagementApi.Models;
using WorkManagementApi.ViewModels;

namespace WorkManagementApi.Controllers
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
        public IActionResult Login(string username, string password)
        {
            try
            {
                password = FunctionPlus.GetMD5HashString(password);

                var user = _context.Users.FirstOrDefault(q => q.Username == username && q.Password == password);

                if(user !=null)
                {
                    var userModel = _mapper.Map<UserViewModels>(user);
                    if(user.RoleId != 1)
                    {
                        userModel.GroupName = user.Group.GroupName;
                    }

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