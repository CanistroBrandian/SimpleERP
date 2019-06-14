using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Entities.GoalEntity

{
    public class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFinished { get; set; }
        public string Reported { get; set; }
        public string Assigne { get; set; }
        public List<GoalEmploye> GoalEmployes { get; set; }


    }
}
