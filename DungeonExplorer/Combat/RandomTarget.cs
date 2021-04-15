using System.Collections.Generic;
using System;


namespace DungeonExplorer.Combat
{
    /// <summary>
    /// Selects a random enemy.
    /// </summary>
    class RandomTarget : CombatBehavior
    {
        // TODO: If our actingCharacter has supportive actions, consider those!
        public override Character selectTarget(
            Character actingCharacter,
            List<Character> ownPartyMembers,
            List<Character> enemies
        )
        {
            return enemies[new Random().Next(enemies.Count)];
        }
    }

}
