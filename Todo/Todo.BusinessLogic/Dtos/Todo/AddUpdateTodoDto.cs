using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Utilities;

namespace Todo.BusinessLogic.Dtos.Todo
{
    public class AddUpdateTodoDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TodoStatus Status { get; set; }
    }
}
