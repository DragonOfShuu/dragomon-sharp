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
        .Attacks(
        [
            new AttackPattern().Name("Thunderbolt").Description("A bolt of lightning").Damage(11).Chance(50),
            new AttackPattern().Name("Thunder Wave").Description("Paralyzing waves of electricity").Damage(16).Infliction(new ElementalInfliction(ElementType.Electro, 2, 1)).Chance(40),
            new AttackPattern().Name("Quick Attack").Description("A swift strike").Damage(13).Chance(30)
        ])
        .Chance(35)
        .AddTo(electroDragons);

    // Magnemite - uncommon electric dragon from mountains
    public static readonly DragonProperties Magnemite = new DragonProperties()
        .Name("Magnemite")
        .Description("A mechanical electric dragon that emits powerful electromagnetic waves.")
        .Attacks(
        [
            new AttackPattern().Name("Tackle").Description("A heavy collision").Damage(14).Chance(45),
            new AttackPattern().Name("Electromagnetic Pulse").Description("A pulse of magnetic energy").Damage(18).Infliction(new ElementalInfliction(ElementType.Electro, 2, 1)).Chance(38),
            new AttackPattern().Name("Thunder Lock").Description("Binds opponent with electricity").Damage(17).Infliction(new ElementalInfliction(ElementType.Electrocharge, 3, 1)).Chance(25)
        ])
        .Chance(25)
        .AddTo(electroDragons);

    // Voltorb - common electric dragon from grassland/swamp
    public static readonly DragonProperties Voltorb = new DragonProperties()
        .Name("Voltorb")
        .Description("A spherical electric dragon constantly charged with volatile energy.")
        .Attacks(
        [
            new AttackPattern().Name("Spark").Description("A spark of electricity").Damage(13).Chance(50),
            new AttackPattern().Name("Charge Beam").Description("A beam of charging electricity").Damage(17).Infliction(new ElementalInfliction(ElementType.Electro, 2, 1)).Chance(40),
            new AttackPattern().Name("Sonic Boom").Description("A booming sound wave").Damage(15).Chance(35)
        ])
        .Chance(30)
        .AddTo(electroDragons);

    // Electabuzz - rare electric dragon from mountains
    public static readonly DragonProperties Electabuzz = new DragonProperties()
        .Name("Electabuzz")
        .Description("A powerful humanoid electric dragon with crackling energy surrounding its body.")
        .Attacks(
        [
            new AttackPattern().Name("Thunder Punch").Description("A punch charged with electricity").Damage(20).Chance(40),
            new AttackPattern().Name("Thunderbolt").Description("A powerful bolt of lightning").Damage(23).Infliction(new ElementalInfliction(ElementType.Electro, 3, 2)).Chance(40),
            new AttackPattern().Name("Thunder Crash").Description("Electricity crashes down").Damage(25).Infliction(new ElementalInfliction(ElementType.Overload, 3, 1)).Chance(20)
        ])
        .Chance(15)
        .AddTo(electroDragons);

    // Jolteon - rare electric dragon from mistlands (evolved electric type)
    public static readonly DragonProperties Jolteon = new DragonProperties()
        .Name("Jolteon")
        .Description("An evolved electric dragon of tremendous speed and power. Found in mystical areas.")
        .Attacks(
        [
            new AttackPattern().Name("Thunder Strike").Description("A strike of pure electricity").Damage(26).Chance(35),
            new AttackPattern().Name("Discharge").Description("Electricity spreads in all directions").Damage(30).Infliction(new ElementalInfliction(ElementType.Electro, 4, 2)).Chance(40),
            new AttackPattern().Name("Lightning Bolt").Description("A focused bolt of lightning").Damage(28).Chance(25),
            new AttackPattern().Name("Thunder Nova").Description("A nova of electric energy").Damage(33).Infliction(new ElementalInfliction(ElementType.Electrocharge, 4, 2)).Chance(20)
        ])
        .Chance(10)
        .AddTo(electroDragons);

    /// <summary>
    /// Creates an Electro-type dragon.
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


