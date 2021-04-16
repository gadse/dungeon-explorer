using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class CharacterBuilder
    {
        private Character Constructed
        {
            get; set;
        }
    
        public CharacterBuilder WithBasicStats(string name, long healthPoints, long averageDamagePerRound)
        {
            Constructed = new Character(name, healthPoints, averageDamagePerRound, Constants.NO_RESOURCES_NEEDED);
            return this;
        }

        public CharacterBuilder WithResources(long resources)
        {
            Constructed = new Character(
                Constructed.Name,
                Constructed.HealthPoints,
                Constructed.AverageDamagePerRound,
                resources,
                Constructed.SpecialAttacks,
                Constructed.HealActions
            );
            return this;
        }
        public CharacterBuilder WithSpecialAttack(long damage, long resourceCost)
        {
            var extendedAttacks = Constructed.SpecialAttacks;
            extendedAttacks.Add((damage, resourceCost));
            Constructed = new Character(
                Constructed.Name,
                Constructed.HealthPoints,
                Constructed.AverageDamagePerRound,
                Constructed.Resources,
                extendedAttacks,
                Constructed.HealActions
            );
            return this;
        }
        public CharacterBuilder WithHealAction(long regeneration, long resourceCost)
        {
            var extendedHealActions = Constructed.HealActions;
            extendedHealActions.Add((regeneration, resourceCost));
            Constructed = new Character(
                Constructed.Name,
                Constructed.HealthPoints,
                Constructed.AverageDamagePerRound,
                Constructed.Resources,
                extendedHealActions,
                Constructed.HealActions
            );
            return this;
        }

        public Boolean CanBuild()
        {
            if (String.IsNullOrEmpty(Constructed.Name))
            {
                return false;
            }
            if (Constructed.HealthPoints < 0)
            {
                return false;
            }
            if (Constructed.AverageDamagePerRound < 0)
            {
                return false;
            }
            if (Constructed.Resources != Constants.NO_RESOURCES_NEEDED && Constructed.Resources < 0)
            {
                return false;
            }
            foreach ((long damage, long resourceCost) in Constructed.SpecialAttacks)
            {
                if (damage < 0) // We want to allow negative resource cost for resource regeneration.
                {
                    return false;
                }
            }
            foreach ((long regeneration, long resourceCost) in Constructed.HealActions)
            {
                if (regeneration < 0) // We want to allow negative resource cost for resource regeneration.
                {
                    return false;
                }
            }
            return true;
        }

        public Character SafeBuild()
        {
            if (CanBuild()) {
                return Constructed;
            }
            else
            {
                throw new ApplicationException(
                    "Character is not safe to construct."
                );
            }
        }

        public Character Build()
        {
            return Constructed;
        }
    }
}