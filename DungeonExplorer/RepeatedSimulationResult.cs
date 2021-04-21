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
    public class RepeatedSimulationResult : IEnumerable<SimulationResult>
    {
        public List<SimulationResult> Results
        {
            get;
        } = new List<SimulationResult>();

        public void AddResult(SimulationResult res)
        {
            this.Results.Add(res);
        }

        public Object AggregatedResults() 
        {
            Dictionary<string, IEnumerable<Type>> valuesByProperty = new Dictionary<string, IEnumerable<Type>>();
            foreach ((string propertyName, Type t) in GetAvailableProperties())
            {
                // 
                valuesByProperty.Add(propertyName, GetResultValues<t>(propertyName));
            }
            return valuesByProperty;
        }

        public IEnumerator<SimulationResult> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<(string, Type)> GetAvailableProperties()
        {
            return typeof(SimulationResult).GetProperties().Select(r => (r.Name, r.PropertyType));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<T> GetResultValues<T>(string property)
        {
            List<T> values = new List<T>();
            foreach (SimulationResult r in Results)
            {
                T value = (T) r.GetType().GetProperty(property).GetValue(r);
                values.Add(value);
            }
            return values;
        }
    }
}
