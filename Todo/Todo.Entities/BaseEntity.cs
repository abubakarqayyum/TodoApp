using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long ? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long ? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}

