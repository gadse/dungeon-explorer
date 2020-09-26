using System;
using System.Collections.Generic;
using System.Linq;

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

        override public string ToString() {
            string sourceTitle;
            if (Source != null) {
                sourceTitle = Source.Name;
            } else {
                sourceTitle = "";
            }

            string targetTitle;
            if (Target != null) {
                targetTitle = Target.Name;
            } else {
                targetTitle = "";
            }

            return String.Format(
                "{0} | {1} | {2}",
                sourceTitle,
                targetTitle,
                Description
            );
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

        override public string ToString() {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("Party's end condition:");
            foreach (Character member in Party) {
                sb.AppendLine(member.ToString());
            }
            sb.AppendLine("Enemies' end condition:");
            foreach (Character enemy in Enemies) {
                sb.AppendLine(enemy.ToString());
            }
            sb.AppendLine(String.Format("Rounds fought: {0}", Rounds));
            if (PartyVictorious) {
                sb.AppendLine("The party was victorious! :D ");
            } else {
                sb.AppendLine("The party was defeated. :( ");
            }
            sb.AppendLine("Event Log:");
            foreach (Event e in EventLog) {
                sb.AppendLine(e.ToString());
            }
            return sb.ToString();
        }
    }

   class Engine {
        public static SimulationResult Simulate(
            in List<Character> party,
            in List<Character> enemies,
            Boolean partyBegins
        ) {
            Boolean partyTurn = partyBegins;
            List<Character> partyWorkingCopy = party.ConvertAll(c => new Character(c));
            List<Character> enemiesWorkingCopy = enemies.ConvertAll(c => new Character(c));
            List<Event> eventLog = new List<Event>();
            long rounds = 0;

            while(FactionIsAlive(partyWorkingCopy) && FactionIsAlive(enemiesWorkingCopy)) {
                Console.WriteLine("computing round...");
                eventLog.AddRange(
                    // No need to pass a ref here
                    ComputeRound(partyWorkingCopy, enemiesWorkingCopy, partyTurn)
                );
                partyTurn = !partyTurn;
                rounds += 1;
            }

            Boolean partyVictorious = FactionIsAlive(partyWorkingCopy);
            return new SimulationResult(
                partyWorkingCopy,
                enemiesWorkingCopy,
                rounds,
                partyVictorious,
                eventLog
            );
        }

        private static List<Event> ComputeRound(
            List<Character> party,
            List<Character> enemies,
            in Boolean partyTurn
        ) {
            if (partyTurn) {
                return ComputeRound(party, enemies);
            } else {
                return ComputeRound(enemies, party);
            }
        }

        private static List<Event> ComputeRound(List<Character> activeFaction, List<Character> otherFaction) {
            List<Event> eventLog = new List<Event>();

            Character mostDangerousOpponent = otherFaction.OrderByDescending(opp => opp.HealthPoints).First();
            foreach (Character member in activeFaction) {
                Boolean resourcesAreRelevant = (member.Resources != Constants.NO_RESOURCES_NEEDED);
                long damage = member.AverageDamagePerRound;

                if (resourcesAreRelevant) {
                    if (member.Resources > 0) {
                        eventLog.Add(new Event(
                            new Character(member), null, "spends resource"
                        ));
                        member.Resources -= 1;
                        damage = damage; // B e explicit about not changing it!
                    } else {
                        eventLog.Add(new Event(
                            new Character(member), null, String.Format("is out of resources")
                        ));
                        damage = damage / 2;  // Integer division on purpose
                    }
                } else {
                    damage = damage; // B e explicit about not changing it!
                }
                mostDangerousOpponent.HealthPoints -= damage;
                eventLog.Add(new Event(
                    new Character(member),
                    new Character(mostDangerousOpponent),
                    String.Format("attack for {0} DMG", damage)
                ));
                if (mostDangerousOpponent.HealthPoints <= 0) {
                    otherFaction.Remove(mostDangerousOpponent);
                    eventLog.Add(new Event(
                    new Character(mostDangerousOpponent), null, "faints"
                ));
                }
            }
            return eventLog;
        }

        private static Boolean FactionIsAlive(in List<Character> faction) {
            if (faction.Count == 0) {
                return false;
            } else {
                foreach (Character c in faction) {
                    if (c.HealthPoints > 0) {
                        return true;
                    }
                }
                return false;
            }
        }
    }

    

} // namespace DungeonExplorer