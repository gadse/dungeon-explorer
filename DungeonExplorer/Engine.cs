using System;
using System.Collections.Generic;

namespace DungeonExplorer {

    class Event {
        public Character Source {get; private set;}
        public Character Target {get; private set;}
        public string Description {get; private set;}

        public Event(Character source, Character target, string description) {
            Source = source;
            Target = target;
            Description = description;
        }
    }
    
    struct SimulationResult {
        public readonly List<Character> Party;
        public readonly List<Character> Enemies;
        public readonly long Rounds;
        public readonly Boolean PartyVictorious;
        public readonly List<Event> EventLog;

        public SimulationResult(
            List<Character> party,
            List<Character> enemies,
            long rounds,
            Boolean partyVictorious,
            List<Event> eventLog
        ) {
            Party = party;
            Enemies = enemies;
            Rounds = rounds;
            PartyVictorious = partyVictorious;
            EventLog = eventLog;    
        }
    }

   class Engine {
        public static SimulationResult Simulate(List<Character> party, List<Character> enemies, Boolean partyBegins) {
            throw new System.NotImplementedException();
        }
    }

} // namespace DungeonExplorer