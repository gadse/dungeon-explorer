﻿using System;
using System.Collections.Generic;

using DungeonExplorer.Combat;

namespace DungeonExplorer {
    class Shell {
        static void Main(string[] args) {
            Console.WriteLine("Hi there! Welcome to the dungeon explorer.");
            Console.WriteLine("Let's get to know our party!");
            List<Character> characters = QueryPartyMembers();
            Console.WriteLine("Now let's talk about the enemies our party faces.");
            List<Character> enemies = QueryEnemies();

            Console.WriteLine("====== OVERVIEW ======");
            Console.WriteLine("====== THE PARTY ======");
            foreach (Character character in characters) {
                Console.WriteLine(character.ToString());
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine("====== THE ENEMIES ======");
            foreach (Character enemy in enemies) {
                Console.WriteLine(enemy.ToString());
            }
            Console.WriteLine("-----------------------");

            Console.WriteLine("Processing...");
            SimulationResult result = Engine.Simulate(
                characters,
                enemies,
                true,
                CombatBehavior.Default(),
                CombatBehavior.Random()
            );

            Console.WriteLine("");
            Console.WriteLine("Simulation Complete!");
            Console.WriteLine("====== RESULTS =======");
            Console.WriteLine(result.ToString());
            Console.WriteLine("kthxbye ^u^");
        }

        private static List<Character> QueryPartyMembers() {
            List<Character> characters = new List<Character>();
            Boolean done_with_characters = false;
            while (!done_with_characters) {
                Console.WriteLine(
                    String.Format(
                        "Please enter the name of character {0} or press ENTER to start the program > ",
                        characters.Count + 1
                    )
                );
                String name = Console.ReadLine();
                if (name.Trim() != "") {
                    long healthPoints = ReadHealthFromConsole();
                    long averageDamage = ReadDamageFromConsole();
                    long resources = ReadResourcesFromConsole();
                    CharacterBuilder builder = new CharacterBuilder();
                    try
                    {
                        characters.Add(
                            builder.WithBasicStats(
                                name, healthPoints, averageDamage
                            ).WithResources(
                                resources
                            ).SafeBuild()
                        );
                    }
                    catch (ArgumentException) {
                        Console.WriteLine(
                            $"Something went wrong with {name}. " +
                            $"Maybe I've misunderstood (HP={healthPoints}, AVGDMG={averageDamage}, RES={resources}). " +
                            $"Let's try again!"
                        );
                        continue;
                    }
                }
                else
                {
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
                        "Please enter the name of enemy {0} or press ENTER to start the program > ",
                        enemies.Count + 1
                    )
                );
                String name = Console.ReadLine();
                if (name.Trim() != "") {
                    long healthPoints = ReadHealthFromConsole();
                    long averageDamage = ReadDamageFromConsole();
                    long resources = ReadResourcesFromConsole();
                    CharacterBuilder builder = new CharacterBuilder();
                    try
                    {
                        enemies.Add(
                            builder.WithBasicStats(
                                name,
                                healthPoints,
                                averageDamage
                            ).WithResources(
                                resources
                            ).SafeBuild()
                        );
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine(
                            $"Something went wrong with {name}. " +
                            $"Maybe I've misunderstood (HP={healthPoints}, AVGDMG={averageDamage}, RES={resources}). " +
                            $"Let's try again!"
                        );
                        continue;
                    }
                } else {
                    done_with_enemies = true;
                }
            }
            return enemies;
        }

        private static long ReadHealthFromConsole() {
            return ReadNonNegativeLongFromConsole(
                "Health points? [number expected]"
            );
        }

        private static long ReadDamageFromConsole() {
            return ReadNonNegativeLongFromConsole(
                "Average damage per round? [number expected]"
            );
        }

        private static long ReadResourcesFromConsole() {
            Console.Write(
                "Resources available? (Spell slots etc.) ['no' or number expected] > "
            );
            long inputNumber = -1;
            while (inputNumber != Constants.NO_RESOURCES_NEEDED && inputNumber < 1) {
                try {
                    string input = Console.ReadLine();
                    if (input == "no")
                    {
                        inputNumber = Constants.NO_RESOURCES_NEEDED;
                    }
                    else
                    {
                        inputNumber = Convert.ToInt64(input);
                    }
                } catch (System.FormatException) {
                    Console.WriteLine("Come again?");
                    inputNumber = -1;
                }
            }
            return inputNumber;
        }

        private static long ReadNonNegativeLongFromConsole(string prompt) {
            Console.Write(prompt + " > ");
            long input = -1;
            while (input < 0) {
                try {
                    input = Convert.ToInt64(Console.ReadLine());
                } catch (System.FormatException) {
                    Console.WriteLine("Come again?");
                    input = -1;
                }
            }
            return input;
        }
    }

} // namespace DungeonExplorer
