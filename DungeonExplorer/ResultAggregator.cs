using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents the results of a completed repeated simulation.
    /// </summary>
    /// 
    /// Implementation Notes:
    ///     I'd really like to make this a record in the future.
    /// 
    /// TODO:
    ///     Implement aggregation stuff using LINQ (Groupby, SelectMany, Aggregate)
    public class ResultAggregator
    {
        public Dictionary<string, object> AggregateResults(IEnumerable<SimulationResult> results)
        {
            var table = from result in results
                        select (
                            rounds: result.Rounds,
                            partyVictorious: result.PartyVictorious,
                            party: result.Party,
                            enemies: result.Enemies
                        );

            foreach (var res in table)
            {
                // TODO: Measure avg, median, min, max, distribution
                var statistics = MeasureStatistics(table);
                var distributions = MeasureDistributions(table);
            }
            return null;
        }


        Dictionary<string, double> MeasureStatistics(
            IEnumerable<(long, bool, List<Character>, List<Character>)> table
        )
        {
            var result = new Dictionary<String, double>();
            result["rounds"] = table.Select(row => row.Item1).Average();
            result["partyVictorious"] = table.Select(row => row.Item2 ? 1 : 0).Average();
            result["partySurvivors"] = table.Select(row => row.Item3.Count).Average();
            result["enemySurvivors"] = table.Select(row => row.Item4.Count).Average();
            return result;
        }
    }
}
