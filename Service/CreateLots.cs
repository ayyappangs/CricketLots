using CricketLots.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketLots.Service
{
    public class CreateLots
    {
        public List<string> GetLotsSameGroup(LotsRequest lotsRequest)
        {
            try
            {
                
                Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
                Dictionary<string, int> teamTotalGames = new Dictionary<string, int>();
                List<Slot> slots = new List<Slot>();
                List<string> completedTeams = new List<string>();
                List<int> completedTeamsNumbers = new List<int>();


                foreach (var item in lotsRequest.Teams)
                {
                    keyValuePairs.Add(lotsRequest.Teams.IndexOf(item) + 1, item);
                }

                foreach (var item in lotsRequest.Teams)
                {
                    teamTotalGames.Add(item, 0);
                }

                while (completedTeams.Count != lotsRequest.Teams.Count)
                {
                    
                    var randomTeam = new Random();
                    var possibilities1 = Enumerable.Range(1, lotsRequest.Teams.Count).ToList();
                    var result1 = possibilities1.Where(x=>!completedTeamsNumbers.Any(y => y == x)).OrderBy(number => randomTeam.Next()).Take(1);

                    var item = keyValuePairs[result1.First()];

                    var random = new Random();  
                    var possibilities = Enumerable.Range(1, lotsRequest.Teams.Count).ToList();
                    var result = possibilities.Where(x => (x != lotsRequest.Teams.IndexOf(item) + 1 && !completedTeamsNumbers.Any(y => y == x))).OrderBy(number => random.Next()).Take(1).ToArray();
                    
                    if(result.Count() == 0)
                    {
                        keyValuePairs.Clear();
                        teamTotalGames.Clear();
                        slots.Clear();
                        completedTeams.Clear();
                        completedTeamsNumbers.Clear();

                        foreach (var item1 in lotsRequest.Teams)
                        {
                            keyValuePairs.Add(lotsRequest.Teams.IndexOf(item1) + 1, item1);
                        }

                        foreach (var item2 in lotsRequest.Teams)
                        {
                            teamTotalGames.Add(item2, 0);
                        }
                    }

                    foreach (var i in result)
                    {
                        if (teamTotalGames[item] < lotsRequest.NumberOfGames && teamTotalGames[keyValuePairs[i]] < lotsRequest.NumberOfGames)
                        {
                            if(!slots.Any(x=>x.Match ==$"{item} vs {keyValuePairs[i]}") || !slots.Any(x => x.Match == $"{keyValuePairs[i]} vs {item}"))
                            {
                                Slot slot = new Slot();
                                slot.Team1 = item;
                                slot.Team2 = keyValuePairs[i];
                                teamTotalGames[item] = teamTotalGames[item] + 1;
                                teamTotalGames[slot.Team2] = teamTotalGames[slot.Team2] + 1;
                                slot.Venue = lotsRequest.GroundName;
                                slot.Match = $"{item} vs {keyValuePairs[i]}";
                                slots.Add(slot);
                            }
                           
                        }

                    }                    

                    var completedTeamsGroup = teamTotalGames.Where(x => x.Value == lotsRequest.NumberOfGames);
                    foreach (var complete in completedTeamsGroup)
                    {
                        if (!completedTeams.Contains(complete.Key))
                        {
                            completedTeams.Add(complete.Key);
                            completedTeamsNumbers.Add(lotsRequest.Teams.IndexOf(complete.Key) + 1);
                        }

                    }
                }

                var matches = string.Join(Environment.NewLine,slots.Select(x => x.Match).ToList());

                return slots.Select(x => x.Match).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public List<string> GetLotsDiffGroup()
        {
            List<string> teamGroup = new List<string>() { "Beta", "Gladiators", "San Risers", "Spartans", "Bolts", "Thalaivas"};
            List<string> teamGroup1 = new List<string>() { "Knightmares", "Sharks", "Marvels", "Risers", "Wolfpack", "Royals" };
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
            List<Slot> slots = new List<Slot>();


            foreach (var item in teamGroup1)
            {
                keyValuePairs.Add(teamGroup1.IndexOf(item) + 1, item);
            }

            foreach (var item in teamGroup)
            {
                var random = new Random();
                var possibilities = Enumerable.Range(1, 6).ToList();
                var result = possibilities.OrderBy(number => random.Next()).Take(2).ToArray();
                foreach (var i in result)
                {
                    Slot slot = new Slot();
                    slot.Venue = "TomSlick";
                    slot.Match = $"{item} vs {keyValuePairs[i]}";
                    slots.Add(slot);
                }
            }
            return slots.Select(x => x.Match).ToList();
        }
    }

    public class Slot
    {
        public string Match { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public string Venue { get; set; }
    }
}
