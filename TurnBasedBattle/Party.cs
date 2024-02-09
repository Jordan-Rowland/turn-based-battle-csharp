
class Party
{
    public Character.CCharacter[] Members { get; }
    public int Turn { get; set; }
    public string Player { get; }
    public List<Inventory.Item>? Items { get; set; }

    public Party(string player, List<Inventory.Item> items, params Character.CCharacter[] members)
    {
        Player = player;
        Items = items;
        Members = members;
    }

    public bool IsActive() => (from m in Members where !m.Dead select m).Any();
    public void IncrementTurn() => Turn = Turn < Members.Length - 1 ? Turn + 1 : 0;
    public Character.CCharacter GetCharacter()
    {
        Character.CCharacter character = Members[Turn];
        while (character.Dead)
        {
            IncrementTurn();
            character = Members[Turn];
        }
        IncrementTurn();
        return character;
    }
}