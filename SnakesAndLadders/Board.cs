class Board
{
    private int Width;
    private int Height;
    private int finalTile;
    private List<Tile> tiles;

    public Board(List<Tile> tiles, int width = 10, int height = 10)
    {
        this.tiles = tiles;
        this.Width = width;
        this.Height = height;
        this.finalTile = Width * Height - 1;
    }

    public Player ApplyMove(Player player, int steps)
    {
        int tentative;
        if (player.MoveInReverse){
            tentative = player.Position - steps;
        } else {
            tentative = player.Position + steps;
        }

        if (tentative > finalTile)
        {
            // Bounce back
            int overflow = tentative - finalTile;
            tentative = finalTile - overflow;
        }

        if (tentative < 0)
        {
            // Rebounce forward
            tentative = 0 - tentative;
        }

        

        TileEffect? effect = tiles[tentative].ApplyEffect(player);

        if (effect is null){
            player.Position = tentative;
            return player;
        } else {
            Console.WriteLine("\nPlayer stepped on trap tile: {0}", effect.EffectName);
        }
        if (effect.NewPosition is int newPosition){
            player.Position = newPosition;
        } else if (effect.SkipNextTurn){
            player.CanMove = false;
        } else if (effect.ExtraTurn){
            player.GotExtraTurn = true;
        } else if (effect.MoveInReverse){
            player.MoveInReverse = true;
        }


        return player;
    }

    public bool IsWinningPosition(int position) => position == finalTile;
}

static class BoardFactory{
    // public static List<ITile> LoadFromFile(){

    // }
}