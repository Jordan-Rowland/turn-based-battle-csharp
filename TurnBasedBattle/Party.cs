
public class Party
{
    public event Action<int>? PartyEnchant;

    public List<Characters.Character> Members { get; } = new();
    public int Turn { get; set; }
    public string Player { get; }
    public List<Inventory.Item>? Items { get; set; }

    public Party(string player, List<Inventory.Item> items)
    {
        Player = player;
        Items = items;
    }

    public bool IsActive() => (from m in Members where !m.Dead select m).Any();
    public void IncrementTurn() => Turn = Turn < Members!.Count - 1 ? Turn + 1 : 0;
    public Characters.Character GetCharacter()
    {
        Characters.Character character = Members![Turn];
        while (character.Dead)
        {
            IncrementTurn();
            character = Members[Turn];
        }
        IncrementTurn();
        return character;
    }
}
