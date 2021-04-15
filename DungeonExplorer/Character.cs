using System;

namespace DungeonExplorer
{
    class Character
    {
        public string Name
        {
            get; set;
        }
        public long HealthPoints
        {
            get; set;
        }
        public long AverageDamagePerRound
        {
            get; set;
        }
        public long Resources
        {
            get; set;
        }

        /*
        This model is extremely simplified. In the end, the Game Master needs to utilize their experience to find
        reasonable values.
        */
        public Character(
            string name,
            long healthPoints,
            long averageDamagePerRound,
            long resources
        )
        {

            Name = name;
            HealthPoints = healthPoints;
            AverageDamagePerRound = averageDamagePerRound;
            Resources = resources;
        }

        public Character(Character c)
        {
            Name = c.Name;
            HealthPoints = c.HealthPoints;
            AverageDamagePerRound = c.AverageDamagePerRound;
            Resources = c.Resources;
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
