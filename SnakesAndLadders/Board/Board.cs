using System.ComponentModel.DataAnnotations;

public class Board
{
    private IList<Tile> TileList;

    public int Width {get; set;}
    public int Length {get; set;}
    public int FinalTile => Width * Length;

    public Board(List<Tile> tileList){
        TileList = tileList;
    }

    public int PreviewMove(Player player, int amountOfMoves){
        int currentPosition = player.Position;
        int tentative = currentPosition + amountOfMoves;

        //Bounce-back mechanism
        if (tentative > FinalTile)
        {
            // Bounce back
            Console.WriteLine("Bounced Back!");
            int overflow = tentative - FinalTile;
            tentative = FinalTile - overflow;
        }

        Console.WriteLine($"Player now occupies tile {tentative}");
        return tentative;
    }

    public Player ApplyMove(Player player, int amountOfMoves){
        int currentPosition = player.Position;
        int tentative = PreviewMove(player, amountOfMoves);
        Tile tile = TileList.FirstOrDefault(t => t.Position == tentative) 
                    ?? throw new IndexOutOfRangeException
                    (
                        $"Cannot find a tile with ID {tentative}." + 
                        "Possible causes: input is bigger than the board size, or input is negative"
                    );
        return tile.ApplyEffect(player);
    }
}