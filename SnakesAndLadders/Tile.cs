public class TileEffect
{
    public string? EffectName = "";
    public int? NewPosition { get; set; } // null = no change
    public bool SkipNextTurn { get; set; } = false;
    public bool ExtraTurn { get; set; } = false;
    public bool MoveInReverse {get; set;} = false;
}

public class Tile{
    private TileEffect? Effect;

    public Tile(TileEffect? effect = null){
        Effect = effect;
    }

    public TileEffect? ApplyEffect(Player player){
        return Effect;
    }
}


// interface ITileWithEffect{
//     public int Destination {get; set;}
//     public int ApplyEffect();
// }

// class PlainTile : ITile{
//     public bool ShouldTriggerEffect(){
//         return false;
//     }
// }
// class SnakeTile : ITile, ITileWithEffect{
//     public int Destination {get; set;}
//     public string EffectName = "as";
//     public bool ShouldTriggerEffect(){
//         return true;
//     }
//     public int ApplyEffect(){
//         return Destination;
//     }
// }

// class LadderTile : ITile, ITileWithEffect{
//     public int Destination {get; set;}
//     public string EffectName = "Ladder";
//     public bool ShouldTriggerEffect(){
//         return true;
//     }
//     public int ApplyEffect(){
//         return Destination;
//     }
// }