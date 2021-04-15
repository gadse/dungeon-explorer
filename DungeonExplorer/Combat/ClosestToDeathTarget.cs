using System.Collections.Generic;
using System.Linq;


namespace DungeonExplorer.Combat
{

    /// <summary>
    /// Selects the enemy with the lowest health points.
    /// </summary>
    class ClosestToDeathTarget : CombatBehavior
    {
        // TODO Temporarily add some random HP values, since the actingCharacter usually has no way
        //      of telling how many health points our enemies have exactly.
        public override Character selectTarget(
            Character actingCharacter,
            List<Character> ownPartyMembers,
            List<Character> enemies
        )
        {
            return enemies.OrderBy(opp => opp.HealthPoints).First();
        }
    }

}
