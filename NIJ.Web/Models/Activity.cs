using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIJ.Web.Models
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
    }
}
