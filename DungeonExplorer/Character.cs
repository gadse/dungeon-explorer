using System;

class Character {
        public string Name { get; private set; }
        public long HealthPoints { get; private set; }
        public long AverageDamagePerRound { get; private set; }
        public long ResourceLimit { get; private set; }
        
        /*
        This model is extremely simplified. In the end, the Game Master needs to utilize their experience to find
        reasonable values.
        */
        public Character(
            string name,
            long healthPoints,
            long averageDamagePerRound, 
            long resourceLimit
        ) {
            
            Name = name;
            HealthPoints = healthPoints;
            AverageDamagePerRound = averageDamagePerRound;
            ResourceLimit = resourceLimit;
        }

        override public string ToString() {
            return String.Format(
                "{0} | {1} HP | {2} DMG/RD | {3} RES",
                Name,
                HealthPoints,
                AverageDamagePerRound,
                ResourceLimit
            );
        }
    } // namespace DungeonExplorer