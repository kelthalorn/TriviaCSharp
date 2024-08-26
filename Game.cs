namespace TriviaKata;

public class Game
{
    private int _currentPlayerIndex;
    private Player CurrentPlayer => _players[_currentPlayerIndex];
    private readonly List<Player> _players = [];

    public PenaltyBox PenaltyBox { get; } = new();
    private readonly Questions _questions = new();

    private readonly Board _board = new();
    
    public bool IsPlayable()
    {
        return HowManyPlayers() >= 2;
    }

    public bool Add(Player player)
    {
        _players.Add(player);

        Log.AddPlayer(player.Name, _players.Count);
        return true;
    }

    private int HowManyPlayers()
    {
        return _players.Count;
    }

    public void Roll(int dice)
    {
        Log.RollDice(dice, CurrentPlayer.Name);

        if (PenaltyBox.IsIn(CurrentPlayer))
        {
            if (dice.PermitGettingOutPenaltyBox())
            {
                PenaltyBox.PutOut(CurrentPlayer);

                Log.PlayerIsGettingOutPenaltyBox(CurrentPlayer.Name);

                _board.MovePlayer(dice, _currentPlayerIndex);
                Log.MovePlayerToNextLocation(CurrentPlayer.Name, _board.GetPlayerPlace(_currentPlayerIndex), CurrentCategory());
                AskQuestion();
            }
            else
            {
                Log.PlayerIsNotGettingOutPenaltyBox(CurrentPlayer.Name);
                NextPlayer();
            }
        }
        else
        {
            _board.MovePlayer(dice, _currentPlayerIndex);
            Log.MovePlayerToNextLocation(CurrentPlayer.Name, _board.GetPlayerPlace(_currentPlayerIndex), CurrentCategory());
            AskQuestion();
        }
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
        if (didPlayerNotWon)
        {
            NextPlayer();    
        }
        
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