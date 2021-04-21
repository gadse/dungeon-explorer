using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents the results of a completed simulation.
    /// </summary>
    /// 
    /// Implementation Notes:
    ///     I'd really like to make this a record in the future.
    public class SimulationResult
    {
        public List<Character> Party
        {
            get;
        }
        public List<Character> Enemies
        {
            get;
        }
        public long Rounds
        {
            get;
        }
        public bool PartyVictorious
        {
            get;
        }
        public List<Event> EventLog
        {
            get;
        }

        public SimulationResult(
            List<Character> party,
            List<Character> enemies,
            long rounds,
            bool partyVictorious,
            List<Event> eventLog
        )
        {
            Party = party;
            Enemies = enemies;
            Rounds = rounds;
            PartyVictorious = partyVictorious;
            EventLog = eventLog;
        }

        override public string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (PartyVictorious)
            {
                sb.AppendLine("The party was victorious! :D ");
            }
            else
            {
                sb.AppendLine("The party was defeated. :( ");
            }
            sb.AppendLine("");
            sb.AppendLine("Party's end condition:");
            foreach (Character member in Party)
            {
                sb.AppendLine(member.ToString());
            }
            sb.AppendLine("");
            sb.AppendLine("Enemies' end condition:");
            foreach (Character enemy in Enemies)
            {
                sb.AppendLine(enemy.ToString());
            }
            sb.AppendLine("");
            sb.AppendLine(String.Format("Rounds fought: {0}", Rounds));
            sb.AppendLine("");
            sb.AppendLine("====== DETAILED EVENT LOG ======");
            foreach (Event e in EventLog)
            {
                sb.AppendLine(e.ToString());
            }
            return sb.ToString();
        }
    }



} // namespace DungeonExplorer