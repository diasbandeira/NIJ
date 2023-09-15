using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Cadastros
{
    public class Activity
    {
        public long? ActivityId { get; set; }
        public string Description { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public Status Status { get; set; }
        public long? ProjectId { get; set; }
        public Project Project { get; set; }
        //public virtual ICollection<UserActivity> UsersActivities { get; set; }

    }
}
