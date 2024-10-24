using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Application.DTOs;
using AutoMapper;
using Domain.Enum;



namespace Application.Mappings
{
    public class UserTaskProfile : Profile
    {
        public UserTaskProfile()
        {
            CreateMap<UserTask, UserTaskDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (UserTaskStatus)src.Status));

            CreateMap<UserTask, UserTaskDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))  // Mapeia o enum para int
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (UserTaskStatus)src.Status));
        }
    }
}