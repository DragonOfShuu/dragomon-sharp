using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public static class Anemo
{
    private static readonly List<DragonProperties> anemoDragons = [];

    // Gastly - common wind dragon from swamp/mistlands
    public static readonly DragonProperties Gastly = new DragonProperties()
        .Name("Gastly")
        .Description(
            "A spectral wind dragon wreathed in ethereal vapor, drifting through misty areas."
        )
        .Attacks([
            new AttackPattern()
                .WithName("Shadow Ball")
                .WithDescription("A ball of shadow energy")
                .WithDamage(10)
                .WithChance(50),
            new AttackPattern()
                .WithName("Ominous Wind")
                .WithDescription("Wind carries ill intent")
                .WithDamage(14)
                .WithInfliction(new ElementalInfliction(ElementType.Anemo, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Night Shade")
                .WithDescription("Shadowy night attack")
                .WithDamage(12)
                .WithChance(30),
        ])
        .Chance(30)
        .AddTo(anemoDragons);

    // Misdreavus - uncommon wind dragon from mistlands
    public static readonly DragonProperties Misdreavus = new DragonProperties()
        .Name("Misdreavus")
        .Description("A wind dragon that feeds on emotions. Found in ethereal realms.")
        .Attacks([
            new AttackPattern()
                .WithName("Disarming Voice")
                .WithDescription("A disarming wind voice")
                .WithDamage(12)
                .WithChance(45),
            new AttackPattern()
                .WithName("Wind Blast")
                .WithDescription("A blast of wind")
                .WithDamage(16)
                .WithInfliction(new ElementalInfliction(ElementType.Anemo, 2, 1))
                .WithChance(38),
            new AttackPattern()
                .WithName("Screech")
                .WithDescription("A piercing screech")
                .WithDamage(14)
                .WithChance(35),
        ])
        .Chance(26)
        .AddTo(anemoDragons);

    // Duskull - uncommon wind dragon from swamp/mistlands
    public static readonly DragonProperties Duskull = new DragonProperties()
        .Name("Duskull")
        .Description("A wind dragon that glides through shadows. Elusive and mysterious.")
        .Attacks([
            new AttackPattern()
                .WithName("Shadow Sneak")
                .WithDescription("Sneaking shadow attack")
                .WithDamage(13)
                .WithChance(45),
            new AttackPattern()
                .WithName("Wind Storm")
                .WithDescription("A storm of wind")
                .WithDamage(17)
                .WithInfliction(new ElementalInfliction(ElementType.Anemo, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Pursuit")
                .WithDescription("Pursues the foe")
                .WithDamage(15)
                .WithChance(35),
        ])
        .Chance(28)
        .AddTo(anemoDragons);

    // Drifloon - rare wind dragon from mountains
    public static readonly DragonProperties Drifloon = new DragonProperties()
        .Name("Drifloon")
        .Description("A buoyant wind dragon that floats on air currents. Found at high altitudes.")
        .Attacks([
            new AttackPattern()
                .WithName("Gust")
                .WithDescription("A gust of wind")
                .WithDamage(17)
                .WithChance(40),
            new AttackPattern()
                .WithName("Whirlwind")
                .WithDescription("A spiraling whirlwind")
                .WithDamage(20)
                .WithInfliction(new ElementalInfliction(ElementType.Anemo, 3, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Minimize")
                .WithDescription("Makes the dragon tiny")
                .WithDamage(22)
                .WithChance(20),
        ])
        .Chance(16)
        .AddTo(anemoDragons);

    // Spiritomb - rare wind dragon from mistlands (advanced form)
    public static readonly DragonProperties Spiritomb = new DragonProperties()
        .Name("Spiritomb")
        .Description(
            "A collected wind dragon formed from many spirits. Rarely seen in mystical heights."
        )
        .Attacks([
            new AttackPattern()
                .WithName("Dark Pulse")
                .WithDescription("A pulse of dark wind")
                .WithDamage(21)
                .WithChance(40),
            new AttackPattern()
                .WithName("Spirit Cyclone")
                .WithDescription("A cyclone of spirits")
                .WithDamage(24)
                .WithInfliction(new ElementalInfliction(ElementType.Anemo, 3, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Phantom Assault")
                .WithDescription("Phantom wind assault")
                .WithDamage(26)
                .WithInfliction(new ElementalInfliction(ElementType.Swirl, 3, 1))
                .WithChance(20),
        ])
        .Chance(14)
        .AddTo(anemoDragons);

    // Tornadus - rare wind dragon from mistlands (evolved wind type)
    public static readonly DragonProperties Tornadus = new DragonProperties()
        .Name("Tornadus")
        .Description("An evolved wind dragon of immense power. Master of tempests and storms.")
        .Attacks([
            new AttackPattern()
                .WithName("Hurricane")
                .WithDescription("A devastating hurricane")
                .WithDamage(28)
                .WithChance(35),
            new AttackPattern()
                .WithName("Eternal Cyclone")
                .WithDescription("An endless cyclone")
                .WithDamage(32)
                .WithInfliction(new ElementalInfliction(ElementType.Anemo, 4, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Tornado Dive")
                .WithDescription("Dives into a tornado")
                .WithDamage(30)
                .WithChance(25),
            new AttackPattern()
                .WithName("Tempest Rising")
                .WithDescription("Rises with a tempest")
                .WithDamage(35)
                .WithInfliction(new ElementalInfliction(ElementType.Swirl, 4, 2))
                .WithChance(20),
        ])
        .Chance(10)
        .AddTo(anemoDragons);

    /// <summary>
    /// Creates an Anemo-type dragon.
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
            ElementType.Anemo,
            properties,
            selectedAttacks
        );
    }

    private static DragonProperties SelectRandomDragon()
    {
        int totalWeight = anemoDragons.Sum(dp => dp.SelectionChance);
        int randomValue = Statics.GenRNum(0, totalWeight);

        int currentWeight = 0;
        foreach (var dragon in anemoDragons)
        {
            currentWeight += dragon.SelectionChance;
            if (randomValue < currentWeight)
            {
                return dragon;
            }
        }

        return Gastly;
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
