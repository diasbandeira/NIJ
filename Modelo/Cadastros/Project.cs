using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Cadastros
{
    public class Project
    {
        public long? ProjectId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
