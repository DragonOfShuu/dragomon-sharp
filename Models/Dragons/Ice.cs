using DragoSharp.Models.Elements;
using DragoSharp.Models.Utils;

namespace DragoSharp.Models.Dragons;

public static class Cryo
{
    private static readonly List<DragonProperties> cryoDragons = [];

    // Seel - common ice dragon from mountains with snow
    public static readonly DragonProperties Seel = new DragonProperties()
        .Name("Seel")
        .Description("A graceful ice dragon with a sleek form adapted for frozen waters.")
        .Attacks(
        [
            new AttackPattern().Name("Ice Shard").Description("Shards of ice strike").Damage(11).Chance(50),
            new AttackPattern().Name("Aurora Beam").Description("A beam of icy light").Damage(15).Infliction(new ElementalInfliction(ElementType.Cryo, 2, 1)).Chance(40),
            new AttackPattern().Name("Aqua Jet").Description("A jet of icy water").Damage(13).Chance(30)
        ])
        .Chance(30)
        .AddTo(cryoDragons);

    // Shellder - uncommon ice dragon from grassland/swamp
    public static readonly DragonProperties Shellder = new DragonProperties()
        .Name("Shellder")
        .Description("A small crustacean ice dragon protected by a hard crystalline shell.")
        .Attacks(
        [
            new AttackPattern().Name("Clamp").Description("Clamps down with icy grip").Damage(12).Chance(45),
            new AttackPattern().Name("Ice Beam").Description("A beam of freezing ice").Damage(16).Infliction(new ElementalInfliction(ElementType.Cryo, 2, 1)).Chance(38),
            new AttackPattern().Name("Icicle Spear").Description("Spears of ice attack").Damage(14).Chance(35)
        ])
        .Chance(28)
        .AddTo(cryoDragons);

    // Swinub - common ice dragon from mountains
    public static readonly DragonProperties Swinub = new DragonProperties()
        .Name("Swinub")
        .Description("A sturdy ice dragon with a thick coat adapted to extreme cold.")
        .Attacks(
        [
            new AttackPattern().Name("Powder Snow").Description("Powder of snow").Damage(13).Chance(50),
            new AttackPattern().Name("Blizzard").Description("A fierce blizzard").Damage(17).Infliction(new ElementalInfliction(ElementType.Cryo, 2, 1)).Chance(40),
            new AttackPattern().Name("Ancient Power").Description("The power of ancient ice").Damage(15).Chance(35)
        ])
        .Chance(32)
        .AddTo(cryoDragons);

    // Lapras - rare ice dragon from mountains/mistlands
    public static readonly DragonProperties Lapras = new DragonProperties()
        .Name("Lapras")
        .Description("A majestic ice dragon of great size and ancient wisdom. Rarely encountered.")
        .Attacks(
        [
            new AttackPattern().Name("Ice Crash").Description("Ice crashes down").Damage(20).Chance(40),
            new AttackPattern().Name("Absolute Zero").Description("The ultimate freezing attack").Damage(23).Infliction(new ElementalInfliction(ElementType.Cryo, 3, 2)).Chance(40),
            new AttackPattern().Name("Glacial Spikes").Description("Spikes of glacial ice").Damage(25).Infliction(new ElementalInfliction(ElementType.Freeze, 3, 1)).Chance(20)
        ])
        .Chance(15)
        .AddTo(cryoDragons);

    // Glaceon - rare ice dragon from mistlands (evolved ice type)
    public static readonly DragonProperties Glaceon = new DragonProperties()
        .Name("Glaceon")
        .Description("An evolved ice dragon of crystalline beauty. Found only in the mystlands.")
        .Attacks(
        [
            new AttackPattern().Name("Ice Punch").Description("A punch of frozen power").Damage(27).Chance(35),
            new AttackPattern().Name("Ice Storm").Description("A storm of ice and snow").Damage(31).Infliction(new ElementalInfliction(ElementType.Cryo, 4, 2)).Chance(40),
            new AttackPattern().Name("Icy Wind").Description("Wind of freezing cold").Damage(29).Chance(25),
            new AttackPattern().Name("Eternal Winter").Description("An eternal freeze descends").Damage(34).Infliction(new ElementalInfliction(ElementType.Freeze, 4, 2)).Chance(20)
        ])
        .Chance(10)
        .AddTo(cryoDragons);

    /// <summary>
    /// Creates a Cryo-type dragon.
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
            ElementType.Cryo,
            properties,
            selectedAttacks
        );
    }

    private static DragonProperties SelectRandomDragon()
    {
        int totalWeight = cryoDragons.Sum(dp => dp.SelectionChance);
        int randomValue = Statics.GenRNum(0, totalWeight);

        int currentWeight = 0;
        foreach (var dragon in cryoDragons)
        {
            currentWeight += dragon.SelectionChance;
            if (randomValue < currentWeight)
            {
                return dragon;
            }
        }

        return Seel;
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


