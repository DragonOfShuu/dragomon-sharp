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
        .Attacks(
        [
            new AttackPattern().Name("Gust").Description("A gust of wind").Damage(10).Chance(50),
            new AttackPattern().Name("Leaf Tornado").Description("A tornado of leaves").Damage(14).Infliction(new ElementalInfliction(ElementType.Dendro, 2, 1)).Chance(40),
            new AttackPattern().Name("Peck").Description("A quick peck attack").Damage(12).Chance(30)
        ])
        .Chance(32)
        .AddTo(dendroDragons);

    // Rattata - common nature dragon from grassland/swamp
    public static readonly DragonProperties Rattata = new DragonProperties()
        .Name("Rattata")
        .Description("A swift nature dragon known for its quick movements and powerful bite.")
        .Attacks(
        [
            new AttackPattern().Name("Quick Attack").Description("A swift strike").Damage(12).Chance(50),
            new AttackPattern().Name("Vine Whip").Description("Whips with vines").Damage(15).Infliction(new ElementalInfliction(ElementType.Dendro, 2, 1)).Chance(40),
            new AttackPattern().Name("Super Fang").Description("A powerful bite").Damage(13).Chance(35)
        ])
        .Chance(30)
        .AddTo(dendroDragons);

    // Doduo - uncommon nature dragon from forest
    public static readonly DragonProperties Doduo = new DragonProperties()
        .Name("Doduo")
        .Description("A two-headed nature dragon with coordinated attacks and natural speed.")
        .Attacks(
        [
            new AttackPattern().Name("Peck").Description("A coordinated peck").Damage(14).Chance(45),
            new AttackPattern().Name("Synthesis").Description("Absorbs solar energy").Damage(17).Infliction(new ElementalInfliction(ElementType.Dendro, 2, 1)).Chance(38),
            new AttackPattern().Name("Fury Attack").Description("A flurry of attacks").Damage(16).Chance(35)
        ])
        .Chance(25)
        .AddTo(dendroDragons);

    // Eevee - uncommon nature dragon from grassland (base form)
    public static readonly DragonProperties Eevee = new DragonProperties()
        .Name("Eevee")
        .Description("A versatile nature dragon with potential to evolve into different forms.")
        .Attacks(
        [
            new AttackPattern().Name("Tackle").Description("A basic tackle").Damage(13).Chance(45),
            new AttackPattern().Name("Chlorophyll Burst").Description("A burst of nature").Damage(16).Infliction(new ElementalInfliction(ElementType.Dendro, 2, 1)).Chance(40),
            new AttackPattern().Name("Swift").Description("Swift moving attack").Damage(14).Chance(35)
        ])
        .Chance(27)
        .AddTo(dendroDragons);

    // Tauros - rare nature dragon from mountains
    public static readonly DragonProperties Tauros = new DragonProperties()
        .Name("Tauros")
        .Description("A powerful bull-like nature dragon known for aggressive charges.")
        .Attacks(
        [
            new AttackPattern().Name("Horn Attack").Description("Attacks with horns").Damage(19).Chance(40),
            new AttackPattern().Name("Seed Bomb").Description("Seeds explode on impact").Damage(22).Infliction(new ElementalInfliction(ElementType.Dendro, 3, 2)).Chance(40),
            new AttackPattern().Name("Double Edge").Description("A double-edged attack").Damage(24).Chance(20)
        ])
        .Chance(12)
        .AddTo(dendroDragons);

    // Bidoof - rare nature dragon from mistlands (base-like evolution)
    public static readonly DragonProperties Bidoof = new DragonProperties()
        .Name("Bidoof")
        .Description("A hardy nature dragon of ancient lineage. Found in mystical waters.")
        .Attacks(
        [
            new AttackPattern().Name("Bite").Description("A powerful bite").Damage(16).Chance(50),
            new AttackPattern().Name("Ingrain").Description("Roots the dragon in place").Damage(20).Infliction(new ElementalInfliction(ElementType.Dendro, 3, 1)).Chance(40),
            new AttackPattern().Name("Tail Whip").Description("A tail whip attack").Damage(18).Chance(30)
        ])
        .Chance(9)
        .AddTo(dendroDragons);

    // Leafeon - rare nature dragon from mistlands (evolved nature type)
    public static readonly DragonProperties Leafeon = new DragonProperties()
        .Name("Leafeon")
        .Description("An evolved nature dragon surrounded by foliage. A mystical sight in the highlands.")
        .Attacks(
        [
            new AttackPattern().Name("Leaf Blade").Description("Attacks with leaf blade").Damage(26).Chance(35),
            new AttackPattern().Name("Solar Beam").Description("A beam of solar energy").Damage(30).Infliction(new ElementalInfliction(ElementType.Dendro, 4, 2)).Chance(40),
            new AttackPattern().Name("Magical Leaf").Description("Magical leaves attack").Damage(28).Chance(25),
            new AttackPattern().Name("Forest's Blessing").Description("Blesses with nature's power").Damage(33).Infliction(new ElementalInfliction(ElementType.Swirl, 4, 2)).Chance(20)
        ])
        .Chance(8)
        .AddTo(dendroDragons);

    /// <summary>
    /// Creates a Dendro-type dragon.
    /// </summary>
    public static Dragon CreateDragon(int level, float health = 1f, float maxHealth = 100f, DragonProperties? properties = null)
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


