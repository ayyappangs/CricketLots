using System;
using System.Collections.Generic;
using System.Text;

namespace CricketLots.Model
{
    public class MatchSlots
    {
        public int MatchId { get; set; }
        public string Match { get; set; }
        public string GroundName { get; set; }
        public string Day { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }
    }
}
