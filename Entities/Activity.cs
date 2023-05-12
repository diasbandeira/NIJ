using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIJ.Controllers
{
    public class Activity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public Status Status { get; set; }

        public void InsertActivity(string description, DateTime started)
        {
            if (description != null)
            {
                Description = description;
                Status = Status.Started;
                StartedAt = started;
            }
            else
            {
                throw new CustomExeception("A descrição da atividade não pode ser vazio");
            }

        }

    }
}
