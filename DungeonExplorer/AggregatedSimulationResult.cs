using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonExplorer
{
    public class AggregatedSimulationResult
    {
        public Dictionary<string, double> Statistics
        {
            get;
        }

        public Dictionary<string, IEnumerable<int>> Distributions
        {
            get;
        }

        public AggregatedSimulationResult(
            Dictionary<string, double> statistics,
            Dictionary<string, IEnumerable<int>> distributions
        )
        {
            Statistics = statistics;
            Distributions = distributions;
        }
    }
}
