using System;

namespace SimpleERP.Models.API.Goal
{
    public class GoalModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateFinished { get; set; }
        public string AssigneId { get; set; }
        public string ReporterId { get; set; }


    }
}
