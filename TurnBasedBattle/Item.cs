namespace Inventory;

public abstract class Item
{
    public string Name { get; init; } = "";
    public ItemCategory Category { get; init; }
    public int Value { get; set; }
}

class HealthPotion : Item
{
    public HealthPotion()
    {
        Name = "Health Potion";
        Category = ItemCategory.Heal;
        Value = 10;
    }
}

class Poison : Item
{
    public Poison()
    {
        Name = "Poison";
        Category = ItemCategory.Damage;
        Value = 7;
    }
}

class PhoenixDown : Item
{
    public PhoenixDown()
    {
        Name = "Phoenix Down";
        Category = ItemCategory.Revive;
    }
}

public static class SimpleItemFactory
{
    public static Item CreateItem(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.HealthPotion => new HealthPotion(),
            ItemType.Poison => new Poison(),
            ItemType.PhoenixDown => new PhoenixDown(),
            _ => throw new NotImplementedException(),
        };
    }
}

public enum ItemCategory { Heal, Revive, Damage }

public enum ItemType { HealthPotion, Poison, PhoenixDown }
