public static class FakeData
{
    public static IList<Player> FakePlayers {get; set;}= new List<Player>
    {
        new Player(1, "Alice"),
        new Player(2, "Bob"),
        new Player(3, "Kate")
    };

    public static Board FakeBoard {get; set;}= new (
        new List<Tile>
        {
            new PlainTile(1),
            new PlainTile(2),
            new TeleportTile(3, 11),
            new PlainTile(4),
            new PlainTile(5),
            new PlainTile(6),
            new ExtraTurnTile(7),
            new PlainTile(8),
            new PlainTile(9),
            new TeleportTile(10, 4),
            new PlainTile(11),
            new TeleportTile(12, 19),
            new PlainTile(13),
            new PlainTile(14),
            new LoseTurnTile(15),
            new PlainTile(16),
            new PlainTile(17),
            new TeleportTile(18, 9),
            new PlainTile(19),
            new WinnerTile(20),
        },
        10,
        2
    );
}