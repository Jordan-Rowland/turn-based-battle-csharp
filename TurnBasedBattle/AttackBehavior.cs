
public interface IAttackBehavior
{
    public string Name { get; }
    public int Damage { get; }
    public int Attack(Characters.Character targetCharacter)
    {
        targetCharacter.TakeDamage(Damage);
        return Damage;
    }
}

public class Punch : IAttackBehavior
{
    public string Name { get; } = "PUNCH";
    public int Damage => 2;
}

public class BoneCrunch : IAttackBehavior
{
    public string Name { get; } = "BONE-CRUNCH";
    public int Damage
    {
        get
        {
            Random r = new();
            return r.Next(2);
        }
    }
}

public class Scratch : IAttackBehavior
{
    public string Name { get; } = "SCRATCH";
    public int Damage
    {
        get
        {
            Random r = new();
            if (r.Next(20) == 19) return 7;
            return r.Next(3);
        }
    }
}

public class UnRaveling : IAttackBehavior
{
    public string Name { get; } = "!Unraveling!";
    public int Damage
    {
        get
        {
            Random r = new();
            return r.Next(3);
        }
    }
}

public abstract class AttackModifierDecorator : IAttackBehavior
{
    public IAttackBehavior? BaseAttack { get; init; }
    public int Damage { get; init; }
    public string Name { get; init; } = "";
}

public class DoubleModifier : AttackModifierDecorator
{
    public DoubleModifier(IAttackBehavior baseAttack)
    {
        BaseAttack = baseAttack;
        Name = $"DOUBLE {BaseAttack.Name}";
        Damage = BaseAttack.Damage * 2;
    }
}

public class TripleModifier : AttackModifierDecorator
{
    public TripleModifier(IAttackBehavior baseAttack)
    {
        BaseAttack = baseAttack;
        Name = $"TRIPLE {BaseAttack.Name}";
        Damage = BaseAttack.Damage * 3;
    }
}
