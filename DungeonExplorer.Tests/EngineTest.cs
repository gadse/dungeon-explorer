using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

using DungeonExplorer.Combat;

namespace DungeonExplorer.Tests
{
    public class EngineTest
    {
        private CharacterBuilder builder;
        
        public EngineTest()
        {
            builder = new CharacterBuilder();
        }
        public void Dispose()
        {
            builder = null;
        }

        private Boolean someoneIsAlive(List<Character> chars)
        {
            Boolean atLeastOneStillAlive = false;
            foreach (Character c in chars)
            {
                if (c.HealthPoints > 0)
                {
                    atLeastOneStillAlive = true;
                }
            }
            return atLeastOneStillAlive;
        }

        [Fact]
        public void Test_RunEmpty()
        {
            // Here, a rather worrying misfeature of C# is obvious.
            // In order to access the classes in a test project, they need to be public. So you'd need to nest
            // solutions and/or projects in order to a) stick to information hiding principle and b) have
            // testable classes. 😠
            List<Character> characters = new List<Character>();
            List<Character> enemies = new List<Character>();
            SimulationResult result = Engine.Simulate(
                characters,
                enemies,
                true,
                CombatBehavior.Default(),
                CombatBehavior.Random()
            );
            Assert.Equal(0, result.Rounds);
            Assert.Empty(result.EventLog);
        }

        [Fact]
        public void Test_RunWithSimpleCharacters()
        {
            List<Character> characters = new List<Character>();
            characters.Add(
                builder.WithBasicStats("helga", 100, 7).Build()
            );

            List<Character> enemies = new List<Character>();
            enemies.Add(
                builder.WithBasicStats("hugo", 50, 20).Build()
            );

            SimulationResult result = Engine.Simulate(
                characters,
                enemies,
                true,
                CombatBehavior.Default(),
                CombatBehavior.Random()
            );

            Assert.False(result.PartyVictorious);

            
            Assert.True(someoneIsAlive(result.Enemies));
            Assert.False(someoneIsAlive(result.Party));
        }

        [Fact]
        public void Test_VeryLongRun()
        {
            List<Character> characters = new List<Character>();
            characters.Add(
                builder.WithBasicStats("helga", (long) 1.0E6, 1).Build()
            );

            List<Character> enemies = new List<Character>();
            enemies.Add(
                builder.WithBasicStats("hugo", (long) 5.0E5, 4).Build()
            );

            SimulationResult result = Engine.Simulate(
                characters,
                enemies,
                true,
                CombatBehavior.Default(),
                CombatBehavior.Random()
            );

            Assert.False(result.PartyVictorious);


            Assert.True(someoneIsAlive(result.Enemies));
            Assert.False(someoneIsAlive(result.Party));
        }
    }
}
