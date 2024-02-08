namespace Character;

public interface ICharacter
{
    public string Name { get; init; }
    public int HP { get; set; }
    public int MaxHP { get; init; }
    public bool Dead { get; set; }

    public void Pass() => Console.WriteLine($"{Name} did NOTHING");
    public void Attack(ICharacter targetCharacter) { Console.WriteLine($"{Name} Attacked"); }
    
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Dead = true;
            HP = 0;
            Console.WriteLine($"{Name} has died!");
        }
    }

    public void UseItem(Inventory.Item item, ICharacter targetCharacter)
    {
        Console.WriteLine($"{Name} Used {item.Name} on {targetCharacter.Name}");
    }
}

class Hero : ICharacter
{
    public string Name { get; init; } = "Hero";
    public int HP { get; set; } = 25;
    public int MaxHP { get; init; } = 25;
    public bool Dead { get; set; } = false;
    public void Attack(ICharacter targetCharacter)
    {
        int damage = 2;
        targetCharacter.TakeDamage(damage);
        Console.WriteLine($"{Name} used PUNCH on {targetCharacter.Name}");
        Console.WriteLine($"PUNCH did {damage} damage");
    }
}

class Skeleton : ICharacter
{
    public string Name { get; init; } = "SKELETON";
    public int HP { get; set; } = 5;
    public int MaxHP { get; init; } = 5;
    public bool Dead { get; set; } = false;
    public void Attack(ICharacter targetCharacter)
    {
        Random r = new();
        int damage = r.Next(2);
        targetCharacter.TakeDamage(damage);
        Console.WriteLine($"{Name} used BONE-CRUNCH on {targetCharacter.Name}");
        Console.WriteLine($"BONE-CRUNCH did {damage} damage");
    }
}

class UncodedOne : ICharacter
{
    public string Name { get; init; } = "Uncoded One";
    public int HP { get; set; } = 40;
    public int MaxHP { get; init; } = 40;
    public bool Dead { get; set; } = false;
    public void Attack(ICharacter targetCharacter)
    {
        Random r = new();
        int damage = r.Next(3);
        targetCharacter.TakeDamage(damage);
        Console.WriteLine($"{Name} used !Unraveling! on {targetCharacter.Name}");
        Console.WriteLine($"!Unraveling! did {damage} damage");
    }
}

