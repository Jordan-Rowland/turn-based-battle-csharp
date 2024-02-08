class Game
{
    public Party MainParty { get; init; }
    public Party EnemyParty { get; set; }
    public Party[] EnemyParties { get; init; }

    public Game()
    {
        MainParty = new(
            "Player1",
            new List<Inventory.Item>() {
                new Inventory.HealthPotion(),
                new Inventory.HealthPotion(),
                new Inventory.HealthPotion(),
                new Inventory.Poison(),
            },
            new Character.Hero() { Name = "Hiro" },
            new Character.Hero() { Name = "Aya" }
        );
        EnemyParties = new Party[] {
            new(
                "Computer",
                new List<Inventory.Item>() {
                    new Inventory.HealthPotion(),
                },
                new Character.Skeleton()
            ),
            new(
                "Computer",
                new List<Inventory.Item>() {
                    new Inventory.HealthPotion(),
                    new Inventory.Poison(),
                    new Inventory.PhoenixDown(),
                },
                new Character.Skeleton(),
                new Character.Skeleton()
            ),
            new(
                "Computer",
                new List<Inventory.Item>() {
                    new Inventory.HealthPotion(),
                    new Inventory.HealthPotion(),
                    new Inventory.HealthPotion(),
                    new Inventory.Poison(),
                    new Inventory.Poison(),
                },
                new Character.UncodedOne()
            ),
        };
        EnemyParty = EnemyParties[0];
    }

    public void RunAllBattles()
    {
        foreach (Party enemyParty in EnemyParties)
        {
            EnemyParty = enemyParty;
            RunBattle();
        }

        if (MainParty.IsActive()) Console.WriteLine("You have defeated the Uncoded One!");
        else Console.WriteLine("Darkness has prevailed...");
    }

    public void RunBattle()
    {
        Party? currParty = MainParty;
        Console.Clear();
        while (true)
        {
            Console.Clear(); Thread.Sleep(250);
            if (currParty.IsActive())
            {
                PlayTurn(currParty, player: currParty.Player);
                currParty = currParty == MainParty ? EnemyParty : MainParty;
                Console.Write("Press ENTER to continue...");
                Console.ReadLine();
            }
            else break;
        }
    }

    private void PlayTurn(Party party, string player)
    {
        Character.ICharacter character = party.GetCharacter();
        bool isComputer = player.ToLower() == "computer";
        while (true)
        {
            Console.WriteLine(
                $"It is {character.Name}'s turn ({character.HP}/{character.MaxHP} Health)..."
            );
            DisplayUserActions();
            if (player.ToLower() == "computer") Console.WriteLine();
            else Console.Write(" > ");
            if (isComputer)
            {
                // TODO: Need logic to compute how computer enemy will react
                Random r = new();
                Party opposingParty = party == MainParty ? EnemyParty : MainParty;
                character.Attack(opposingParty.Members[r.Next(opposingParty.Members.Length)]);
            }
            else
            {
                switch (GetUserInputAction(Console.ReadLine()))
                {
                    case Action.Attack:
                        SelectAndAttackCharacter();
                        break;
                    case Action.UseItem:
                        if (party.Items!.Any())
                        {
                            Inventory.Item? chosenItem = ChoseItem(party);
                            if (chosenItem == null) continue;
                            UseItemOnCharacter(character, chosenItem);
                        }
                        break;
                    default:
                        character.Pass();
                        break;
                }
            }
            break;
        }

        void SelectAndAttackCharacter()
        {
            DisplayTargetCharacters();
            character.Attack(GetUserInputTargetCharacter(Console.ReadLine()));
        }

        static Inventory.Item? ChoseItem(Party party)
        {
            DisplayItems(party.Items!);
            return GetUserInputItem(Console.ReadLine(), party.Items!);;
        }

        void UseItemOnCharacter(Character.ICharacter character, Inventory.Item? chosenItem)
        {
            DisplayTargetCharacters();
            character.UseItem(chosenItem!, GetUserInputTargetCharacter(Console.ReadLine(), character));
        }
    }

    private static Action GetUserInputAction(string? input)
    {
        if (int.TryParse(input, out int value))
        {
            return value switch
            {
                1 => Action.Attack,
                2 => Action.UseItem,
                _ => Action.Pass,
            };
        }
        else return Action.Pass;
    }

    private Character.ICharacter GetUserInputTargetCharacter(string? input, Character.ICharacter? character = null)
    {
        if (int.TryParse(input, out int value))
        {
            if (value <= MainParty.Members.Length) return MainParty.Members[value - 1];
            else if (value <= MainParty.Members.Length + EnemyParty.Members.Length)
            {
                value -= MainParty.Members.Length;
                return EnemyParty.Members[value - 1];
            }
        }
        return character ?? EnemyParty.Members[0];
    }

    readonly Dictionary<int, string> actions = new()
    {
        [1] = "Attack",
        [2] = "Use Item",
        [0] = "Pass",
    };
    private void DisplayUserActions()
    {
        Console.WriteLine("Select a user action: ");
        foreach ((int i, string a) in actions) Console.WriteLine($"{i}: {a}");
    }

    private void DisplayTargetCharacters()
    {
        int idx = 1;
        foreach (Character.ICharacter c in MainParty.Members)
        {
            Console.WriteLine($"    {idx}: {c.Name} {(c.Dead ? "(Dead) " : $"({c.HP}/{c.MaxHP} Health) ")}(My Party)");
            idx += 1;
        }
        foreach (Character.ICharacter c in EnemyParty.Members)
        {
            Console.WriteLine($"    {idx}: {c.Name} {(c.Dead ? "(Dead) " : $"({c.HP}/{c.MaxHP} Health) ")}(Enemy Party)");
            idx += 1;
        }
    }

    private static void DisplayItems(List<Inventory.Item> items)
    {
        int idx = 1;
        foreach (Inventory.Item item in items)
        {
            Console.WriteLine($"    {idx}: {item.Name}");
            idx += 1;
        }
    }

    private static Inventory.Item? GetUserInputItem(string? input, List<Inventory.Item> items)
    {
        if (int.TryParse(input, out int value) && value <= items.Count)
        {
            Inventory.Item chosenItem = items[value - 1];
            items.RemoveAt(value - 1);
            return chosenItem;
        }
        return null;
    }

    public enum Action { Pass, Attack, UseItem }
}

