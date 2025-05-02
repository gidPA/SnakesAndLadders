public enum InitialGameSignal{
    Continue,
    Skipped,
    GameOver
}

public class Game{
    private IList<Player> Players;
    private Board GameBoard;
    private GameState CurrentState;
    int DiceAmount {get; set;} = 1;

    public Game(Board board, IList<Player> players, GameState? gameState = null){
        if (gameState is not null){
            CurrentState = gameState;
        } else{
            CurrentState = new GameState();
        }

        GameBoard = board;
        Players = players;
    }

    public InitialGameSignal DisplayPreMoveInfo(){
        Console.WriteLine("\n");

        foreach(Player player in Players){
            Console.WriteLine($"{player.FullName} is currently on tile {player.Position}");
        }

        Console.WriteLine("\n");

        if (Players.All(p => p.State == PlayerState.HasWon))
        {
            Console.WriteLine("Game over! Everyone has won.");
            return InitialGameSignal.GameOver;
        } else if (Players.Where(p => p.State != PlayerState.HasWon).All(p => p.State == PlayerState.HasMoved)){
            foreach(Player player in Players){
                player.State = PlayerState.None;
            }
        }

        Player? currentPlayer = Players
            .OrderBy(p => p.PlayerID)
            .FirstOrDefault(p => p.State == PlayerState.HasExtraTurn ||
                                p.State == PlayerState.LostTurn || 
                                p.State == PlayerState.None)
            ?? throw new IndexOutOfRangeException
            (
                "There are no players that has a chance to play the list. Game might not be setup properly"
            );

        switch(currentPlayer!.State){
            case PlayerState.LostTurn:
                Console.WriteLine($"{currentPlayer.FullName} Loses turn and cannot play");
                currentPlayer.State = PlayerState.HasMoved;
                return InitialGameSignal.Skipped;
            case PlayerState.HasExtraTurn:
                Console.WriteLine($"It's {currentPlayer.FullName}'s turn again!\nRoll the dice to move!");
                currentPlayer.State = PlayerState.HasMoved;
                CurrentState.CurrentPlayer = currentPlayer;
                break;
            case PlayerState.None:
                Console.WriteLine($"It's {currentPlayer.FullName}'s turn!\nRoll the dice to move!");
                CurrentState.CurrentPlayer = currentPlayer;
                break;
        }
        return InitialGameSignal.Continue;

    }

    public void GetDiceRoll(){
        Player player = CurrentState.CurrentPlayer ?? throw new NullReferenceException("Cannot obtain current player to preview");
        player.DiceRollResult = Dice.Roll(DiceAmount);
        Console.WriteLine($"Dice Rolled! Result: {player.DiceRollResult}");
    }

    public void PreviewTurn(){
        Player player = CurrentState.CurrentPlayer ?? throw new NullReferenceException("Cannot obtain current player to preview");
        int tentative = GameBoard.PreviewMove(player);
        Console.WriteLine($"{player.FullName} now occupies tile {tentative}");
    }

    public void FinalizeTurn(){
        Player player = CurrentState.CurrentPlayer ?? throw new NullReferenceException("Cannot obtain current player to process");
        player = GameBoard.ApplyMove(player);
    }

    public Player? GetCurrentPlayer(){
        return CurrentState.CurrentPlayer;
    }

    public Player GetPlayerByName(string name){
        Player player = Players.First(p => p.Name == name);
        return player;
    }

    public Player GetPlayerById(int id){
        Player player = Players.First(p => p.PlayerID == id);
        return player;
    }
}

