using CricketLots.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CricketLots.Service
{
    public class CreateMatchesService
    {
        public List<string> CreateSameGroupMatches(LotsRequest lotsRequest)
        {
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            Dictionary<string, int> teamTotalGames = new Dictionary<string, int>();
            List<Slot> slots = new List<Slot>();
            List<string> completedTeams = new List<string>();
            List<string> opponentTeams = new List<string>();

            foreach (var item in lotsRequest.Teams)
            {
                keyValuePairs.Add(lotsRequest.Teams.IndexOf(item) + 1, item);
            }

            foreach (var item in lotsRequest.Teams)
            {
                teamTotalGames.Add(item, 0);
            }

            foreach (var item in lotsRequest.Teams)
            {
                opponentTeams = lotsRequest.Teams.Where(x => x != item).ToList();
                foreach (var opponent in opponentTeams)
                {
                    if (!slots.Any(x => x.Match == $"{item} vs {opponent}") && !slots.Any(x => x.Match == $"{opponent} vs {item}"))
                    {
                        Slot slot = new Slot();
                        slot.Team1 = item;
                        slot.Team2 = opponent;
                        teamTotalGames[item] = teamTotalGames[item] + 1;
                        teamTotalGames[slot.Team2] = teamTotalGames[slot.Team2] + 1;
                        slot.Venue = lotsRequest.GroundName;
                        slot.Match = $"{item} vs {opponent}";
                        slots.Add(slot);
                    }
                       
                }
               
            }


            return slots.Select(x => x.Match).ToList();

        }
    }
}
