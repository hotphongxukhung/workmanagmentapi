using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WorkManagementAPI.Models;
using WorkManagementAPI.ViewModels;

namespace WorkManagementAPI.Global
{
    public class Global
    {
    }
    public class FunctionPlus
    {
        public static string GetMD5HashString(string str)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(str)).Select(s => s.ToString("x2")));
        }
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Users, UserViewModels>();
            CreateMap<UserViewModels, Users>();
            CreateMap<Tasks, TaskViewModels>();
            CreateMap<TaskViewModels, Tasks>();
            CreateMap<Groups, GroupViewModels>();
            CreateMap<GroupViewModels, Groups>();
        }
    }
}
