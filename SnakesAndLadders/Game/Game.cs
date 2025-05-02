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

    public void DisplayPreMoveInfo(){
        Player? currentPlayer = Players
            .OrderBy(p => p.PlayerID)
            .FirstOrDefault(p => p.State == PlayerState.HasExtraTurn ||
                                p.State == PlayerState.LostTurn || 
                                p.State == PlayerState.None);
        if (currentPlayer is null){
            if (Players.All(p => p.State == PlayerState.HasWon))
            {
                Console.WriteLine("Game over! Everyone has won.");
            } 
            else
            {
                throw new IndexOutOfRangeException
                (
                    "There are no players that has a chance to play the list. Game might not be setup properly"
                );
            }
        }
                                

        switch(currentPlayer.State){
            case PlayerState.LostTurn:
                Console.WriteLine($"{currentPlayer.FullName} Loses turn and cannot play");
                currentPlayer.State = PlayerState.HasMoved;
                return;
            case PlayerState.HasExtraTurn:
                Console.WriteLine($"It's {currentPlayer.FullName}'s turn again!\nRoll the dice to move!");
                CurrentState.CurrentPlayer = currentPlayer;
                break;
            case PlayerState.None:
                Console.WriteLine($"It's {currentPlayer.FullName}'s turn!\nRoll the dice to move!");
                CurrentState.CurrentPlayer = currentPlayer;
                break;
        }

    }

    public void GetDiceRoll(){
        CurrentState.DiceRollResult = Dice.Roll(DiceAmount);
        Console.WriteLine($"Dice Rolled! Result: {CurrentState.DiceRollResult}");
    }

    public void PreviewTurn(){
        Player player = CurrentState.CurrentPlayer ?? throw new NullReferenceException("Cannot obtain current player to preview");
        int tentative = GameBoard.PreviewMove(player, CurrentState.DiceRollResult);
    }

    public void FinalizeTurn(){
        Player player = CurrentState.CurrentPlayer ?? throw new NullReferenceException("Cannot obtain current player to process");
        int tentative = player.Position + CurrentState.DiceRollResult;
        player = GameBoard.ApplyMove(player, tentative);

        
    }
}

