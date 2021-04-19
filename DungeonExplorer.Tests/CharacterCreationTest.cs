using System;
using Xunit;

namespace DungeonExplorer.Tests
{
    public class CharacterCreationTest
    {
        private CharacterBuilder builder;

        public CharacterCreationTest()
        {
            builder = new CharacterBuilder();
        }

        public void Dispose()
        {
            builder = null;
        }

        [Fact]
        public void Test_BuildBasicCharacter()
        {
            string name = "Helga";
            long healthPoints = 100;
            long damagePerRound = 10;

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

            Character helga = builder.WithBasicStats(name, healthPoints, damagePerRound).WithResources(resources).Build();

            Assert.Equal(name, helga.Name);
            Assert.Equal(healthPoints, helga.HealthPoints);
            Assert.Equal(damagePerRound, helga.AverageDamagePerRound);
            Assert.Equal(resources, helga.Resources);
            Assert.Empty(helga.SpecialAttacks);
            Assert.Empty(helga.HealActions);
        }

        [Fact]
        public void Test_BuildCharacterWithSpecialAttacks()
        {
            string name = "Helga";
            long healthPoints = 11;
            long damagePerRound = 222;
            (long damage, long resourceCost) = (10, 7);

            Character helga = builder.WithBasicStats(name, healthPoints, damagePerRound).WithSpecialAttack(damage, resourceCost).Build();

            Assert.Equal(name, helga.Name);
            Assert.Equal(healthPoints, helga.HealthPoints);
            Assert.Equal(damagePerRound, helga.AverageDamagePerRound);
            Assert.Equal(Constants.NO_RESOURCES_NEEDED, helga.Resources);
            Assert.Equal(damage, helga.SpecialAttacks[0].damage);
            Assert.Equal(resourceCost, helga.SpecialAttacks[0].resourceCost);
            Assert.Empty(helga.HealActions);
        }

        [Fact]
        public void Test_BuildCharacterWithHealAction()
        {
            string name = "Helga";
            long healthPoints = 11;
            long damagePerRound = 222;
            (long healing, long resourceCost) = (25, 7);

            Character helga = builder.WithBasicStats(name, healthPoints, damagePerRound).WithHealAction(healing, resourceCost).Build();

            Assert.Equal(name, helga.Name);
            Assert.Equal(healthPoints, helga.HealthPoints);
            Assert.Equal(damagePerRound, helga.AverageDamagePerRound);
            Assert.Equal(Constants.NO_RESOURCES_NEEDED, helga.Resources);
            Assert.Equal(healing, helga.HealActions[0].healing);
            Assert.Empty(helga.SpecialAttacks);
        }

        [Fact]
        public void Test_BuildCharacterWithEverything()
        {
            string name = "Helga";
            long healthPoints = 11;
            long damagePerRound = 222;
            long resources = 333;
            (long damage, long atkResourceCost) = (10, 7);
            (long healing, long healResourceCost) = (25, 7);

            Character helga = builder
                .WithBasicStats(name, healthPoints, damagePerRound)
                .WithHealAction(healing, healResourceCost)
                .WithSpecialAttack(damage, atkResourceCost)
                .WithResources(resources)
                .Build();

            Assert.Equal(name, helga.Name);
            Assert.Equal(healthPoints, helga.HealthPoints);
            Assert.Equal(damagePerRound, helga.AverageDamagePerRound);
            Assert.Equal(resources, helga.Resources);
            Assert.Equal(healing, helga.HealActions[0].healing);
            Assert.Equal(damage, helga.SpecialAttacks[0].damage);
        }
    }
}
