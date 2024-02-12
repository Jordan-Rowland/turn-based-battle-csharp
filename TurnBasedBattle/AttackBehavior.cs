
public interface IAttackBehavior
{
    public string? Name { get; }
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
    public IAttackBehavior? BaseAttack;
    public virtual int Damage { get; }
    public virtual string? Name { get; }
}

public class DoubleModifier : AttackModifierDecorator
{
    public DoubleModifier(IAttackBehavior baseAttack)
    {
        BaseAttack = baseAttack;
        Name = $"DOUBLE {BaseAttack!.Name}";
    }
    public override int Damage { get => BaseAttack!.Damage * 2; }
    public override string? Name { get; }
}

public class TripleModifier : AttackModifierDecorator
{
    public TripleModifier(IAttackBehavior baseAttack)
    {
        BaseAttack = baseAttack;
        Name = $"TRIPLE {BaseAttack!.Name}";
    }
    public override int Damage { get => BaseAttack!.Damage * 3; }
    public override string? Name { get; }
}
