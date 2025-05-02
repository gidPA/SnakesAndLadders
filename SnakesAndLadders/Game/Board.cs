using System.ComponentModel.DataAnnotations;

public class Board
{
    private IList<Tile> TileList;

    public int Width {get; set;}
    public int Length {get; set;}
    public int FinalTile => Width * Length;

    public Board(List<Tile> tileList, int width, int length){
        TileList = tileList;
        Width = width;
        Length = length;
    }

    public int PreviewMove(Player player){
        int currentPosition = player.Position;
        int tentative = currentPosition + player.DiceRollResult;

        //Bounce-back mechanism
        if (tentative > FinalTile)
        {
            // Bounce back
            Console.WriteLine("Bounced Back!");
            int overflow = tentative - FinalTile;
            tentative = FinalTile - overflow;
        }
        return tentative;
    }

    public Player ApplyMove(Player player){
        int currentPosition = player.Position;
        player.Position = PreviewMove(player);
        Tile tile = TileList.FirstOrDefault(t => t.Position == player.Position) 
                    ?? throw new IndexOutOfRangeException
                    (
                        $"Cannot find a tile with ID {player.Position}." + 
                        "Possible causes: input is bigger than the board size, or input is negative"
                    );
        // Player affectedPlayer = tile.ApplyEffect(player);
        // affectedPlayer.State = PlayerState.None;

        return tile.ApplyEffect(player);
    }
}