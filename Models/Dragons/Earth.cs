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
        .Attacks(
        [
            new AttackPattern().Name("Sand Attack").Description("Attacks with sand").Damage(11).Chance(50),
            new AttackPattern().Name("Earthquake").Description("The earth shakes").Damage(15).Infliction(new ElementalInfliction(ElementType.Geo, 2, 1)).Chance(40),
            new AttackPattern().Name("Fury Swipes").Description("Rapid sand swipes").Damage(13).Chance(30)
        ])
        .Chance(30)
        .AddTo(geoDragons);

    // Diglett - common earth dragon from grassland/swamp
    public static readonly DragonProperties Diglett = new DragonProperties()
        .Name("Diglett")
        .Description("A peculiar earth dragon that burrows through soil, leaving tunnels behind.")
        .Attacks(
        [
            new AttackPattern().Name("Scratch").Description("A scratching attack").Damage(10).Chance(50),
            new AttackPattern().Name("Mud Slap").Description("Mud slaps the foe").Damage(14).Infliction(new ElementalInfliction(ElementType.Geo, 2, 1)).Chance(40),
            new AttackPattern().Name("Dig").Description("Digs underground").Damage(12).Chance(35)
        ])
        .Chance(32)
        .AddTo(geoDragons);

    // Cubone - uncommon earth dragon from mountains
    public static readonly DragonProperties Cubone = new DragonProperties()
        .Name("Cubone")
        .Description("A small earth dragon that wears a skull as armor, found in rocky terrain.")
        .Attacks(
        [
            new AttackPattern().Name("Bone Club").Description("A bone club attack").Damage(13).Chance(45),
            new AttackPattern().Name("Rock Throw").Description("Throws rocks").Damage(17).Infliction(new ElementalInfliction(ElementType.Geo, 2, 1)).Chance(38),
            new AttackPattern().Name("Focus Blast").Description("A focused blast of power").Damage(15).Chance(35)
        ])
        .Chance(28)
        .AddTo(geoDragons);

    // Phanpy - rare earth dragon from mountains with dense soil
    public static readonly DragonProperties Phanpy = new DragonProperties()
        .Name("Phanpy")
        .Description("A sturdy earth dragon with a powerful frame suited for rocky environments.")
        .Attacks(
        [
            new AttackPattern().Name("Tackle").Description("A heavy tackle").Damage(18).Chance(40),
            new AttackPattern().Name("Stone Edge").Description("Sharp stone edges").Damage(21).Infliction(new ElementalInfliction(ElementType.Geo, 3, 2)).Chance(40),
            new AttackPattern().Name("Boulder Crush").Description("Crushes with boulders").Damage(23).Infliction(new ElementalInfliction(ElementType.Crystallise, 3, 1)).Chance(20)
        ])
        .Chance(15)
        .AddTo(geoDragons);

    // Gligar - rare earth dragon from mistlands (evolved earth type)
    public static readonly DragonProperties Gligar = new DragonProperties()
        .Name("Gligar")
        .Description("An evolved earth dragon capable of flight despite its massive bulk. A rare find.")
        .Attacks(
        [
            new AttackPattern().Name("Wing Attack").Description("A powerful wing strike").Damage(24).Chance(35),
            new AttackPattern().Name("Earthquake Slam").Description("Slams earth with quake").Damage(28).Infliction(new ElementalInfliction(ElementType.Geo, 4, 2)).Chance(40),
            new AttackPattern().Name("Avalanche").Description("An avalanche of rock").Damage(26).Chance(25),
            new AttackPattern().Name("Stone Cyclone").Description("A cyclone of stone").Damage(31).Infliction(new ElementalInfliction(ElementType.Crystallise, 4, 2)).Chance(20)
        ])
        .Chance(10)
        .AddTo(geoDragons);

    /// <summary>
    /// Creates a Geo-type dragon.
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
        foreach (var dragon in geoDragons)
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


