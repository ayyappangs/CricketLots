using CricketLots.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketLots.Service
{
    public class CreateMatchSlotsService
    {
        public Dictionary<int, List<string>> CreateMatchSlotsForIPL(MatchSlotsRequest matchSlotsRequest)
        {
            try
            {
                List<MatchSlots> matchSlots = new List<MatchSlots>();
                Dictionary<int, int> weekvsmatches = new Dictionary<int, int>();
                List<int> numberOfWeeks = new List<int>() { 1, 2, 3, 4, 5, 6,7 };
                Dictionary<int, List<string>> matchesPerWeek = new Dictionary<int, List<string>>();
                List<Match> matches = new List<Match>();
                foreach (var item in matchSlotsRequest.AllMatches)
                {
                    Match match = new Match();
                    match.MatchId = matchSlotsRequest.AllMatches.IndexOf(item) + 1;
                    match.MatchName = item;
                    var teams = match.MatchName.Split("vs");
                    match.Team1 = teams[0].Trim();
                    match.Team2 = teams[1].Trim();
                    matches.Add(match);
                }
                
                foreach (var item in numberOfWeeks)
                {
                    List<MatchSlots> matchSlotsPerWeek = new List<MatchSlots>();
                    List<string> teamsDoneFortheWeek = new List<string>();
                    while (matchSlotsPerWeek.Count < 3)
                    {
                        try
                        {
                            var matchSelected = matches.Where(y => !matchSlots.Any(z => z.Match == y.MatchName) &&
                        (!teamsDoneFortheWeek.Contains(y.Team1) && !teamsDoneFortheWeek.Contains(y.Team2)))?
                        .OrderBy(x => x.MatchId)?.First();
                            if (matchSelected != null)
                            {
                                MatchSlots matchSlots1 = new MatchSlots();
                                matchSlots1.MatchId = matchSelected.MatchId;
                                matchSlots1.Match = matchSelected.MatchName;
                                var teams = matchSlots1.Match.Split("vs");
                                matchSlots1.Team1 = teams[0].Trim();
                                matchSlots1.Team2 = teams[1].Trim();
                                teamsDoneFortheWeek.Add(teams[0].Trim());
                                teamsDoneFortheWeek.Add(teams[1].Trim());
                                matchSlotsPerWeek.Add(matchSlots1);

                                matchSlots.Add(matchSlots1);
                            }
                        }
                        catch (Exception exception)
                        {

                            break;
                        }
                        
                    }
                    matchesPerWeek.Add(item, matchSlotsPerWeek.Select(x=>x.Match).ToList());

                }

                return matchesPerWeek;

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
