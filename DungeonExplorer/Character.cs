using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    class Character
    {
        public string Name
        {
            get;
        }
        public long HealthPoints
        {
            get; set;
        }
        public long AverageDamagePerRound
        {
            get;
        }
        public long Resources
        {
            get; set;
        }

        public List<(long damage, long resourceCost)> SpecialAttacks
        {
            get;
        } = new List<(long damage, long resourceCost)>();

        public List<(long healing, long resourceCost)> HealActions
        {
            get;
        } = new List<(long healing, long resourceCost)>();

        /// <summary>
        /// This model is extremely simplified. In the end, the Game Master needs to utilize their experience to find
        /// reasonable values.
        /// </summary>
        public Character(
            string name,
            long healthPoints,
            long averageDamagePerRound,
            long resources
        )
        {
            if (resources > -1)
            {
                WithBasicStats(name, healthPoints, averageDamagePerRound);
            } else {
                WithBasicStats(name, healthPoints, averageDamagePerRound).WithResources(resources);
            }
        }

        public Character(Character c)
        {
            Name = c.Name;
            HealthPoints = c.HealthPoints;
            AverageDamagePerRound = c.AverageDamagePerRound;
            Resources = c.Resources;
        }

        public Character WithBasicStats(string name, long healthPoints, long averageDamagePerRound)
        {
            return new Character(name, healthPoints, averageDamagePerRound, Constants.NO_RESOURCES_NEEDED);
        }
        public Character WithResources(long resources)
        {
            if (resources < 0)
            {
                throw new ArgumentOutOfRangeException("A character can't have less than 0 resources.");
            } else {
                Character character = new Character(this);
                character.Resources = resources;
                return character;
            }
        }
        public Character WithSpecialAttack(long damage, long resourceCost)
        {
            if (damage < 1)
            {
                throw new ArgumentOutOfRangeException("A character can't have special attacks with less than 1 damage.");
            } else {
                Character character = new Character(this);
                character.SpecialAttacks.Add((damage, resourceCost));
                return character;
            }
        }
        public Character WithHealAction(long regeneration, long resourceCost)
        {
            if (regeneration < 1)
            {
                throw new ArgumentOutOfRangeException("A character can't have heal actions with less than 1 regeneration.");
            } else {
                Character character = new Character(this);
                character.HealActions.Add((regeneration, resourceCost));
                return character;
            }
        }

        override public string ToString()
        {
            String resourceLimitRepresentation;
            // This is way prettier than the ternary operator
            if (Resources != Constants.NO_RESOURCES_NEEDED)
            {
                resourceLimitRepresentation = Resources.ToString();
            }
            else
            {
                resourceLimitRepresentation = "x";
            }
            return String.Format(
                "{0} | {1} HP | {2} DMG/RD | {3} RES",
                Name,
                HealthPoints,
                AverageDamagePerRound,
                resourceLimitRepresentation
            );
        }
    }
} // namespace DungeonExplorer
