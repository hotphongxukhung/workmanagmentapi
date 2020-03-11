using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkManagementAPI.Global;
using WorkManagementAPI.Models;
using WorkManagementAPI.ViewModels;

namespace WorkManagementAPI.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private WorkManagementContext _context;

        public UserController(IMapper mapper, WorkManagementContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        [HttpGet("{userID}")]
        public IActionResult GetUsersByID(int userID)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(q => q.Id == userID);
                
                return Ok(new ResponseModels { Success = true, Data = user });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }

        [HttpGet("Role/{userID}")]
        public IActionResult GetUsersByUserRole(int userID)
        {
            try
            {
                var users = _context.Users.Select(q => new { q.Id, q.Fullname});
                var user = _context.Users.FirstOrDefault(q => q.Id == userID);
                switch (user.Id)
                {
                    
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            var empOfGroup = _context.Users.Where(q => q.GroupId == user.Group.GroupId).Select(q => q.Id).ToList();
                            empOfGroup.Add(user.Id);
                            users.Where(q => empOfGroup.Contains(q.Id)).Select(q => new { q.Id, q.Fullname});
                            break;
                        }
                    case 3:
                        {
                            users.Where(q => q.Id == userID);
                            break;
                        }
                }

                var userListModel = _mapper.Map<List<UserViewModels>>(users);
                return Ok(new ResponseModels { Success = true, Data = userListModel });
            }
            catch(Exception e)
            {
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }

        [HttpPut("{userID}")]
        public IActionResult RemoveFromGroup(int userID)
        {
            try
            {
                var emp = _context.Users.FirstOrDefault(q => q.Id == userID);
                
                emp.GroupId = 0;

                _context.Users.Update(emp);

                _context.SaveChanges();

                return Ok(new ResponseModels {Success = true , Message = "Remove succeed"});
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }

        [HttpPut("{userID}/{groupID}")]
        public IActionResult MoveToGroup(int userID, int groupID)
        {
            try
            {
                var emp = _context.Users.FirstOrDefault(q => q.Id == userID);

                emp.GroupId = groupID;

                _context.Users.Update(emp);

                _context.SaveChanges();

                return Ok(new ResponseModels { Success = true , Message = "Move succeed"});
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }

        [HttpPost("")]
        public IActionResult CreateUser([FromBody]UserViewModels model)
        {
            try 
            {
                model.Password = FunctionPlus.GetMD5HashString(model.Password);
                _context.Users.Add(_mapper.Map<Users>(model));
                _context.SaveChanges();
                return Ok(new ResponseModels { Success = true , Message = "Create succeed"});
            }
            catch(Exception e)
            {
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }
    }
}