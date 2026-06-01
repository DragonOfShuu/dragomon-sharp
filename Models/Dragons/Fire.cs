using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public static class Pyro
{
    // Charmander - weak fire dragon from grassland/forest
    public static readonly DragonProperties Charmander = new DragonProperties()
        .Name("Charmander")
        .Description("A small reptilian dragon with a flame on its tail. Found in warm areas.")
        .Attacks(
        [
            new AttackPattern().Name("Ember").Description("A burst of embers").Damage(10).Chance(50),
            new AttackPattern().Name("Flame Burst").Description("A sudden burst of flames").Damage(15).Infliction(new ElementalInfliction(ElementType.Pyro, 2, 1)).Chance(40),
            new AttackPattern().Name("Scratch").Description("A fierce scratch").Damage(12).Chance(30)
        ])
        .Chance(30);

    // Vulpix - common fire dragon from grassland
    public static readonly DragonProperties Vulpix = new DragonProperties()
        .Name("Vulpix")
        .Description("A fox-like fire dragon with multiple tails. Commonly found in warm regions.")
        .Attacks(
        [
            new AttackPattern().Name("Ember").Description("A burst of embers").Damage(12).Chance(50),
            new AttackPattern().Name("Fire Spin").Description("A spiraling inferno").Damage(16).Infliction(new ElementalInfliction(ElementType.Pyro, 2, 1)).Chance(35),
            new AttackPattern().Name("Quick Attack").Description("A swift strike").Damage(14).Chance(35)
        ])
        .Chance(35);

    // Growlithe - uncommon fire dragon from forest
    public static readonly DragonProperties Growlithe = new DragonProperties()
        .Name("Growlithe")
        .Description("A fierce canine fire dragon. Known for its aggressive temperament.")
        .Attacks(
        [
            new AttackPattern().Name("Bite").Description("A vicious bite").Damage(18).Chance(45),
            new AttackPattern().Name("Flame Charge").Description("A charging attack wreathed in flames").Damage(20).Infliction(new ElementalInfliction(ElementType.Pyro, 3, 2)).Chance(40),
            new AttackPattern().Name("Wild Charge").Description("A wild, desperate attack").Damage(22).Chance(25)
        ])
        .Chance(25);

    // Ponyta - rare fire dragon from mountains
    public static readonly DragonProperties Ponyta = new DragonProperties()
        .Name("Ponyta")
        .Description("An equine fire dragon wreathed in flames. Found in mountainous terrain.")
        .Attacks(
        [
            new AttackPattern().Name("Fury Attack").Description("A flurry of strikes").Damage(22).Chance(40),
            new AttackPattern().Name("Fire Blast").Description("An intense column of flames").Damage(25).Infliction(new ElementalInfliction(ElementType.Pyro, 3, 2)).Chance(38),
            new AttackPattern().Name("Inferno").Description("The ultimate fire attack").Damage(28).Chance(22)
        ])
        .Chance(15);

    // Flareon - rare fire dragon from mistlands (evolved fire type)
    public static readonly DragonProperties Flareon = new DragonProperties()
        .Name("Flareon")
        .Description("An evolved fire dragon of immense power. A rare sight in the mystlands.")
        .Attacks(
        [
            new AttackPattern().Name("Fire Punch").Description("A punch engulfed in flames").Damage(28).Chance(35),
            new AttackPattern().Name("Overheat").Description("Releases all internal fire at once").Damage(32).Infliction(new ElementalInfliction(ElementType.Pyro, 4, 3)).Chance(40),
            new AttackPattern().Name("Flamethrower").Description("A sustained stream of fire").Damage(30).Chance(25),
            new AttackPattern().Name("Burning Rage").Description("Enraged flames of destruction").Damage(35).Infliction(new ElementalInfliction(ElementType.Burning, 5, 2)).Chance(20)
        ])
        .Chance(10);

    /// <summary>
    /// Creates a Pyro-type dragon.
    /// </summary>
    /// <param name="level">The level of the dragon.</param>
    /// <param name="health">Health multiplier from 0 to 1. Defaults to 1 (full health).</param>
    /// <param name="maxHealth">Maximum health value. Defaults to 100.</param>
    /// <param name="properties">Specific dragon properties to use. If null, a random Pyro dragon is selected.</param>
    /// <returns>A new Pyro dragon instance.</returns>
    public static Dragon CreateDragon(int level, float health = 1f, float maxHealth = 100f, DragonProperties? properties = null)
    {
        // Normalize health between 0 and 1
        health = Math.Clamp(health, 0f, 1f);
        float actualHealth = health * maxHealth;

        // Select random properties if not provided
        properties ??= SelectRandomDragon();

        // Select 1-3 random attacks from the properties
        List<AttackPattern> selectedAttacks = SelectRandomAttacks(properties);

        return new Dragon(
            properties.SpeciesName,
            level,
            actualHealth,
            maxHealth,
            ElementType.Pyro,
            properties,
            selectedAttacks
        );
    }

    private static DragonProperties SelectRandomDragon()
    {
        var pyroDragons = new[] { Charmander, Vulpix, Growlithe, Ponyta, Flareon };

        // Build weighted selection based on SelectionChance values
        int totalWeight = pyroDragons.Sum(dp => dp.SelectionChance);
        int randomValue = Statics.GenRNum(0, totalWeight);

        int currentWeight = 0;
        foreach (var dragon in pyroDragons)
        {
            currentWeight += dragon.SelectionChance;
            if (randomValue < currentWeight)
            {
                return dragon;
            }
        }

        return Charmander;
    }

    private static List<AttackPattern> SelectRandomAttacks(DragonProperties properties)
    {
        var availableAttacks = properties.PossibleAttacks.ToList();
        int attackCount = Statics.GenRNum(1, 4); // 1-3 attacks
        List<AttackPattern> selectedAttacks = [];

        // Build weighted selection for attacks
        for (int i = 0; i < attackCount && availableAttacks.Count > 0; i++)
        {
            int totalWeight = availableAttacks.Sum(ap => ap.SelectionChance);
            int randomValue = Statics.GenRNum(0, totalWeight);

            int currentWeight = 0;
            for (int j = 0; j < availableAttacks.Count; j++)
            {
                currentWeight += availableAttacks[j].SelectionChance;
                if (randomValue < currentWeight)
                {
                    selectedAttacks.Add(availableAttacks[j]);
                    availableAttacks.RemoveAt(j);
                    break;
                }
            }
        }

        return selectedAttacks;
    }
}


