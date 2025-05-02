Game game = new Game(FakeData.FakeBoard, FakeData.FakePlayers);

InitialGameSignal signal = InitialGameSignal.Continue;

while (true)
{
    signal = game.DisplayPreMoveInfo();

    if (signal == InitialGameSignal.Skipped)
    {
        continue;
    }
    else if (signal == InitialGameSignal.GameOver)
    {
        break;
    }

    string inp = Console.ReadLine();
    game.GetDiceRoll();
    //inp = Console.ReadLine();
    game.PreviewTurn();
    //inp = Console.ReadLine();
    game.FinalizeTurn();
}