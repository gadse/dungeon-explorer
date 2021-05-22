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
    public class ResultAggregator
    {

        /// <summary>
        /// Aggregates a bunch of Simulation Results.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static AggregatedSimulationResult AggregateResults(IEnumerable<SimulationResult> results)
        {
            var table = from result in results
                        select (
                            rounds: result.Rounds,
                            partyVictorious: result.PartyVictorious,
                            party: result.Party,
                            enemies: result.Enemies
                        );

            var statistics = MeasureStatistics(table);
            var distributions = MeasureDistributions(table);

            return new AggregatedSimulationResult(statistics, distributions);
        }


        public static Dictionary<string, double> MeasureStatistics(
            IEnumerable<(long, bool, List<Character>, List<Character>)> table
        )
        {
            var result = new Dictionary<String, double>();
            result["Rounds"] = table.Select(row => row.Item1).Average();
            result["PartyVictorious"] = table.Select(row => row.Item2 ? 1 : 0).Average();
            result["PartySurvivors"] = table.Select(row => row.Item3.Count).Average();
            result["EnemySurvivors"] = table.Select(row => row.Item4.Count).Average();
            return result;
        }

        public static Dictionary<string, IEnumerable<int>> MeasureDistributions(
            IEnumerable<(long, bool, List<Character>, List<Character>)> table
        )
        {
            var result = new Dictionary<string, IEnumerable<int>>();
            result["PartySurvivorCountDistribution"] = MeasurePartySurvivorDistribution(table);
            result["EnemySurvivorCountDistribution"] = MeasureEnemySurvivorDistribution(table);
            return result;
        }

        public static IEnumerable<int> MeasurePartySurvivorDistribution(
            IEnumerable<(long, bool, List<Character>, List<Character>)> table
        )
        {
            int partySize = table.First().Item3.Count;
            List<List<Character>> partyPerIteration = table.Select(row => row.Item3).ToList();
            int[] survivorDistribution = MeasureSurvivorDistribution(partySize, partyPerIteration);

            return survivorDistribution;
        }

        private static IEnumerable<int> MeasureEnemySurvivorDistribution(
            IEnumerable<(long, bool, List<Character>, List<Character>)> table
        )
        {
            int enemyFactionSize = table.First().Item4.Count;
            List<List<Character>> enemiesPerIteration = table.Select(row => row.Item4).ToList();
            int[] distribution = MeasureSurvivorDistribution(enemyFactionSize, enemiesPerIteration);

            return distribution;
        }

        private static int[] MeasureSurvivorDistribution(int factionSize, List<List<Character>> enemiesPerIteration)
        {
            int[] distribution = Enumerable.Repeat(0, factionSize).ToArray();

            foreach (List<Character> iteration in enemiesPerIteration)
            {
                int survivorCount = iteration.Where(x => x.HealthPoints > 0).Count();
                distribution[survivorCount] += 1;
            }

            return distribution;
        }
    }
}
