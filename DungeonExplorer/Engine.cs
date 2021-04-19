using System;
using System.Collections.Generic;

using DungeonExplorer.Combat;

namespace DungeonExplorer
{

    /// <summary>
    /// Represents the results of a completed simulation.
    /// </summary>
    /// Implementation Notes:
    ///     This is implemented as a struct, since records are not available in .NET Core 3.1 without caveats.
    ///     (https://btburnett.com/csharp/2020/12/11/csharp-9-records-and-init-only-setters-without-dotnet5.html)
    public struct SimulationResult
    {
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

    public class Engine
    {
        public static SimulationResult Simulate(
            in List<Character> party,
            in List<Character> enemies,
            Boolean partyBegins,
            CombatBehavior playerBehavior,
            CombatBehavior enemyBehavior
        )
        {
            Boolean partyTurn = partyBegins;
            List<Character> partyWorkingCopy = party.ConvertAll(c => new Character(c));
            List<Character> enemiesWorkingCopy = enemies.ConvertAll(c => new Character(c));
            List<Event> eventLog = new List<Event>();
            long rounds = 0;

            while (FactionIsAlive(partyWorkingCopy) && FactionIsAlive(enemiesWorkingCopy))
            {
                Console.WriteLine("computing round...");
                eventLog.AddRange(
                    // No need to pass a ref here
                    ComputeRound(partyWorkingCopy, enemiesWorkingCopy, partyTurn, playerBehavior, enemyBehavior)
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
            in Boolean partyTurn,
            CombatBehavior playerBehavior,
            CombatBehavior enemyBehavior
        )
        {
            if (partyTurn)
            {
                return ComputeRound(party, enemies, playerBehavior);
            }
            else
            {
                return ComputeRound(enemies, party, enemyBehavior);
            }
        }

        private static List<Event> ComputeRound(
            List<Character> activeFaction,
            List<Character> otherFaction,
            CombatBehavior combatBehavior
        )
        {
            List<Event> eventLog = new List<Event>();

            foreach (Character member in activeFaction)
            {
                Character target = combatBehavior.selectTarget(member, activeFaction, otherFaction);
                Boolean resourcesAreRelevant = (member.Resources != Constants.NO_RESOURCES_NEEDED);
                long damage = member.AverageDamagePerRound;

                if (resourcesAreRelevant)
                {
                    if (member.Resources > 0)
                    {
                        member.Resources -= 1;
                        eventLog.Add(new Event(
                            new Character(member), null, "spent resource"
                        ));
                    }
                    else
                    {
                        eventLog.Add(new Event(
                            new Character(member), null, String.Format("is out of resources")
                        ));
                        damage = damage / 2;  // Integer division is on purpose
                    }
                }
                else
                {
                    // don't change anything right now
                }
                target.HealthPoints -= damage;
                eventLog.Add(new Event(
                    new Character(member),
                    new Character(target),
                    String.Format("attacked for {0} DMG", damage)
                ));
                if (target.HealthPoints <= 0)
                {
                    otherFaction.Remove(target);
                    eventLog.Add(new Event(
                    new Character(target), null, "faints"
                ));
                }
            }
            return eventLog;
        }

        private static Boolean FactionIsAlive(in List<Character> faction)
        {
            if (faction.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (Character c in faction)
                {
                    if (c.HealthPoints > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }



} // namespace DungeonExplorer