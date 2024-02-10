namespace Character;

public abstract class CCharacter
{
    public virtual string? Name { get; init; }
    public virtual int HP { get; set; }
    public virtual int MaxHP { get; init; }
    public virtual bool Dead { get; set; }
    public virtual IAttackBehavior? AttackBehavior { get; set; }

    public virtual int PerformAttack(CCharacter targetCharacter) => AttackBehavior!.Attack(targetCharacter);
    
    public void Pass() => Console.WriteLine($"{Name} did NOTHING");

    public void TakeDamage(int? damage)
    {
        HP -= damage ?? 0;
        if (HP <= 0)
        {
            Dead = true;
            HP = 0;
            Console.WriteLine($"{Name} has died!");  // Possibly move this to the Game class
        }
    }

    public void GainHealth(int? health)
    {
        HP += health ?? 0;
        if (HP > MaxHP) HP = MaxHP;
    }

    public void Revive()
    {
        Random r = new();
        if (r.Next(10) == 9) HP = MaxHP;
        else HP = 5;
        Dead = false;
    }

    public void UseItem(Inventory.Item item)
    {
        if (item.Type == Inventory.ItemType.Heal) GainHealth(item.Value);
        else if (item.Type == Inventory.ItemType.Damage) TakeDamage(item.Value);
        else if (item.Type == Inventory.ItemType.Revive) Revive();
    }
}

class Hero : CCharacter
{
    public override string? Name { get; init; } = "Hero";
    public override int HP { get; set; } = 25;
    public override int MaxHP { get; init; } = 25;
    public override bool Dead { get; set; } = false;
    public override IAttackBehavior? AttackBehavior { get; set; } = new Punch();

    public override int PerformAttack(CCharacter targetCharacter) => AttackBehavior!.Attack(targetCharacter);
}

class Skeleton : CCharacter
{
    public override string? Name { get; init; } = "SKELETON";
    public override int HP { get; set; } = 5;
    public override int MaxHP { get; init; } = 5;
    public override bool Dead { get; set; } = false;
    public override IAttackBehavior? AttackBehavior { get; set; } = new BoneCrunch();

    public override int PerformAttack(CCharacter targetCharacter) => AttackBehavior!.Attack(targetCharacter);
}

class UncodedOne : CCharacter
{
    public override string? Name { get; init; } = "Uncoded One";
    public override int HP { get; set; } = 40;
    public override int MaxHP { get; init; } = 40;
    public override bool Dead { get; set; } = false;
    public override IAttackBehavior? AttackBehavior { get; set; } = new Punch();

    public override int PerformAttack(CCharacter targetCharacter) => AttackBehavior!.Attack(targetCharacter);
}
