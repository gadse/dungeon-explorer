using System.Collections.Generic;


namespace DungeonExplorer.Combat
{
    /// <summary>
    /// Base for different combat behaviors (strategy pattern).
    /// 
    /// I still don't now where to put the factory methods other than here, but that'll come with time.
    /// </summary>
    public abstract class CombatBehavior
    {
        public abstract Character selectTarget(
            Character actingCharacter,
            List<Character> ownPartyMembers,
            List<Character> enemies
        );

        public static CombatBehavior Default() {
            return new MostThreateningEnemyTarget();
        }

        public static CombatBehavior Random() {
            return new RandomTarget();
        }
    }
}