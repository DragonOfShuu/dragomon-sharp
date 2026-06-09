using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public static class Electro
{
    private static readonly List<DragonProperties> electroDragons = [];

    // Pikachu - common electric dragon from grassland
    public static readonly DragonProperties Pikachu = new DragonProperties()
        .Name("Pikachu")
        .Description("A small, mouse-like electric dragon famous for its speed and charge attacks.")
        .Attacks([
            new AttackPattern()
                .WithName("Thunderbolt")
                .WithDescription("A bolt of lightning")
                .WithDamage(11)
                .WithChance(50),
            new AttackPattern()
                .WithName("Thunder Wave")
                .WithDescription("Paralyzing waves of electricity")
                .WithDamage(16)
                .WithInfliction(new ElementalInfliction(ElementType.Electro, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Quick Attack")
                .WithDescription("A swift strike")
                .WithDamage(13)
                .WithChance(30),
        ])
        .Chance(35)
        .AddTo(electroDragons);

    // Magnemite - uncommon electric dragon from mountains
    public static readonly DragonProperties Magnemite = new DragonProperties()
        .Name("Magnemite")
        .Description("A mechanical electric dragon that emits powerful electromagnetic waves.")
        .Attacks([
            new AttackPattern()
                .WithName("Tackle")
                .WithDescription("A heavy collision")
                .WithDamage(14)
                .WithChance(45),
            new AttackPattern()
                .WithName("Electromagnetic Pulse")
                .WithDescription("A pulse of magnetic energy")
                .WithDamage(18)
                .WithInfliction(new ElementalInfliction(ElementType.Electro, 2, 1))
                .WithChance(38),
            new AttackPattern()
                .WithName("Thunder Lock")
                .WithDescription("Binds opponent with electricity")
                .WithDamage(17)
                .WithInfliction(new ElementalInfliction(ElementType.Electrocharge, 3, 1))
                .WithChance(25),
        ])
        .Chance(25)
        .AddTo(electroDragons);

    // Voltorb - common electric dragon from grassland/swamp
    public static readonly DragonProperties Voltorb = new DragonProperties()
        .Name("Voltorb")
        .Description("A spherical electric dragon constantly charged with volatile energy.")
        .Attacks([
            new AttackPattern()
                .WithName("Spark")
                .WithDescription("A spark of electricity")
                .WithDamage(13)
                .WithChance(50),
            new AttackPattern()
                .WithName("Charge Beam")
                .WithDescription("A beam of charging electricity")
                .WithDamage(17)
                .WithInfliction(new ElementalInfliction(ElementType.Electro, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Sonic Boom")
                .WithDescription("A booming sound wave")
                .WithDamage(15)
                .WithChance(35),
        ])
        .Chance(30)
        .AddTo(electroDragons);

    // Electabuzz - rare electric dragon from mountains
    public static readonly DragonProperties Electabuzz = new DragonProperties()
        .Name("Electabuzz")
        .Description(
            "A powerful humanoid electric dragon with crackling energy surrounding its body."
        )
        .Attacks([
            new AttackPattern()
                .WithName("Thunder Punch")
                .WithDescription("A punch charged with electricity")
                .WithDamage(20)
                .WithChance(40),
            new AttackPattern()
                .WithName("Thunderbolt")
                .WithDescription("A powerful bolt of lightning")
                .WithDamage(23)
                .WithInfliction(new ElementalInfliction(ElementType.Electro, 3, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Thunder Crash")
                .WithDescription("Electricity crashes down")
                .WithDamage(25)
                .WithInfliction(new ElementalInfliction(ElementType.Overload, 3, 1))
                .WithChance(20),
        ])
        .Chance(15)
        .AddTo(electroDragons);

    // Jolteon - rare electric dragon from mistlands (evolved electric type)
    public static readonly DragonProperties Jolteon = new DragonProperties()
        .Name("Jolteon")
        .Description(
            "An evolved electric dragon of tremendous speed and power. Found in mystical areas."
        )
        .Attacks([
            new AttackPattern()
                .WithName("Thunder Strike")
                .WithDescription("A strike of pure electricity")
                .WithDamage(26)
                .WithChance(35),
            new AttackPattern()
                .WithName("Discharge")
                .WithDescription("Electricity spreads in all directions")
                .WithDamage(30)
                .WithInfliction(new ElementalInfliction(ElementType.Electro, 4, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Lightning Bolt")
                .WithDescription("A focused bolt of lightning")
                .WithDamage(28)
                .WithChance(25),
            new AttackPattern()
                .WithName("Thunder Nova")
                .WithDescription("A nova of electric energy")
                .WithDamage(33)
                .WithInfliction(new ElementalInfliction(ElementType.Electrocharge, 4, 2))
                .WithChance(20),
        ])
        .Chance(10)
        .AddTo(electroDragons);

    /// <summary>
    /// Creates an Electro-type dragon.
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
            ElementType.Electro,
            properties,
            selectedAttacks
        );
    }

    private static DragonProperties SelectRandomDragon()
    {
        int totalWeight = electroDragons.Sum(dp => dp.SelectionChance);
        int randomValue = Statics.GenRNum(0, totalWeight);

        int currentWeight = 0;
        foreach (var dragon in electroDragons)
        {
            currentWeight += dragon.SelectionChance;
            if (randomValue < currentWeight)
            {
                return dragon;
            }
        }

        return Pikachu;
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
