using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public static class Hydro
{
    private static readonly List<DragonProperties> hydroDragons = [];

    // Squirtle - weak water dragon from water regions
    public static readonly DragonProperties Squirtle = new DragonProperties()
        .Name("Squirtle")
        .Description("A small aquatic dragon with a protective shell. Found in water regions.")
        .Attacks([
            new AttackPattern()
                .WithName("Water Gun")
                .WithDescription("A spray of water")
                .WithDamage(10)
                .WithChance(50),
            new AttackPattern()
                .WithName("Bubble Beam")
                .WithDescription("Bubbles rise up in a beam")
                .WithDamage(15)
                .WithInfliction(new ElementalInfliction(ElementType.Hydro, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Withdraw")
                .WithDescription("Retreats into shell for protection")
                .WithDamage(5)
                .WithChance(25),
        ])
        .Chance(30)
        .AddTo(hydroDragons);

    // Psyduck - common water dragon from swamp/water
    public static readonly DragonProperties Psyduck = new DragonProperties()
        .Name("Psyduck")
        .Description("A confused and wandering water dragon often seen near water.")
        .Attacks([
            new AttackPattern()
                .WithName("Water Pulse")
                .WithDescription("Pulses of water strike foe")
                .WithDamage(12)
                .WithChance(50),
            new AttackPattern()
                .WithName("Hydro Pump")
                .WithDescription("A torrent of water")
                .WithDamage(16)
                .WithInfliction(new ElementalInfliction(ElementType.Hydro, 2, 1))
                .WithChance(35),
            new AttackPattern()
                .WithName("Confusion")
                .WithDescription("Psychic confusion attack")
                .WithDamage(14)
                .WithChance(35),
        ])
        .Chance(35)
        .AddTo(hydroDragons);

    // Slowpoke - uncommon water dragon from swamp
    public static readonly DragonProperties Slowpoke = new DragonProperties()
        .Name("Slowpoke")
        .Description("A slow-moving water dragon with limited intelligence but great power.")
        .Attacks([
            new AttackPattern()
                .WithName("Water Sport")
                .WithDescription("Wets the area to reduce fire")
                .WithDamage(13)
                .WithChance(45),
            new AttackPattern()
                .WithName("Aqua Ring")
                .WithDescription("Water swirls around foe")
                .WithDamage(18)
                .WithInfliction(new ElementalInfliction(ElementType.Hydro, 3, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Headbutt")
                .WithDescription("A powerful headbutt")
                .WithDamage(16)
                .WithChance(25),
        ])
        .Chance(25)
        .AddTo(hydroDragons);

    // Horsea - rare water dragon from deep water
    public static readonly DragonProperties Horsea = new DragonProperties()
        .Name("Horsea")
        .Description("An elegant seahorse-like water dragon found in deep waters.")
        .Attacks([
            new AttackPattern()
                .WithName("Dragon Rage")
                .WithDescription("The ancient power of dragons")
                .WithDamage(20)
                .WithChance(40),
            new AttackPattern()
                .WithName("Hydro Cannon")
                .WithDescription("A powerful cannon of water")
                .WithDamage(24)
                .WithInfliction(new ElementalInfliction(ElementType.Hydro, 3, 2))
                .WithChance(38),
            new AttackPattern()
                .WithName("Brine")
                .WithDescription("A concentrated salt water attack")
                .WithDamage(22)
                .WithChance(22),
        ])
        .Chance(15)
        .AddTo(hydroDragons);

    // Vaporeon - rare water dragon from mystlands (evolved water type)
    public static readonly DragonProperties Vaporeon = new DragonProperties()
        .Name("Vaporeon")
        .Description(
            "An evolved water dragon that blends seamlessly with its environment. Found in mystical waters."
        )
        .Attacks([
            new AttackPattern()
                .WithName("Water Shuriken")
                .WithDescription("Water shuriken attacks")
                .WithDamage(22)
                .WithChance(35),
            new AttackPattern()
                .WithName("Aqua Jet")
                .WithDescription("A jet of water propels the dragon")
                .WithDamage(28)
                .WithInfliction(new ElementalInfliction(ElementType.Hydro, 4, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Waterfall")
                .WithDescription("Water falls down upon the foe")
                .WithDamage(26)
                .WithChance(25),
            new AttackPattern()
                .WithName("Tidal Wave")
                .WithDescription("A massive wave of destruction")
                .WithDamage(31)
                .WithInfliction(new ElementalInfliction(ElementType.Vaporise, 4, 2))
                .WithChance(20),
        ])
        .Chance(10)
        .AddTo(hydroDragons);

    /// <summary>
    /// Creates a Hydro-type dragon.
    /// </summary>
    public static Dragon CreateDragon(
        int level,
        float health = 1f,
        float maxHealth = 100f,
        DragonProperties? properties = null
    )
    {
        health = Math.Clamp(health, 0f, 1f);
        float actualHealth = health * maxHealth;

        properties ??= SelectRandomDragon();

        List<AttackPattern> selectedAttacks = SelectRandomAttacks(properties);

        return new Dragon(
            properties.SpeciesName,
            level,
            actualHealth,
            maxHealth,
            ElementType.Hydro,
            properties,
            selectedAttacks
        );
    }

    private static DragonProperties SelectRandomDragon()
    {
        int totalWeight = hydroDragons.Sum(dp => dp.SelectionChance);
        int randomValue = Statics.GenRNum(0, totalWeight);

        int currentWeight = 0;
        foreach (var dragon in hydroDragons)
        {
            currentWeight += dragon.SelectionChance;
            if (randomValue < currentWeight)
            {
                return dragon;
            }
        }

        return Squirtle;
    }

    private static List<AttackPattern> SelectRandomAttacks(DragonProperties properties)
    {
        var availableAttacks = properties.PossibleAttacks.ToList();
        int attackCount = Statics.GenRNum(1, 4);
        List<AttackPattern> selectedAttacks = [];

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
