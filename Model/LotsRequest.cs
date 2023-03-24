using System;
using System.Collections.Generic;
using System.Text;

namespace CricketLots.Model
{
    public class LotsRequest
    {
        public List<string> Teams { get; set; }

        public string GroundName { get; set; }

        public int NumberOfGames { get; set; }
    }

    public class LotsRequestMultipleGroups
    {
        public List<string> Group1 { get; set; }

        public List<string> Group2 { get; set; }

        public string GroundName { get; set; }

        public int NumberOfGames { get; set; }
    }

    public class ScheduleExceptions
    {
        public string TeamName { get; set; }

        public List<string> ExceptionsTeams { get; set; }

    }
}
