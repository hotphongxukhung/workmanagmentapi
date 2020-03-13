using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkManagementApi.Global;
using WorkManagementApi.ViewModels;
using WorkManagementApi.Models;

namespace WorkManagementApi.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private WorkManagementContext _context;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public UserController(IMapper mapper, WorkManagementContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        [HttpGet("{currUserID}/{userID}")]
        public IActionResult GetUserByUser(int currUserID, int userID)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(q => q.Id == userID);
                var curr = _context.Users.FirstOrDefault(q => q.Id == currUserID);
                if ((curr.RoleId == 2 && user.GroupId == curr.GroupId) || curr.RoleId == 1)
                {
                    return Ok(new ResponseModels { Success = true, Data = user });
                }
                else
                {
                    return Ok(new ResponseModels { Success = false, Message = "This member is not in your group" });
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }

        [HttpGet("Group/{userID}")]
        public IActionResult GetUsersByUser(int userID)
        {
            try
            {
                var users = _context.Users.Select(q => new { q.Id, q.Fullname });
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
                            users.Where(q => empOfGroup.Contains(q.Id)).Select(q => new { q.Id, q.Fullname });
                            break;
                        }
                    case 3:
                        {
                            users.Where(q => q.Id == userID);
                            break;
                        }
                }
                var userListModel = _mapper.Map<List<UserViewModels>>(users.Select(q => new Users { Id = q.Id, Fullname = q.Fullname }));
                return Ok(new ResponseModels { Success = true, Data = userListModel });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
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

                return Ok(new ResponseModels { Success = true, Message = "Remove succeed" });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
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

                return Ok(new ResponseModels { Success = true, Message = "Move succeed" });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }

        [HttpPost("")]
        public IActionResult CreateUser([FromQuery]UserViewModels model)
        {
            try
            {
                if (_context.Users.FirstOrDefault(q => q.Username == model.Username) != null)
                {
                    return Ok(new ResponseModels { Success = false, Message = "Username is existed" });
                }

                model.Password = FunctionPlus.GetMD5HashString(model.Password);
                _context.Users.Add(_mapper.Map<Users>(model));
                _context.SaveChanges();
                return Ok(new ResponseModels { Success = true, Message = "Create succeed" });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }

        [HttpPut("")]
        public IActionResult UpdateUser([FromQuery]UserViewModels model)
        {
            try
            {
                model.Password = FunctionPlus.GetMD5HashString(model.Password);
                _context.Users.Update(_mapper.Map<Users>(model));
                _context.SaveChanges();
                return Ok(new ResponseModels { Success = true, Message = "Update succeed" });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(q => q.Id == id);
                user.Active = false;
                _context.Users.Update(user);
                _context.SaveChanges();
                return Ok(new ResponseModels { Success = true, Message = "Update succeed" });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Message = "", Success = false });
            }
        }
    }
}