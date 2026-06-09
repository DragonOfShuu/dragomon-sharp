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
        .Attacks([
            new AttackPattern()
                .WithName("Ice Shard")
                .WithDescription("Shards of ice strike")
                .WithDamage(11)
                .WithChance(50),
            new AttackPattern()
                .WithName("Aurora Beam")
                .WithDescription("A beam of icy light")
                .WithDamage(15)
                .WithInfliction(new ElementalInfliction(ElementType.Cryo, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Aqua Jet")
                .WithDescription("A jet of icy water")
                .WithDamage(13)
                .WithChance(30),
        ])
        .Chance(30)
        .AddTo(cryoDragons);

    // Shellder - uncommon ice dragon from grassland/swamp
    public static readonly DragonProperties Shellder = new DragonProperties()
        .Name("Shellder")
        .Description("A small crustacean ice dragon protected by a hard crystalline shell.")
        .Attacks([
            new AttackPattern()
                .WithName("Clamp")
                .WithDescription("Clamps down with icy grip")
                .WithDamage(12)
                .WithChance(45),
            new AttackPattern()
                .WithName("Ice Beam")
                .WithDescription("A beam of freezing ice")
                .WithDamage(16)
                .WithInfliction(new ElementalInfliction(ElementType.Cryo, 2, 1))
                .WithChance(38),
            new AttackPattern()
                .WithName("Icicle Spear")
                .WithDescription("Spears of ice attack")
                .WithDamage(14)
                .WithChance(35),
        ])
        .Chance(28)
        .AddTo(cryoDragons);

    // Swinub - common ice dragon from mountains
    public static readonly DragonProperties Swinub = new DragonProperties()
        .Name("Swinub")
        .Description("A sturdy ice dragon with a thick coat adapted to extreme cold.")
        .Attacks([
            new AttackPattern()
                .WithName("Powder Snow")
                .WithDescription("Powder of snow")
                .WithDamage(13)
                .WithChance(50),
            new AttackPattern()
                .WithName("Blizzard")
                .WithDescription("A fierce blizzard")
                .WithDamage(17)
                .WithInfliction(new ElementalInfliction(ElementType.Cryo, 2, 1))
                .WithChance(40),
            new AttackPattern()
                .WithName("Ancient Power")
                .WithDescription("The power of ancient ice")
                .WithDamage(15)
                .WithChance(35),
        ])
        .Chance(32)
        .AddTo(cryoDragons);

    // Lapras - rare ice dragon from mountains/mistlands
    public static readonly DragonProperties Lapras = new DragonProperties()
        .Name("Lapras")
        .Description("A majestic ice dragon of great size and ancient wisdom. Rarely encountered.")
        .Attacks([
            new AttackPattern()
                .WithName("Ice Crash")
                .WithDescription("Ice crashes down")
                .WithDamage(20)
                .WithChance(40),
            new AttackPattern()
                .WithName("Absolute Zero")
                .WithDescription("The ultimate freezing attack")
                .WithDamage(23)
                .WithInfliction(new ElementalInfliction(ElementType.Cryo, 3, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Glacial Spikes")
                .WithDescription("Spikes of glacial ice")
                .WithDamage(25)
                .WithInfliction(new ElementalInfliction(ElementType.Freeze, 3, 1))
                .WithChance(20),
        ])
        .Chance(15)
        .AddTo(cryoDragons);

    // Glaceon - rare ice dragon from mistlands (evolved ice type)
    public static readonly DragonProperties Glaceon = new DragonProperties()
        .Name("Glaceon")
        .Description("An evolved ice dragon of crystalline beauty. Found only in the mystlands.")
        .Attacks([
            new AttackPattern()
                .WithName("Ice Punch")
                .WithDescription("A punch of frozen power")
                .WithDamage(27)
                .WithChance(35),
            new AttackPattern()
                .WithName("Ice Storm")
                .WithDescription("A storm of ice and snow")
                .WithDamage(31)
                .WithInfliction(new ElementalInfliction(ElementType.Cryo, 4, 2))
                .WithChance(40),
            new AttackPattern()
                .WithName("Icy Wind")
                .WithDescription("Wind of freezing cold")
                .WithDamage(29)
                .WithChance(25),
            new AttackPattern()
                .WithName("Eternal Winter")
                .WithDescription("An eternal freeze descends")
                .WithDamage(34)
                .WithInfliction(new ElementalInfliction(ElementType.Freeze, 4, 2))
                .WithChance(20),
        ])
        .Chance(10)
        .AddTo(cryoDragons);

    /// <summary>
    /// Creates a Cryo-type dragon.
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
