public enum PlayerState{
    None,
    HasMoved,
    LostTurn,
    HasExtraTurn,
    HasWon
}

public class Player{
    public int PlayerID {get; set;}
    public string Name {get; set;} = "John Doe";
    public int Position {get; set;} = 0;
    public PlayerState State {get; set;} = PlayerState.None;

    public string FullName => $"{Name} ({PlayerID})";
}