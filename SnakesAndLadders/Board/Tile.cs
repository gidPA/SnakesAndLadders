enum TeleportType{
    Snake,
    Ladder
}

public abstract class Tile{
    public Tile(int position)
    {
        Position = position;
    }
    public int Position {get; set;}
    public abstract Player ApplyEffect(Player player);
}

public class PlainTile : Tile {
    public PlainTile(int position) : base(position) {}

    public override Player ApplyEffect(Player player) {
        player.State = PlayerState.HasMoved;
        return player;
    }
}

public class TeleportTile : Tile {
    public TeleportTile(int position, int destination) : base(position)
    {
        Destination = destination;
        teleportType = destination > position ? TeleportType.Ladder : TeleportType.Snake;
    }

    private int Destination;
    private TeleportType teleportType;

    public override Player ApplyEffect(Player player) {
        if(teleportType == TeleportType.Ladder){
            Console.WriteLine($"{player.FullName} has got lucky to step in a Ladder Tile! Now {player.FullName} occupies tile {Destination}");
        } else {
            Console.WriteLine($"Oh no! {player.FullName} stepped on a Snake tile! Now {player.FullName} occupies tile {Destination}");
        }
        
        player.Position = Destination;
        player.State = PlayerState.HasMoved;
        return player;
    }
}

public class ExtraTurnTile : Tile {
    public ExtraTurnTile(int position) : base(position) {}

    public override Player ApplyEffect(Player player) {
        Console.WriteLine($"Nice! {player.FullName} has got one extra turn!");

        player.State = PlayerState.HasExtraTurn;
        return player;
    }
}

public class LoseTurnTile : Tile {
    public LoseTurnTile(int position) : base(position) {}

    public override Player ApplyEffect(Player player) {
        Console.WriteLine($"How unfortunate! {player.FullName} cannot move in their next turn!");
        player.State = PlayerState.LostTurn;
        return player;
    }
}

public class WinnerTile : Tile {
    public WinnerTile(int position) : base(position) {}

    public override Player ApplyEffect(Player player) {
        Console.WriteLine($"Congratulations! {player.FullName} has stepped on the final tile and is now a champion!");
        player.State = PlayerState.HasWon;
        return player;
    }
}
