using System;
using Xunit;

using DungeonExplorer;

namespace DungeonExplorer.Tests
{
    public class CharacterCreationTest
    {
        [Fact]
        public void Test_BuildBasicCharacter()
        {
            string name = "Helga";
            long healthPoints = 100;
            long damagePerRound = 10;
            CharacterBuilder builder = new CharacterBuilder();

            Character helga = builder.WithBasicStats(name, healthPoints, damagePerRound).Build();

            Assert.Equal(name, helga.Name);
            Assert.Equal(healthPoints, helga.HealthPoints);
            Assert.Equal(damagePerRound, helga.AverageDamagePerRound);
            Assert.Equal(Constants.NO_RESOURCES_NEEDED, helga.Resources);
            Assert.Empty(helga.SpecialAttacks);
            Assert.Empty(helga.HealActions);
        }

        [Fact]
        public void Test_BuildCharacterWithResources()
        {
            string name = "Helga";
            long healthPoints = 100;
            long damagePerRound = 10;
            long resources = 3;
            CharacterBuilder builder = new CharacterBuilder();

            Character helga = builder.WithBasicStats(name, healthPoints, damagePerRound).WithResources(resources).Build();

            Assert.Equal(name, helga.Name);
            Assert.Equal(healthPoints, helga.HealthPoints);
            Assert.Equal(damagePerRound, helga.AverageDamagePerRound);
            Assert.Equal(resources, helga.Resources);
            Assert.Empty(helga.SpecialAttacks);
            Assert.Empty(helga.HealActions);
        }
    }
}
