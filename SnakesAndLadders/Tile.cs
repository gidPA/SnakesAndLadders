interface ITile{
    public void OnLand();
}

class PlainTile : ITile{
    public void OnLand(){

    }
}
class SnakeTile : ITile{
    private int destination;
    public void OnLand(){

    }
}

class LadderTile : ITile{
    private int destination;
    public void OnLand(){

    }
}