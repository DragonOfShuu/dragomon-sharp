using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public static class Anemo
{
    private static readonly List<DragonProperties> anemoDragons = [];

    // Gastly - common wind dragon from swamp/mistlands
    public static readonly DragonProperties Gastly = new DragonProperties()
        .Name("Gastly")
        .Description("A spectral wind dragon wreathed in ethereal vapor, drifting through misty areas.")
        .Attacks(
        [
            new AttackPattern().Name("Shadow Ball").Description("A ball of shadow energy").Damage(10).Chance(50),
            new AttackPattern().Name("Ominous Wind").Description("Wind carries ill intent").Damage(14).Infliction(new ElementalInfliction(ElementType.Anemo, 2, 1)).Chance(40),
            new AttackPattern().Name("Night Shade").Description("Shadowy night attack").Damage(12).Chance(30)
        ])
        .Chance(30)
        .AddTo(anemoDragons);

    // Misdreavus - uncommon wind dragon from mistlands
    public static readonly DragonProperties Misdreavus = new DragonProperties()
        .Name("Misdreavus")
        .Description("A wind dragon that feeds on emotions. Found in ethereal realms.")
        .Attacks(
        [
            new AttackPattern().Name("Disarming Voice").Description("A disarming wind voice").Damage(12).Chance(45),
            new AttackPattern().Name("Wind Blast").Description("A blast of wind").Damage(16).Infliction(new ElementalInfliction(ElementType.Anemo, 2, 1)).Chance(38),
            new AttackPattern().Name("Screech").Description("A piercing screech").Damage(14).Chance(35)
        ])
        .Chance(26)
        .AddTo(anemoDragons);

    // Duskull - uncommon wind dragon from swamp/mistlands
    public static readonly DragonProperties Duskull = new DragonProperties()
        .Name("Duskull")
        .Description("A wind dragon that glides through shadows. Elusive and mysterious.")
        .Attacks(
        [
            new AttackPattern().Name("Shadow Sneak").Description("Sneaking shadow attack").Damage(13).Chance(45),
            new AttackPattern().Name("Wind Storm").Description("A storm of wind").Damage(17).Infliction(new ElementalInfliction(ElementType.Anemo, 2, 1)).Chance(40),
            new AttackPattern().Name("Pursuit").Description("Pursues the foe").Damage(15).Chance(35)
        ])
        .Chance(28)
        .AddTo(anemoDragons);

    // Drifloon - rare wind dragon from mountains
    public static readonly DragonProperties Drifloon = new DragonProperties()
        .Name("Drifloon")
        .Description("A buoyant wind dragon that floats on air currents. Found at high altitudes.")
        .Attacks(
        [
            new AttackPattern().Name("Gust").Description("A gust of wind").Damage(17).Chance(40),
            new AttackPattern().Name("Whirlwind").Description("A spiraling whirlwind").Damage(20).Infliction(new ElementalInfliction(ElementType.Anemo, 3, 1)).Chance(40),
            new AttackPattern().Name("Minimize").Description("Makes the dragon tiny").Damage(22).Chance(20)
        ])
        .Chance(16)
        .AddTo(anemoDragons);

    // Spiritomb - rare wind dragon from mistlands (advanced form)
    public static readonly DragonProperties Spiritomb = new DragonProperties()
        .Name("Spiritomb")
        .Description("A collected wind dragon formed from many spirits. Rarely seen in mystical heights.")
        .Attacks(
        [
            new AttackPattern().Name("Dark Pulse").Description("A pulse of dark wind").Damage(21).Chance(40),
            new AttackPattern().Name("Spirit Cyclone").Description("A cyclone of spirits").Damage(24).Infliction(new ElementalInfliction(ElementType.Anemo, 3, 2)).Chance(40),
            new AttackPattern().Name("Phantom Assault").Description("Phantom wind assault").Damage(26).Infliction(new ElementalInfliction(ElementType.Swirl, 3, 1)).Chance(20)
        ])
        .Chance(14)
        .AddTo(anemoDragons);

    // Tornadus - rare wind dragon from mistlands (evolved wind type)
    public static readonly DragonProperties Tornadus = new DragonProperties()
        .Name("Tornadus")
        .Description("An evolved wind dragon of immense power. Master of tempests and storms.")
        .Attacks(
        [
            new AttackPattern().Name("Hurricane").Description("A devastating hurricane").Damage(28).Chance(35),
            new AttackPattern().Name("Eternal Cyclone").Description("An endless cyclone").Damage(32).Infliction(new ElementalInfliction(ElementType.Anemo, 4, 2)).Chance(40),
            new AttackPattern().Name("Tornado Dive").Description("Dives into a tornado").Damage(30).Chance(25),
            new AttackPattern().Name("Tempest Rising").Description("Rises with a tempest").Damage(35).Infliction(new ElementalInfliction(ElementType.Swirl, 4, 2)).Chance(20)
        ])
        .Chance(10)
        .AddTo(anemoDragons);

    /// <summary>
    /// Creates an Anemo-type dragon.
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


