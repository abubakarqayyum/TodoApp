using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Utilities
{
    public enum TodoStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("In-Progress")]
        InProgress = 2,
        [Description("Completed")]
        Completed = 3,
        [Description("Archived")]
        Archived = 4
    }
}
