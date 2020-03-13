using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagementApi.Models;
using WorkManagementApi.ViewModels;

namespace WorkManagementApi.Global
{
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
