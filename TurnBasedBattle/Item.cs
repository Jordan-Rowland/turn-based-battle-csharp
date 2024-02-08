namespace Inventory;

public class Item
{
    public string? Name { get; init; }
    public ItemType Type { get; init; }
    public int? Modifier { get; set; }
}

class HealthPotion : Item
{
    public HealthPotion()
    {
        Name = "Health Potion";
        Type = ItemType.Heal;
        Modifier = 10;
    }
}

class Poison : Item
{
    public Poison()
    {
        Name = "Poison";
        Type = ItemType.Damage;
        Modifier = 7;
    }
}

class PhoenixDown : Item
{
    public PhoenixDown()
    {
        Name = "Phoenix Down";
        Type = ItemType.Revive;
    }
}

public enum ItemType { Heal, Revive, Damage }
