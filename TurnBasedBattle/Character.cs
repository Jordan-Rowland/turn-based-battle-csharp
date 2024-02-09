// namespace Character;

// //!! Experiment turning this back into a class
// public interface CCharacter
// {
//     public string Name { get; init; }
//     public int HP { get; set; }
//     public int MaxHP { get; init; }
//     public bool Dead { get; set; }
//     public string StandardAttack { get; }

//     public void Pass() => Console.WriteLine($"{Name} did NOTHING");
//     public int Attack(CCharacter targetCharacter) => 0;
    
//     public void TakeDamage(int? damage)
//     {
//         HP -= damage ?? 0;
//         if (HP <= 0)
//         {
//             Dead = true;
//             HP = 0;
//             Console.WriteLine($"{Name} has died!");  // Possibly move this to the Game class
//         }
//     }

//     public void GainHealth(int? health)
//     {
//         HP += health ?? 0;
//         if (HP > MaxHP) HP = MaxHP;
//     }

//     public void Revive()
//     {
//         Random r = new();
//         if (r.Next(10) == 9) HP = MaxHP;
//         else HP = 5;
//         Dead = false;
//     }

//     public void UseItem(Inventory.Item item)
//     {
//         if (item.Type == Inventory.ItemType.Heal) GainHealth(item.Value);
//         else if (item.Type == Inventory.ItemType.Damage) TakeDamage(item.Value);
//         else if (item.Type == Inventory.ItemType.Revive) Revive();
//     }
// }

// class Hero : CCharacter
// {
//     public string Name { get; init; } = "Hero";
//     public int HP { get; set; } = 25;
//     public int MaxHP { get; init; } = 25;
//     public bool Dead { get; set; } = false;
//     public string StandardAttack { get; } = "PUNCH";

//     public int Attack(CCharacter targetCharacter)
//     {
//         int damage = 2;
//         targetCharacter.TakeDamage(damage);
//         return damage;
//     }
// }

// class Skeleton : CCharacter
// {
//     public string Name { get; init; } = "SKELETON";
//     public int HP { get; set; } = 5;
//     public int MaxHP { get; init; } = 5;
//     public bool Dead { get; set; } = false;
//     public string StandardAttack { get; } = "BONE-CRUNCH";
    
//     public int Attack(CCharacter targetCharacter)
//     {
//         Random r = new();
//         int damage = r.Next(2);
//         targetCharacter.TakeDamage(damage);
//         return damage;
//     }
// }

// class UncodedOne : CCharacter
// {
//     public string Name { get; init; } = "Uncoded One";
//     public int HP { get; set; } = 40;
//     public int MaxHP { get; init; } = 40;
//     public bool Dead { get; set; } = false;
//     public string StandardAttack { get; } = "!Unraveling!";
    
//     public int Attack(CCharacter targetCharacter)
//     {
//         Random r = new();
//         int damage = r.Next(3);
//         targetCharacter.TakeDamage(damage);
//         return damage;
//     }
// }


namespace Character;

//!! Experiment turning this back into a class
public abstract class CCharacter  // Rename if goiong this route
{
    public virtual string? Name { get; init; }
    public virtual int HP { get; set; }
    public virtual int MaxHP { get; init; }
    public virtual bool Dead { get; set; }
    public virtual string? StandardAttack { get; }

    public void Pass() => Console.WriteLine($"{Name} did NOTHING");
    public virtual int Attack(CCharacter targetCharacter) => 0;
    
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
    public override string StandardAttack { get; } = "PUNCH";

    public override int Attack(CCharacter targetCharacter)
    {
        int damage = 2;
        targetCharacter.TakeDamage(damage);
        return damage;
    }
}

class Skeleton : CCharacter
{
    public override string? Name { get; init; } = "SKELETON";
    public override int HP { get; set; } = 5;
    public override int MaxHP { get; init; } = 5;
    public override bool Dead { get; set; } = false;
    public override string StandardAttack { get; } = "BONE-CRUNCH";
    
    public override int Attack(CCharacter targetCharacter)
    {
        Random r = new();
        int damage = r.Next(2);
        targetCharacter.TakeDamage(damage);
        return damage;
    }
}

class UncodedOne : CCharacter
{
    public override string? Name { get; init; } = "Uncoded One";
    public override int HP { get; set; } = 40;
    public override int MaxHP { get; init; } = 40;
    public override bool Dead { get; set; } = false;
    public override string StandardAttack { get; } = "!Unraveling!";
    
    public override int Attack(CCharacter targetCharacter)
    {
        Random r = new();
        int damage = r.Next(3);
        targetCharacter.TakeDamage(damage);
        return damage;
    }
}
