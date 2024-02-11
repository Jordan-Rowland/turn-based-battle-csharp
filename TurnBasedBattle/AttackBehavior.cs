
public interface IAttackBehavior {
    public string Name { get; }
    public int Attack(Characters.Character targetCharacter);
}

public class Punch : IAttackBehavior
{
    public string Name { get; } = "PUNCH";
    public int Attack(Characters.Character targetCharacter)
    {
        int damage = 2;
        targetCharacter.TakeDamage(damage);
        return damage;
    }
}

public class BoneCrunch : IAttackBehavior
{
    public string Name { get; } = "BONE-CRUNCH";
    public int Attack(Characters.Character targetCharacter)
    {
        Random r = new();
        int damage = r.Next(2);
        targetCharacter.TakeDamage(damage);
        return damage;
    }
}

public class UnRaveling : IAttackBehavior
{
    public string Name { get; } = "!Unraveling!";
    public int Attack(Characters.Character targetCharacter)
    {
        Random r = new();
        int damage = r.Next(3);
        targetCharacter.TakeDamage(damage);
        return damage;
    }
}
