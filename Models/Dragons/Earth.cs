using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public static class Geo
{
    private static readonly List<DragonProperties> geoDragons = [];

    // Sandshrew - common earth dragon from grassland/swamp
    public static readonly DragonProperties Sandshrew = new DragonProperties()
        .Name("Sandshrew")
        .Description("A small burrowing earth dragon adept at digging through sand and soil.")
        .Attacks([
            new AttackPattern()
                .WithName("Sand Attack")
                .WithDescription("Attacks with sand")
                .WithDamage(11)
                .WithChance(50),
            new AttackPattern()
                .WithName("Earthquake")
                .WithDescription("The earth shakes")
                .WithDamage(15)
                .WithInfliction(new ElementalInfliction(ElementType.Geo, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Fury Swipes")
                .WithDescription("Rapid sand swipes")
                .WithDamage(13)
                .WithChance(30),
        ])
        .Chance(30)
        .AddTo(geoDragons);

    // Diglett - common earth dragon from grassland/swamp
    public static readonly DragonProperties Diglett = new DragonProperties()
        .Name("Diglett")
        .Description("A peculiar earth dragon that burrows through soil, leaving tunnels behind.")
        .Attacks([
            new AttackPattern()
                .WithName("Scratch")
                .WithDescription("A scratching attack")
                .WithDamage(10)
                .WithChance(50),
            new AttackPattern()
                .WithName("Mud Slap")
                .WithDescription("Mud slaps the foe")
                .WithDamage(14)
                .WithInfliction(new ElementalInfliction(ElementType.Geo, 2, 1))
                .WithChance(40),
            new AttackPattern().WithName("Dig").WithDescription("Digs underground").WithDamage(12).WithChance(35),
        ])
        .Chance(32)
        .AddTo(geoDragons);

    // Cubone - uncommon earth dragon from mountains
    public static readonly DragonProperties Cubone = new DragonProperties()
        .Name("Cubone")
        .Description("A small earth dragon that wears a skull as armor, found in rocky terrain.")
        .Attacks([
            new AttackPattern()
                .WithName("Bone Club")
                .WithDescription("A bone club attack")
                .WithDamage(13)
                .WithChance(45),
            new AttackPattern()
                .WithName("Rock Throw")
                .WithDescription("Throws rocks")
                .WithDamage(17)
                .WithInfliction(new ElementalInfliction(ElementType.Geo, 2, 1))
                .WithChance(38),
            new AttackPattern()
                .WithName("Focus Blast")
                .WithDescription("A focused blast of power")
                .WithDamage(15)
                .WithChance(35),
        ])
        .Chance(28)
        .AddTo(geoDragons);

    // Phanpy - rare earth dragon from mountains with dense soil
    public static readonly DragonProperties Phanpy = new DragonProperties()
        .Name("Phanpy")
        .Description("A sturdy earth dragon with a powerful frame suited for rocky environments.")
        .Attacks([
            new AttackPattern().WithName("Tackle").WithDescription("A heavy tackle").WithDamage(18).WithChance(40),
            new AttackPattern()
                .WithName("Stone Edge")
                .WithDescription("Sharp stone edges")
                .WithDamage(21)
                .WithInfliction(new ElementalInfliction(ElementType.Geo, 3, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Boulder Crush")
                .WithDescription("Crushes with boulders")
                .WithDamage(23)
                .WithInfliction(new ElementalInfliction(ElementType.Crystallise, 3, 1))
                .WithChance(20),
        ])
        .Chance(15)
        .AddTo(geoDragons);

    // Gligar - rare earth dragon from mistlands (evolved earth type)
    public static readonly DragonProperties Gligar = new DragonProperties()
        .Name("Gligar")
        .Description(
            "An evolved earth dragon capable of flight despite its massive bulk. A rare find."
        )
        .Attacks([
            new AttackPattern()
                .WithName("Wing Attack")
                .WithDescription("A powerful wing strike")
                .WithDamage(24)
                .WithChance(35),
            new AttackPattern()
                .WithName("Earthquake Slam")
                .WithDescription("Slams earth with quake")
                .WithDamage(28)
                .WithInfliction(new ElementalInfliction(ElementType.Geo, 4, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Avalanche")
                .WithDescription("An avalanche of rock")
                .WithDamage(26)
                .WithChance(25),
            new AttackPattern()
                .WithName("Stone Cyclone")
                .WithDescription("A cyclone of stone")
                .WithDamage(31)
                .WithInfliction(new ElementalInfliction(ElementType.Crystallise, 4, 2))
                .WithChance(20),
        ])
        .Chance(10)
        .AddTo(geoDragons);

    /// <summary>
    /// Creates a Geo-type dragon.
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
            ElementType.Geo,
            properties,
            selectedAttacks
        );
    }

    private static DragonProperties SelectRandomDragon()
    {
        int totalWeight = geoDragons.Sum(dp => dp.SelectionChance);
        int randomValue = Statics.GenRNum(0, totalWeight);

        int currentWeight = 0;
        foreach (DragonProperties dragon in geoDragons)
        {
            currentWeight += dragon.SelectionChance;
            if (randomValue < currentWeight)
            {
                return dragon;
            }
        }

        return Sandshrew;
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
