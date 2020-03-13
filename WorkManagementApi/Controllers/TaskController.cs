using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkManagementApi.Models;
using WorkManagementApi.ViewModels;

namespace WorkManagementApi.Controllers
{
    [Route("Task")]
    public class TaskController : Controller
    {
        private WorkManagementContext context;
        private readonly IMapper _mapper;
        private IHostingEnvironment _hostingEnvironment;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public TaskController(IMapper mapper, WorkManagementContext context, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            this._mapper = mapper;
            this._hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("")]
        public IActionResult CreateTask([FromQuery]TaskViewModels model)
        {
            try
            {
                context.Tasks.Add(_mapper.Map<Tasks>(model));
                context.SaveChanges();
                return Ok(new ResponseModels { Success = true, Message = "Create task succeed" });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }

        [HttpPut("{taskID}/{isAccepted}")]
        public IActionResult AcceptATask(int taskID, bool isAccepted)
        {
            try
            {
                var task = context.Tasks.FirstOrDefault(q => q.TaskId == taskID);
                task.Acceptance = isAccepted;
                context.Tasks.Update(task);
                context.SaveChanges();

                if (isAccepted)
                {
                    return Ok(new ResponseModels { Message = "Task was accepted", Success = true });
                }
                return Ok(new ResponseModels { Message = "Task was declined", Success = true });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }

        [HttpGet("{taskID}")]
        public IActionResult GetTaskDetail(int taskID)
        {
            try
            {
                var task = context.Tasks.FirstOrDefault(q => q.TaskId == taskID);
                var taskModel = _mapper.Map<TaskViewModels>(task);


                taskModel.CreatorName = task.CreatorNavigation.Fullname;
                taskModel.ProcesssorName = task.ProcessorNavigation.Fullname;
                taskModel.StatusName = task.StatusNavigation.StatusName;

                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, task.ImageConfirmation); ;
                taskModel.Image = System.IO.File.ReadAllBytes(filePath);

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
            catch (Exception e)
            {
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }

        [HttpPost("{taskID}")]
        public IActionResult ModifyTask(TaskViewModels model)
        {
            try
            {
                context.Tasks.Update(_mapper.Map<Tasks>(model));
                context.SaveChanges();

                return Ok(new ResponseModels { Message = "Update task succeed", Success = true });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }

        [HttpGet("")]
        public IActionResult GetTask(int currUserID, int userID, string timeS = "", string timeE = "", int status = 0)
        {
            try
            {
                var tasks = context.Tasks.Where(q => q.CreationTime <= DateTime.Parse(timeE) && q.CreationTime >= DateTime.Parse(timeS));
                var currUser = context.Users.FirstOrDefault(q => q.Id == currUserID);

                switch (currUser.RoleId)
                {
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            var empOfGroup = context.Users.Where(q => q.GroupId == currUser.Group.GroupId).Select(q => q.Id).ToList();
                            tasks.Where(q => empOfGroup.Contains(q.Processor));
                            break;
                        }
                    case 3:
                        {
                            tasks.Where(q => q.Processor == userID);
                            break;
                        }
                }

                if (status != 0)
                {
                    tasks.Where(q => q.Status == status);
                }

                var taskListModel = _mapper.Map<List<TaskViewModels>>(tasks);
                return Ok(new ResponseModels { Success = true, Data = taskListModel });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }

        [HttpPut("Confirm/{taskID}/{confirm}")]
        public async Task<IActionResult> ConfrimEndAsync(int taskID, bool confirm, IFormFile img)
        {
            try
            {
                var task = context.Tasks.FirstOrDefault(q => q.TaskId == taskID);
                task.ConfirmationEnded = confirm;

                var date = DateTime.Now;
                task.TimeEnd = date;

                var fileName = $"{task.ProcessorNavigation.Username}-{task.TaskId}-{date.ToString("yyyyMMdd-hhmmss")}.jpg";
                var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
                {
                    if (img.Length > 0)
                    {
                        var filePath = Path.Combine(uploads, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(fileStream);
                        }
                        task.ImageConfirmation = $"uploads\\{fileName}";
                    }
                }

                if (confirm)
                {
                    task.Status = 3;
                }
                else
                {
                    task.Status = 5;
                }

                context.Tasks.Update(task);
                context.SaveChanges();
                return Ok(new ResponseModels { Message = "", Success = true });
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return StatusCode(500, new ResponseModels { Success = false, Message = "Something went wrong" });
            }
        }
    }
}