using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Application.DTOs;
using AutoMapper;



namespace Application.Mappings
{
    public class UserTaskProfile : Profile
    {
        public UserTaskProfile()
        {
            CreateMap<UserTask, UserTaskDTO>().ReverseMap();
            CreateMap<UserTask, CreateUserTaskDTO>().ReverseMap();
        }
    }
}