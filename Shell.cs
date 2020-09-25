using System;
using System.Collections.Generic;

namespace dungeon_explorer {
    class Shell {
        static void Main(string[] args) {

            Console.WriteLine("Hi there! Welcome to the dungeon explorer.");

            Console.WriteLine("Let's get to know our party!");
            List<Character> characters = QueryPartyMembers();

            Console.WriteLine("Now let's talk about the enemies our party faces.");
            List<Character> enemies = QueryEnemies();
            
            Console.WriteLine("====== THE PARTY ======");
            foreach (Character character in characters) {
                Console.WriteLine(character.ToString());
            }
            Console.WriteLine("====== THE ENEMIES ======");
            foreach (Character enemy in enemies) {
                Console.WriteLine(enemy.ToString());
            }

            Console.WriteLine("Processing...");
            List<Event> eventLog = Engine.Simulate(characters, enemies, true);
            Console.WriteLine(eventLog.ToString());

            Console.WriteLine("kthxbye ^w^");
        }

        private static List<Character> QueryPartyMembers() {
            List<Character> characters = new List<Character>();
            Boolean done_with_characters = false;
            while (!done_with_characters) {
                Console.WriteLine(
                    String.Format(
                        "Please enter the name of character {0} or press ENTER to start the program.",
                        characters.Count + 1
                    )
                );
                String name = Console.ReadLine();
                if (name.Trim() != "") {
                    long healthPoints = ReadHealthFromConsole();
                    long averageDamage = ReadDamageFromConsole();
                    long resourceLimit = ReadResourcesFromConsole();
                    characters.Add(
                        new Character(
                            name: name,
                            healthPoints: healthPoints,
                            averageDamagePerRound: averageDamage,
                            resourceLimit: resourceLimit
                        )
                    );
                } else {
                    done_with_characters = true;
                }
            }

            return characters;
        }

        private static List<Character> QueryEnemies() {
            List<Character> enemies = new List<Character>();
            Boolean done_with_enemies = false;
            while (!done_with_enemies) {
                Console.WriteLine(
                    String.Format(
                        "Please enter the name of enemy {0} or press ENTER to start the program.",
                        enemies.Count + 1
                    )
                );
                String name = Console.ReadLine();
                if (name.Trim() != "") {
                    long healthPoints = ReadHealthFromConsole();
                    long averageDamage = ReadDamageFromConsole();
                    long resourceLimit = ReadResourcesFromConsole();
                    enemies.Add(
                        new Character(
                            name: name,
                            healthPoints: healthPoints,
                            averageDamagePerRound: averageDamage,
                            resourceLimit: resourceLimit
                        )
                    );
                } else {
                    done_with_enemies = true;
                }
            }
            return enemies;
        }

        private static long ReadHealthFromConsole() {
            Console.WriteLine("Health points?");
            long health = -1;
            while (health < 0) {
                try {
                    health = Convert.ToInt64(Console.ReadLine());
                } catch (System.FormatException) {
                    Console.WriteLine("Come again?");
                    health = -1;
                }
            } 
            return health;
        }

        private static long ReadDamageFromConsole() {
            Console.WriteLine("Average damage per round?");
            long average_damage = -1;
            while (average_damage < 0) {
                try {
                    average_damage = Convert.ToInt64(Console.ReadLine());
                } catch (System.FormatException) {
                    Console.WriteLine("Come again?");
                    average_damage = -1;
                }
            }
            return average_damage;
        }

        private static long ReadResourcesFromConsole() {
            Console.WriteLine("Resources available? (Spell slots etc.)");
            long ressources = -1;
            while (ressources < 0) {
                try {
                    ressources = Convert.ToInt64(Console.ReadLine());
                } catch (System.FormatException) {
                    Console.WriteLine("Come again?");
                    ressources = -1;
                }
            }
            return ressources;
        }
    }

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

        override public String ToString() {
            return String.Format(
                "{0} | {1} HP | {2} DMG/RD | {3} RES",
                Name,
                HealthPoints,
                AverageDamagePerRound,
                ResourceLimit
            );
        }
    }


    class Event {
        public Character Source {get; private set;}
        public Character Target {get; private set;}
        public string Description {get; private set;}

        public Event(Character source, Character target, string description) {
            Source = source;
            Target = target;
            Description = description;
        }
    }


    class Engine {
        public static List<Event> Simulate(List<Character> party, List<Character> enemies, Boolean partyBegins) {
            throw new System.NotImplementedException();
        }
    }
}