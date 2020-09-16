using AutoMapper;
using FirstProject.Models.Entities;
using FirstProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Student, StudentViewModel>().ReverseMap();
        }
    }
}
