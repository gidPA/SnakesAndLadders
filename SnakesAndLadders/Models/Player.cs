public enum PlayerState{
    None,
    HasMoved,
    LostTurn,
    HasExtraTurn,
    HasWon
}

public class Player{
    public Player(int playerId, string name)
    {
        PlayerID = playerId;
        Name = name;
    }
    public int PlayerID {get; set;}
    public string Name {get; set;} = "John Doe";
    public int Position {get; set;} = 1;
    public int DiceRollResult = 0;
    public PlayerState State {get; set;} = PlayerState.None;

    public string FullName => $"{Name} ({PlayerID})";
}