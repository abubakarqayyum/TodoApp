using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.BusinessLogic.Dtos.Todo;
using Todo.Entities.Entity;
using Todo.Utilities;
using Todo.Utilities.Extensions;

namespace Todo.BusinessLogic.AutoMapper.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<AddUpdateTodoDto, Todo.Entities.Entity.Todo>()
       .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (TodoStatus)src.Status));

            CreateMap<Todo.Entities.Entity.Todo, TodoReadDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EnumExtention.GetEnumDescriptionValue((TodoStatus)Convert.ToInt32(src.Status))));
        }
    }
}
