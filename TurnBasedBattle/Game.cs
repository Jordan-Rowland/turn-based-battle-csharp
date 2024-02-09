class Game
{
    public Party MainParty { get; init; }
    public Party EnemyParty { get; set; }
    public Party[] EnemyParties { get; init; }

    // TODO: Have a better display of character health, etc.

    public Game()
    {
        MainParty = new(
            "Player1",
            new List<Inventory.Item>() {
                new Inventory.HealthPotion(),
                new Inventory.HealthPotion(),
                new Inventory.PhoenixDown(),
                new Inventory.HealthPotion(),
                new Inventory.Poison(),
            },
            new Character.Hero() { Name = "Hiro" },
            new Character.Hero() { Name = "Aya", HP = 5 }
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
        Character.CCharacter character = party.GetCharacter();
        bool isComputer = player.ToLower() == "computer";
        while (true)
        {
            Display.DisplayUserActions(character);
            if (player.ToLower() == "computer") Console.WriteLine();
            else Console.Write(" > ");
            if (isComputer)
            {
                // TODO: Need logic to compute how computer enemy will react
                Random r = new();
                Party opposingParty = party == MainParty ? EnemyParty : MainParty;
                var targetCharacter = opposingParty.Members[r.Next(opposingParty.Members.Length)];
                int damage = character.Attack(targetCharacter);
                Display.DisplayAttackInfo(character, targetCharacter, damage);
            }
            else
            {
                switch (GetUserInputAction(Console.ReadLine()))
                {
                    case Action.Attack:
                        SelectAndAttackCharacter();
                        break;
                    case Action.UseItem:
                        if (!party.Items!.Any()) continue;
                        Inventory.Item? chosenItem = ChoseItem(party);
                        if (chosenItem == null) continue;
                        UseItemOnCharacter(character, chosenItem);
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
            //!! Possibly Use a record and return attack data and print this in the enclosing block instead
            //!! of inside this
            Display.DisplayTargetCharacters(MainParty, EnemyParty);
            Character.CCharacter targetCharacter = GetUserInputTargetCharacter(Console.ReadLine());
            int damage = character.Attack(targetCharacter);
            Display.DisplayAttackInfo(character, targetCharacter, damage);
        }

        static Inventory.Item? ChoseItem(Party party)
        {
            Display.DisplayItems(party.Items!);
            return GetUserInputItem(Console.ReadLine(), party.Items!); ;
        }

        void UseItemOnCharacter(Character.CCharacter character, Inventory.Item? chosenItem)
        {
            Display.DisplayTargetCharacters(MainParty, EnemyParty);
            Character.CCharacter targetCharacter = GetUserInputTargetCharacter(Console.ReadLine(), character);
            targetCharacter.UseItem(chosenItem!);
            Console.WriteLine(
                $"{character.Name} used {chosenItem!.Name} on {targetCharacter.Name}"
            );
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

    private Character.CCharacter GetUserInputTargetCharacter(string? input, Character.CCharacter? character = null)
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
        return character ?? (from m in EnemyParty.Members where !m.Dead select m).ToList()[0];
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

    static class Display
    {
        static readonly Dictionary<int, string> actions = new()
        {
            [1] = "Attack",
            [2] = "Use Item",
            [0] = "Pass",
        };

        public static void DisplayUserActions(Character.CCharacter character)
        {
            Console.WriteLine(
                $"It is {character.Name}'s turn ({character.HP}/{character.MaxHP} Health)..."
            );
            Console.WriteLine("Select a user action: ");
            foreach ((int i, string a) in actions) Console.WriteLine($"{i}: {a}");
        }

        public static void DisplayTargetCharacters(Party mainParty, Party enemyParty)
        {
            int idx = 1;
            foreach (Character.CCharacter c in mainParty.Members)
            {
                Console.WriteLine($"    {idx}: {c.Name} {(c.Dead ? "(Dead) " : $"({c.HP}/{c.MaxHP} Health) ")}(My Party)");
                idx += 1;
            }
            foreach (Character.CCharacter c in enemyParty.Members)
            {
                Console.WriteLine($"    {idx}: {c.Name} {(c.Dead ? "(Dead) " : $"({c.HP}/{c.MaxHP} Health) ")}(Enemy Party)");
                idx += 1;
            }
        }

        public static void DisplayItems(List<Inventory.Item> items)
        {
            int idx = 1;
            foreach (Inventory.Item item in items)
            {
                Console.WriteLine($"    {idx}: {item.Name}");
                idx += 1;
            }
        }

        public static void DisplayAttackInfo(Character.CCharacter character, Character.CCharacter targetCharacter, int damage)
        {
            Console.WriteLine(
                $"{character.Name} used {character.StandardAttack} " +
                $"on {targetCharacter.Name} for {damage} damage"
            );
        }
    }

    public enum Action { Pass, Attack, UseItem }
}

