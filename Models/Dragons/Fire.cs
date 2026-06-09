using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public static class Pyro
{
    private static readonly List<DragonProperties> pyroDragons = [];

    // Charmander - weak fire dragon from grassland/forest
    public static readonly DragonProperties Charmander = new DragonProperties()
        .Name("Charmander")
        .Description("A small reptilian dragon with a flame on its tail. Found in warm areas.")
        .Attacks([
            new AttackPattern()
                .WithName("Ember")
                .WithDescription("A burst of embers")
                .WithDamage(10)
                .WithChance(50),
            new AttackPattern()
                .WithName("Flame Burst")
                .WithDescription("A sudden burst of flames")
                .WithDamage(15)
                .WithInfliction(new ElementalInfliction(ElementType.Pyro, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Scratch")
                .WithDescription("A fierce scratch")
                .WithDamage(12)
                .WithChance(30),
        ])
        .Chance(30)
        .AddTo(pyroDragons);

    // Vulpix - common fire dragon from grassland
    public static readonly DragonProperties Vulpix = new DragonProperties()
        .Name("Vulpix")
        .Description("A fox-like fire dragon with multiple tails. Commonly found in warm regions.")
        .Attacks([
            new AttackPattern()
                .WithName("Ember")
                .WithDescription("A burst of embers")
                .WithDamage(12)
                .WithChance(50),
            new AttackPattern()
                .WithName("Fire Spin")
                .WithDescription("A spiraling inferno")
                .WithDamage(16)
                .WithInfliction(new ElementalInfliction(ElementType.Pyro, 2, 1))
                .WithChance(35),
            new AttackPattern()
                .WithName("Quick Attack")
                .WithDescription("A swift strike")
                .WithDamage(14)
                .WithChance(35),
        ])
        .Chance(35)
        .AddTo(pyroDragons);

    // Growlithe - uncommon fire dragon from forest
    public static readonly DragonProperties Growlithe = new DragonProperties()
        .Name("Growlithe")
        .Description("A fierce canine fire dragon. Known for its aggressive temperament.")
        .Attacks([
            new AttackPattern()
                .WithName("Bite")
                .WithDescription("A vicious bite")
                .WithDamage(18)
                .WithChance(45),
            new AttackPattern()
                .WithName("Flame Charge")
                .WithDescription("A charging attack wreathed in flames")
                .WithDamage(20)
                .WithInfliction(new ElementalInfliction(ElementType.Pyro, 3, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Wild Charge")
                .WithDescription("A wild, desperate attack")
                .WithDamage(22)
                .WithChance(25),
        ])
        .Chance(25)
        .AddTo(pyroDragons);

    // Ponyta - rare fire dragon from mountains
    public static readonly DragonProperties Ponyta = new DragonProperties()
        .Name("Ponyta")
        .Description("An equine fire dragon wreathed in flames. Found in mountainous terrain.")
        .Attacks([
            new AttackPattern()
                .WithName("Fury Attack")
                .WithDescription("A flurry of strikes")
                .WithDamage(22)
                .WithChance(40),
            new AttackPattern()
                .WithName("Fire Blast")
                .WithDescription("An intense column of flames")
                .WithDamage(25)
                .WithInfliction(new ElementalInfliction(ElementType.Pyro, 3, 2))
                .WithChance(38),
            new AttackPattern()
                .WithName("Inferno")
                .WithDescription("The ultimate fire attack")
                .WithDamage(28)
                .WithChance(22),
        ])
        .Chance(15)
        .AddTo(pyroDragons);

    // Flareon - rare fire dragon from mistlands (evolved fire type)
    public static readonly DragonProperties Flareon = new DragonProperties()
        .Name("Flareon")
        .Description("An evolved fire dragon of immense power. A rare sight in the mystlands.")
        .Attacks([
            new AttackPattern()
                .WithName("Fire Punch")
                .WithDescription("A punch engulfed in flames")
                .WithDamage(28)
                .WithChance(35),
            new AttackPattern()
                .WithName("Overheat")
                .WithDescription("Releases all internal fire at once")
                .WithDamage(32)
                .WithInfliction(new ElementalInfliction(ElementType.Pyro, 4, 3))
                .WithChance(40),
            new AttackPattern()
                .WithName("Flamethrower")
                .WithDescription("A sustained stream of fire")
                .WithDamage(30)
                .WithChance(25),
            new AttackPattern()
                .WithName("Burning Rage")
                .WithDescription("Enraged flames of destruction")
                .WithDamage(35)
                .WithInfliction(new ElementalInfliction(ElementType.Burning, 5, 2))
                .WithChance(20),
        ])
        .Chance(10)
        .AddTo(pyroDragons);

    /// <summary>
    /// Creates a Pyro-type dragon.
    /// </summary>
    /// <param name="level">The level of the dragon.</param>
    /// <param name="health">Health multiplier from 0 to 1. Defaults to 1 (full health).</param>
    /// <param name="maxHealth">Maximum health value. Defaults to 100.</param>
    /// <param name="properties">Specific dragon properties to use. If null, a random Pyro dragon is selected.</param>
    /// <returns>A new Pyro dragon instance.</returns>
    public static Dragon CreateDragon(
        int level,
        float health = 1f,
        float maxHealth = 100f,
        DragonProperties? properties = null
    )
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
