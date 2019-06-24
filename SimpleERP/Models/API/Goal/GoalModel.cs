using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.API.Goal
{
    public class GoalModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFinished { get; set; }
        public string AsignId { get; set; }
        public string ReporterId { get; set; }


    }
}
