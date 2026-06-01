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
        .Attacks(
        [
            new AttackPattern().Name("Water Gun").Description("A spray of water").Damage(10).Chance(50),
            new AttackPattern().Name("Bubble Beam").Description("Bubbles rise up in a beam").Damage(15).Infliction(new ElementalInfliction(ElementType.Hydro, 2, 1)).Chance(40),
            new AttackPattern().Name("Withdraw").Description("Retreats into shell for protection").Damage(5).Chance(25)
        ])
        .Chance(30)
        .AddTo(hydroDragons);

    // Psyduck - common water dragon from swamp/water
    public static readonly DragonProperties Psyduck = new DragonProperties()
        .Name("Psyduck")
        .Description("A confused and wandering water dragon often seen near water.")
        .Attacks(
        [
            new AttackPattern().Name("Water Pulse").Description("Pulses of water strike foe").Damage(12).Chance(50),
            new AttackPattern().Name("Hydro Pump").Description("A torrent of water").Damage(16).Infliction(new ElementalInfliction(ElementType.Hydro, 2, 1)).Chance(35),
            new AttackPattern().Name("Confusion").Description("Psychic confusion attack").Damage(14).Chance(35)
        ])
        .Chance(35)
        .AddTo(hydroDragons);

    // Slowpoke - uncommon water dragon from swamp
    public static readonly DragonProperties Slowpoke = new DragonProperties()
        .Name("Slowpoke")
        .Description("A slow-moving water dragon with limited intelligence but great power.")
        .Attacks(
        [
            new AttackPattern().Name("Water Sport").Description("Wets the area to reduce fire").Damage(13).Chance(45),
            new AttackPattern().Name("Aqua Ring").Description("Water swirls around foe").Damage(18).Infliction(new ElementalInfliction(ElementType.Hydro, 3, 2)).Chance(40),
            new AttackPattern().Name("Headbutt").Description("A powerful headbutt").Damage(16).Chance(25)
        ])
        .Chance(25)
        .AddTo(hydroDragons);

    // Horsea - rare water dragon from deep water
    public static readonly DragonProperties Horsea = new DragonProperties()
        .Name("Horsea")
        .Description("An elegant seahorse-like water dragon found in deep waters.")
        .Attacks(
        [
            new AttackPattern().Name("Dragon Rage").Description("The ancient power of dragons").Damage(20).Chance(40),
            new AttackPattern().Name("Hydro Cannon").Description("A powerful cannon of water").Damage(24).Infliction(new ElementalInfliction(ElementType.Hydro, 3, 2)).Chance(38),
            new AttackPattern().Name("Brine").Description("A concentrated salt water attack").Damage(22).Chance(22)
        ])
        .Chance(15)
        .AddTo(hydroDragons);

    // Vaporeon - rare water dragon from mystlands (evolved water type)
    public static readonly DragonProperties Vaporeon = new DragonProperties()
        .Name("Vaporeon")
        .Description("An evolved water dragon that blends seamlessly with its environment. Found in mystical waters.")
        .Attacks(
        [
            new AttackPattern().Name("Water Shuriken").Description("Water shuriken attacks").Damage(22).Chance(35),
            new AttackPattern().Name("Aqua Jet").Description("A jet of water propels the dragon").Damage(28).Infliction(new ElementalInfliction(ElementType.Hydro, 4, 2)).Chance(40),
            new AttackPattern().Name("Waterfall").Description("Water falls down upon the foe").Damage(26).Chance(25),
            new AttackPattern().Name("Tidal Wave").Description("A massive wave of destruction").Damage(31).Infliction(new ElementalInfliction(ElementType.Vaporise, 4, 2)).Chance(20)
        ])
        .Chance(10)
        .AddTo(hydroDragons);

    /// <summary>
    /// Creates a Hydro-type dragon.
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

