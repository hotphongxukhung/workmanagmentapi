using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkManagementAPI.Models;
using WorkManagementAPI.ViewModels;

namespace WorkManagementAPI.Controllers
{
    [Route("Task")]
    public class TaskController : Controller
    {
        private WorkManagementContext _context;
        private readonly IMapper _mapper;

        public TaskController(IMapper mapper, WorkManagementContext context)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpPost("")]
        public IActionResult CreateTask([FromBody]TaskViewModels model)
        {
            try
            {
                _context.Tasks.Add(_mapper.Map<Tasks>(model));
                _context.SaveChanges();
                return Ok(new ResponseModels { Success = true, Message = "Create task succeed" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }

        [HttpPut("{taskID}/{isAccepted}")]
        public IActionResult AcceptATask(int taskID, bool isAccepted)
        {
            try
            {
                var task = _context.Tasks.FirstOrDefault(q => q.TaskId == taskID);
                task.Acceptance = isAccepted;
                _context.Tasks.Update(task);
                _context.SaveChanges();

                if (isAccepted)
                {
                    return Ok(new ResponseModels { Message = "Task was accepted", Success = true });
                }
                return Ok(new ResponseModels { Message = "Task was declined", Success = true });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }

        [HttpGet("{taskID}")]
        public IActionResult GetTaskDetail(int taskID)
        {
            try
            {
                var task = _context.Tasks.FirstOrDefault(q => q.TaskId == taskID);
                var taskModel = _mapper.Map<TaskViewModels>(task);

                taskModel.CreatorName = task.CreatorNavigation.Fullname;
                taskModel.ProcesssorName = task.ProcessorNavigation.Fullname;
                taskModel.StatusName = task.StatusNavigation.StatusName;

                switch (task.Acceptance)
                {
                    case true:
                        {
                            taskModel.AcceptanceName = "Accepted";
                            break;
                        }
                    case false:
                        {
                            taskModel.AcceptanceName = "Declined";
                            break;
                        }
                    default:
                        {
                            taskModel.AcceptanceName = "Unaccepted";
                            break;
                        }
                }

                switch (task.ConfirmationEnded)
                {
                    case true:
                        {
                            taskModel.ConfirmationEndedName = "Completed";
                            break;
                        }
                    case false:
                        {
                            taskModel.ConfirmationEndedName = "Could not be done";
                            break;
                        }
                    default:
                        {
                            taskModel.ConfirmationEndedName = "Unconfimred";
                            break;
                        }
                }

                return Ok(new ResponseModels { Data = taskModel, Success = true });
            }
            catch(Exception e)
            {
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }

        [HttpPut("{taskID}")]
        public IActionResult ModifyTask(TaskViewModels model)
        {
            try
            {
                _context.Tasks.Update(_mapper.Map<Tasks>(model));
                _context.SaveChanges();

                return Ok(new ResponseModels { Message = "Update task succeed", Success = true });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }

        [HttpGet("")]
        public IActionResult GetTask(int currUserID, int userID, string timeS = "", string timeE = "", int status = 0)
        {
            try
            {
                var tasks = _context.Tasks.Where(q => q.CreationTime <= DateTime.Parse(timeE) && q.CreationTime >= DateTime.Parse(timeS));
                var currUser = _context.Users.FirstOrDefault(q => q.Id == currUserID);

                switch (currUser.RoleId)
                {
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            var empOfGroup = _context.Users.Where(q => q.GroupId == currUser.Group.GroupId).Select(q => q.Id).ToList();
                            empOfGroup.Add(currUser.Id);
                            tasks.Where(q => empOfGroup.Contains(q.Processor));
                            break;
                        }
                    case 3:
                        {
                            tasks.Where(q => q.Processor == userID);
                            break;
                        }
                }
                
                if(status != 0)
                {
                    tasks.Where(q => q.Status == status);
                }

                var taskListModel = _mapper.Map<List<TaskViewModels>>(tasks);
                return Ok(new ResponseModels { Success = true , Data = taskListModel});
            }
            catch(Exception e)
            {
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }

        [HttpPut("{taskID}/{confirm}")]
        public IActionResult ConfrimEnd(int taskID, bool confirm, IFormFile img)
        {
            try
            {
                var task = _context.Tasks.FirstOrDefault(q => q.TaskId == taskID);
                task.ConfirmationEnded = confirm;
                _context.Tasks.Update(task);
                _context.SaveChanges();
                return Ok(new ResponseModels { Message = "", Success = true });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }
    }
}