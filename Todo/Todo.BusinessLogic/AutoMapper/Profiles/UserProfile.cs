using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.BusinessLogic.Dtos.User;
using Todo.Entities.Entity;

namespace Todo.BusinessLogic.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserReadDto>();
        }
    }
}
