namespace TriviaKata;

public class Game
{
    private int _currentPlayer;

    private readonly bool[] _inPenaltyBox = new bool[6];
    
    private bool IsPlayerInPenaltyBox => _inPenaltyBox[_currentPlayer];

    private readonly int[] _places = new int[6];
    private int CurrentPlayerPlace => _places[_currentPlayer];

    private readonly List<string> _players = new();
    private string PlayerName => _players[_currentPlayer];
    
    private readonly int[] _purses = new int[6];
    private int PlayerPurses => _purses[_currentPlayer];

    private readonly Dictionary<string, Stack<string>> _questions = new()
    {
        ["Pop"] = new Stack<string>(),
        ["Science"] = new Stack<string>(),
        ["Sports"] = new Stack<string>(),
        ["Rock"] = new Stack<string>()
    };

    public Game()
    {
        for (var i = 0; i < 50; i++)
        {
            _questions["Pop"].Push("Pop Question " + i);
            _questions["Science"].Push("Science Question " + i);
            _questions["Sports"].Push("Sports Question " + i);
            _questions["Rock"].Push("Rock Question " + i);
        }
    }

    public bool IsPlayable()
    {
        return HowManyPlayers() >= 2;
    }

    public bool Add(string playerName)
    {
        _players.Add(playerName);
        _places[HowManyPlayers()] = 0;
        _purses[HowManyPlayers()] = 0;
        _inPenaltyBox[HowManyPlayers()] = false;

        Log.AddPlayer(playerName, _players);
        return true;
    }

    private int HowManyPlayers()
    {
        return _players.Count;
    }

    public void Roll(int dice)
    {
        Log.RollDice(dice, PlayerName);

        if (IsPlayerInPenaltyBox)
        {
            if (dice.PermitGettingOutPenaltyBox())
            {
                PutOutPlayerFromPenaltyBox();

                Log.PlayerIsGettingOutPenaltyBox(PlayerName);

                MovePlayer(dice);
                AskQuestion();
            }
            else
            {
                Log.PlayerIsNotGettingOutPenaltyBox(PlayerName);
                NextPlayer();
            }
        }
        else
        {
            MovePlayer(dice);
            AskQuestion();
        }
    }

    private void MovePlayer(int dice)
    {
        _places[_currentPlayer] += dice;
        if (_places[_currentPlayer] > 11) _places[_currentPlayer] -= 12;

        Log.MovePlayerToNextLocation(PlayerName, CurrentPlayerPlace, CurrentCategory());
    }

    private void AskQuestion()
    {
        var question = _questions[CurrentCategory()].Pop();
        Log.AskQuestion(question);
    }

    public string CurrentCategory()
    {
        var categoryPosition = _places[_currentPlayer] % 4;
        return categoryPosition switch
        {
            0 => "Pop",
            1 => "Science",
            2 => "Sports",
            _ => "Rock"
        };
    }

    public bool WrongAnswer()
    {
        Log.AnswerIsWrong(PlayerName);
        PutPlayerInPenaltyBox();

        NextPlayer();
        return true;
    }

    public bool CorrectAnswer()
    {
        WinPurse();

        var didPlayerNotWon = DidPlayerNotWin();
        if (didPlayerNotWon)
        {
            NextPlayer();    
        }
        
        return didPlayerNotWon;
    }

    private void WinPurse()
    {
        _purses[_currentPlayer]++;
        Log.AnswerIsCorrect(PlayerName, PlayerPurses);
    }

    private void NextPlayer()
    {
        _currentPlayer++;
        if (_currentPlayer == _players.Count) _currentPlayer = 0;
    }

    
    private void PutPlayerInPenaltyBox()
    {
        _inPenaltyBox[_currentPlayer] = true;
    }

    private void PutOutPlayerFromPenaltyBox()
    {
        _inPenaltyBox[_currentPlayer] = false;
    }

    private bool DidPlayerNotWin()
    {
        return _purses[_currentPlayer] != 6;
    }

    public string GetCurrentPlayer()
    {
        return _players[_currentPlayer];
    }

    public bool IsInPenaltyBox(string playerName)
    {
        return _inPenaltyBox[GetIndexOfPlayer(playerName)];
    }
    
    public int GetPurses(string playerName)
    {
        return _purses[GetIndexOfPlayer(playerName)];
    }

    public int GetPlace(string playerName)
    {
        return _places[GetIndexOfPlayer(playerName)];
    }
    
    private int GetIndexOfPlayer(string playerName)
    {
        var indexOfPlayer = _players.IndexOf(playerName);
        if (indexOfPlayer < 0) 
            Console.WriteLine("Player doesn't exist");
        return indexOfPlayer;
    }
}