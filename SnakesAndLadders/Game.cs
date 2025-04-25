public class Player{
    public string Name {get; set;} = "John Doe";
    public int Position {get; set;} = 0;
    public bool CanMove {get; set;} = true;
    public bool MoveInReverse {get; set;} = false;
    public bool GotExtraTurn = false;

    public int RollDice(){
        return 1;
    }

    public void ResetEffect(){
        CanMove = true;
        MoveInReverse = true;
    }
}

class Game{
    private IList<Player> Players = new List<Player>(){
        new Player(),
        new Player(),
    };

    private static List<Tile> TileList = new List<Tile>(){
        new Tile(),
        new Tile(),
        new Tile(new TileEffect{EffectName = "Ladder", NewPosition = 8}),
        new Tile(),
        new Tile(),
        new Tile(),
        new Tile(),
        new Tile(new TileEffect{EffectName = "Snake", NewPosition = 1}),
        new Tile(),
        new Tile(),
        new Tile(),
    };
    private Board GameBoard = new Board(TileList, 10, 1);
    private bool IsGameOver = false;

    public void GameLoop(){

        while(!IsGameOver){
            for (int i = 0; i < Players.Count; i++){
                Console.WriteLine("\nPlayer {0} - Current Position = {1}", i, Players[i].Position);
                Console.Write("Press enter to roll dice: ");
                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                int diceSteps = Dice.GetSteps();
                Console.WriteLine("\nDice Roll: Player {0} got {1}", i, diceSteps);
                Players[i] = GameBoard.ApplyMove(Players[i], diceSteps);
                Console.WriteLine("Player {0} now steps on tile {1}", i, Players[i].Position);

                IsGameOver = GameBoard.IsWinningPosition(Players[i].Position);
                
                if (IsGameOver){
                    Console.WriteLine("VICTORY! Player {0} wins!", i);
                    break;
                }
            }
        }
    }

}

class Dice {
    public static int GetSteps(){
        Random random = new Random();
        return random.Next(1, 7);
    }
}