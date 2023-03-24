using System;
using System.Collections.Generic;
using System.Text;

namespace CricketLots.Model
{
    public class MatchSlotsRequest
    {
        public List<string> AllMatches { get; set; }

        public int MaxMacthesPerWeek { get; set; }

        public int SatCount { get; set; }

        public int SunCount { get; set; }
    }

    public class Match
    {
        public int MatchId { get; set; }

        public string MatchName { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }


    }
}
