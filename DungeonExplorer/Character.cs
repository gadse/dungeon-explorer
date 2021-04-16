using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Character
    {
        public string Name
        {
            get; private set;
        }
        public long HealthPoints
        {
            get; set;
        }
        public long AverageDamagePerRound
        {
            get; private set;
        }
        public long Resources
        {
            get; set;
        }
        public List<(long damage, long resourceCost)> SpecialAttacks
        {
            get; private set;
        } = new List<(long damage, long resourceCost)>();
        public List<(long healing, long resourceCost)> HealActions
        {
            get; private set;
        } = new List<(long healing, long resourceCost)>();

        public Character(Character c)
        {
            init(
                c.Name,
                c.HealthPoints,
                c.AverageDamagePerRound,
                c.Resources,
                c.SpecialAttacks,
                c.HealActions
            );
        }
        
        public Character(
            string name,
            long healthPoints,
            long averageDamagePerRound,
            long resources
        )
        {
            init(
                name,
                healthPoints,
                averageDamagePerRound,
                resources,
                new List<(long healing, long resourceCost)>(),
                new List<(long healing, long resourceCost)>()
            );
        }

        public Character(
            string name,
            long healthPoints,
            long averageDamagePerRound,
            long resources,
            List<(long healing, long resourceCost)> specialAttacks,
            List<(long healing, long resourceCost)> healActions
        )
        {
            init(
                name,
                healthPoints,
                averageDamagePerRound,
                resources,
                specialAttacks,
                healActions
            );
        }

        private void init(
            string name,
            long healthPoints,
            long averageDamagePerRound,
            long resources,
            List<(long healing, long resourceCost)> specialAttacks,
            List<(long healing, long resourceCost)> healActions
        )
        {
            this.Name = name;
            this.HealthPoints = healthPoints;
            this.AverageDamagePerRound = averageDamagePerRound;
            this.Resources = resources;
            this.SpecialAttacks = specialAttacks;
            this.HealActions = healActions;
        }

        override public string ToString()
        {
            String resourceLimitRepresentation;
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
