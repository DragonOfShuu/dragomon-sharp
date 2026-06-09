using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public static class Dendro
{
    private static readonly List<DragonProperties> dendroDragons = [];

    // Pidgey - common nature dragon from grassland
    public static readonly DragonProperties Pidgey = new DragonProperties()
        .Name("Pidgey")
        .Description("A small winged nature dragon common in open fields and grasslands.")
        .Attacks([
            new AttackPattern()
                .WithName("Gust")
                .WithDescription("A gust of wind")
                .WithDamage(10)
                .WithChance(50),
            new AttackPattern()
                .WithName("Leaf Tornado")
                .WithDescription("A tornado of leaves")
                .WithDamage(14)
                .WithInfliction(new ElementalInfliction(ElementType.Dendro, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Peck")
                .WithDescription("A quick peck attack")
                .WithDamage(12)
                .WithChance(30),
        ])
        .Chance(32)
        .AddTo(dendroDragons);

    // Rattata - common nature dragon from grassland/swamp
    public static readonly DragonProperties Rattata = new DragonProperties()
        .Name("Rattata")
        .Description("A swift nature dragon known for its quick movements and powerful bite.")
        .Attacks([
            new AttackPattern()
                .WithName("Quick Attack")
                .WithDescription("A swift strike")
                .WithDamage(12)
                .WithChance(50),
            new AttackPattern()
                .WithName("Vine Whip")
                .WithDescription("Whips with vines")
                .WithDamage(15)
                .WithInfliction(new ElementalInfliction(ElementType.Dendro, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Super Fang")
                .WithDescription("A powerful bite")
                .WithDamage(13)
                .WithChance(35),
        ])
        .Chance(30)
        .AddTo(dendroDragons);

    // Doduo - uncommon nature dragon from forest
    public static readonly DragonProperties Doduo = new DragonProperties()
        .Name("Doduo")
        .Description("A two-headed nature dragon with coordinated attacks and natural speed.")
        .Attacks([
            new AttackPattern()
                .WithName("Peck")
                .WithDescription("A coordinated peck")
                .WithDamage(14)
                .WithChance(45),
            new AttackPattern()
                .WithName("Synthesis")
                .WithDescription("Absorbs solar energy")
                .WithDamage(17)
                .WithInfliction(new ElementalInfliction(ElementType.Dendro, 2, 1))
                .WithChance(38),
            new AttackPattern()
                .WithName("Fury Attack")
                .WithDescription("A flurry of attacks")
                .WithDamage(16)
                .WithChance(35),
        ])
        .Chance(25)
        .AddTo(dendroDragons);

    // Eevee - uncommon nature dragon from grassland (base form)
    public static readonly DragonProperties Eevee = new DragonProperties()
        .Name("Eevee")
        .Description("A versatile nature dragon with potential to evolve into different forms.")
        .Attacks([
            new AttackPattern()
                .WithName("Tackle")
                .WithDescription("A basic tackle")
                .WithDamage(13)
                .WithChance(45),
            new AttackPattern()
                .WithName("Chlorophyll Burst")
                .WithDescription("A burst of nature")
                .WithDamage(16)
                .WithInfliction(new ElementalInfliction(ElementType.Dendro, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Swift")
                .WithDescription("Swift moving attack")
                .WithDamage(14)
                .WithChance(35),
        ])
        .Chance(27)
        .AddTo(dendroDragons);

    // Tauros - rare nature dragon from mountains
    public static readonly DragonProperties Tauros = new DragonProperties()
        .Name("Tauros")
        .Description("A powerful bull-like nature dragon known for aggressive charges.")
        .Attacks([
            new AttackPattern()
                .WithName("Horn Attack")
                .WithDescription("Attacks with horns")
                .WithDamage(19)
                .WithChance(40),
            new AttackPattern()
                .WithName("Seed Bomb")
                .WithDescription("Seeds explode on impact")
                .WithDamage(22)
                .WithInfliction(new ElementalInfliction(ElementType.Dendro, 3, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Double Edge")
                .WithDescription("A double-edged attack")
                .WithDamage(24)
                .WithChance(20),
        ])
        .Chance(12)
        .AddTo(dendroDragons);

    // Bidoof - rare nature dragon from mistlands (base-like evolution)
    public static readonly DragonProperties Bidoof = new DragonProperties()
        .Name("Bidoof")
        .Description("A hardy nature dragon of ancient lineage. Found in mystical waters.")
        .Attacks([
            new AttackPattern()
                .WithName("Bite")
                .WithDescription("A powerful bite")
                .WithDamage(16)
                .WithChance(50),
            new AttackPattern()
                .WithName("Ingrain")
                .WithDescription("Roots the dragon in place")
                .WithDamage(20)
                .WithInfliction(new ElementalInfliction(ElementType.Dendro, 3, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Tail Whip")
                .WithDescription("A tail whip attack")
                .WithDamage(18)
                .WithChance(30),
        ])
        .Chance(9)
        .AddTo(dendroDragons);

    // Leafeon - rare nature dragon from mistlands (evolved nature type)
    public static readonly DragonProperties Leafeon = new DragonProperties()
        .Name("Leafeon")
        .Description(
            "An evolved nature dragon surrounded by foliage. A mystical sight in the highlands."
        )
        .Attacks([
            new AttackPattern()
                .WithName("Leaf Blade")
                .WithDescription("Attacks with leaf blade")
                .WithDamage(26)
                .WithChance(35),
            new AttackPattern()
                .WithName("Solar Beam")
                .WithDescription("A beam of solar energy")
                .WithDamage(30)
                .WithInfliction(new ElementalInfliction(ElementType.Dendro, 4, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Magical Leaf")
                .WithDescription("Magical leaves attack")
                .WithDamage(28)
                .WithChance(25),
            new AttackPattern()
                .WithName("Forest's Blessing")
                .WithDescription("Blesses with nature's power")
                .WithDamage(33)
                .WithInfliction(new ElementalInfliction(ElementType.Swirl, 4, 2))
                .WithChance(20),
        ])
        .Chance(8)
        .AddTo(dendroDragons);

    /// <summary>
    /// Creates a Dendro-type dragon.
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
            ElementType.Dendro,
            properties,
            selectedAttacks
        );
    }

    private static DragonProperties SelectRandomDragon()
    {
        int totalWeight = dendroDragons.Sum(dp => dp.SelectionChance);
        int randomValue = Statics.GenRNum(0, totalWeight);

        int currentWeight = 0;
        foreach (var dragon in dendroDragons)
        {
            currentWeight += dragon.SelectionChance;
            if (randomValue < currentWeight)
            {
                return dragon;
            }
        }

        return Pidgey;
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
