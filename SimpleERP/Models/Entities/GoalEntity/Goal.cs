using SimpleERP.Models.Entities.Auth;
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
        public Employe Assigne { get; set; }
        public int AssigneId { get; set; }
        public Manager Reporter { get; set; }
        public int ReporterId { get; set; }
        
    }
}
