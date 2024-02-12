namespace Characters;

public abstract class Character
{
    public virtual string Name { get; init; } = "";
    public virtual int HP { get; set; }
    public virtual int MaxHP { get; init; }
    public virtual bool Dead { get; set; }
    public virtual IAttackBehavior? AttackBehavior { get; set; }
    public virtual Party? Party { get; set; }
    public void InheritEnchantment(int value) {}

    public Character(Party party)
    {
        Party = party;
        Party.Members?.Add(this);
    }

    public void Pass() => Console.WriteLine($"{Name} did NOTHING");

    public virtual int PerformAttack(Character targetCharacter) => AttackBehavior!.Attack(targetCharacter);

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

class Hero : Character
{
    public override string Name { get; init; } = "Hero";
    public override int HP { get; set; } = 25;
    public override int MaxHP { get; init; } = 25;
    public override IAttackBehavior? AttackBehavior { get; set; } = new Punch();
    public Hero(Party party) : base(party) => Party!.PartyEnchant += InheritEnchantment;
}

class Skeleton : Character
{
    public override string Name { get; init; } = "SKELETON";
    public override int HP { get; set; } = 5;
    public override int MaxHP { get; init; } = 5;
    public override IAttackBehavior? AttackBehavior { get; set; } = new BoneCrunch();
    public Skeleton(Party party) : base(party) => Party!.PartyEnchant += InheritEnchantment;
}

class Goblin : Character
{
    public override string Name { get; init; } = "GOBLIN";
    public override int HP { get; set; } = 10;
    public override int MaxHP { get; init; } = 10;
    public override IAttackBehavior? AttackBehavior { get; set; } = new Scratch();
    public Goblin(Party party) : base(party) => Party!.PartyEnchant += InheritEnchantment;
}

class UncodedOne : Character
{
    public override string Name { get; init; } = "Uncoded One";
    public override int HP { get; set; } = 40;
    public override int MaxHP { get; init; } = 40;
    public override IAttackBehavior? AttackBehavior { get; set; } = new UnRaveling();
    public UncodedOne(Party party) : base(party) => Party!.PartyEnchant += InheritEnchantment;
}
