using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Entities.Entity
{
    public class User : BaseEntity
    {
        
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
       
       // Navigation property for One-to-Many
      //  public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }
}
