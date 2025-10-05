using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.BusinessLogic.Dtos.User;
using Todo.Entities.Entity;

namespace Todo.BusinessLogic.IServices
{
    public interface IUserService  : IGenericService<User, UserReadDto, UserRegisterDto>
    {
        Task<bool> RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
    }
}
