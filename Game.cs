namespace TriviaKata;

public class Game
{
    private readonly Board _board = new();
    private readonly List<Player> _players = [];
    private readonly Questions _questions = new();
    private int _currentPlayerIndex;
    private Player CurrentPlayer => _players[_currentPlayerIndex];

    public PenaltyBox PenaltyBox { get; } = new();

    public bool IsPlayable()
    {
        return HowManyPlayers() >= 2;
    }

    public void Add(Player player)
    {
        _players.Add(player);

        Log.AddPlayer(player.Name, _players.Count);
    }

    private int HowManyPlayers()
    {
        return _players.Count;
    }

    public void Roll(int dice)
    {
        Log.RollDice(dice, CurrentPlayer.Name);

        if (AttemptToGetOutOfPenaltyBox(dice) is FailedToGetOutOfPenaltyBox)
        {
            NextPlayer();
            return;
        }

        _board.MovePlayer(dice, _currentPlayerIndex);
        Log.MovePlayerToNextLocation(CurrentPlayer.Name, _board.GetPlayerPlace(_currentPlayerIndex),
            CurrentCategory());
        AskQuestion();
    }

    private IAttemptToGetOutOfPenaltyBox AttemptToGetOutOfPenaltyBox(int dice)
    {
        return PenaltyBox.IsIn(CurrentPlayer) && !PenaltyBox.ShouldPlayerGetOutPenaltyBox(dice, CurrentPlayer)
            ? new FailedToGetOutOfPenaltyBox()
            : new SucceedToGetOutOfPenaltyBox();
    }

    private void AskQuestion()
    {
        Log.AskQuestion(_questions[CurrentCategory()].Pop());
    }

    public string CurrentCategory()
    {
        return _questions.getCategoryFromIndex(_board.GetPlayerPlace(_currentPlayerIndex) % 4);
    }

    public bool WrongAnswer()
    {
        Log.AnswerIsWrong(CurrentPlayer.Name);
        PenaltyBox.PutIn(CurrentPlayer);

        NextPlayer();
        return true;
    }

    public bool CorrectAnswer()
    {
        WinPurse();

        var didPlayerNotWon = CurrentPlayer.DidPlayerNotWin();
        if (didPlayerNotWon) NextPlayer();

        return didPlayerNotWon;
    }

    private void WinPurse()
    {
        CurrentPlayer.Purse++;
        Log.AnswerIsCorrect(CurrentPlayer.Name, CurrentPlayer.Purse);
    }

    private void NextPlayer()
    {
        _currentPlayerIndex++;
        if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
    }

    public string GetCurrentPlayerName()
    {
        return CurrentPlayer.Name;
    }

    public int GetPurses(Player player)
    {
        return _players[GetIndexOfPlayer(player)].Purse;
    }

    public int GetPlace(Player player)
    {
        return _board.GetPlayerPlace(GetIndexOfPlayer(player));
    }

    private int GetIndexOfPlayer(Player player)
    {
        var playerExists = _players.Contains(player);
        if (!playerExists)
            Console.WriteLine("Player doesn't exist");
        return _players.IndexOf(player);
    }
}