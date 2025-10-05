using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Utilities;

namespace Todo.Entities.Entity
{
    public class Todo : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.Pending;
        //public long UserId { get; set; }
        //public User User { get; set; } = null!;
    }
}
