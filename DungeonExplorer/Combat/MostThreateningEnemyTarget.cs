using System.Collections.Generic;
using System.Linq;


namespace DungeonExplorer.Combat
{
    /// <summary>
    /// Selects the enemy that seems the most threatening.
    /// </summary>
    class MostThreateningEnemyTarget : CombatBehavior
    {
        // TODO: What else could make an enemy seem threatening?
        // TODO: Can we assume players to know how much damage the enemies can dish out?
        public override Character selectTarget(
            Character actingCharacter,
            List<Character> ownPartyMembers,
            List<Character> enemies
        )
        {
            return enemies.OrderByDescending(opp => opp.HealthPoints).First();
        }
    }

}
