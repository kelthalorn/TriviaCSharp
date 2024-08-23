namespace TriviaKata;

public class Game
{
    private int _currentPlayer;

    private readonly bool[] _inPenaltyBox = new bool[6];
    private bool _isGettingOutOfPenaltyBox;

    private readonly int[] _places = new int[6];


    private readonly List<string> _players = new();
    private readonly int[] _purses = new int[6];

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

        Console.WriteLine(playerName + " was added");
        Console.WriteLine("They are player number " + _players.Count);
        return true;
    }

    public int HowManyPlayers()
    {
        return _players.Count;
    }

    public void Roll(int roll)
    {
        Console.WriteLine(_players[_currentPlayer] + " is the current player");
        Console.WriteLine("They have rolled a " + roll);

        if (_inPenaltyBox[_currentPlayer])
        {
            if (roll % 2 != 0)
            {
                _isGettingOutOfPenaltyBox = true;

                Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");

                MovePlayer(roll);

                AskQuestion();
            }
            else
            {
                Console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                _isGettingOutOfPenaltyBox = false;
            }
        }
        else
        {
            MovePlayer(roll);

            AskQuestion();
        }
    }

    public void MovePlayer(int roll)
    {
        _places[_currentPlayer] += roll;
        if (_places[_currentPlayer] > 11) _places[_currentPlayer] -= 12;

        Console.WriteLine(_players[_currentPlayer]
                          + "'s new location is "
                          + _places[_currentPlayer]);
        Console.WriteLine("The category is " + CurrentCategory());
    }

    private void AskQuestion()
    {
        var question = _questions[CurrentCategory()].Pop();
        Console.WriteLine(question);
    }

    public string CurrentCategory()
    {
        var categoryPosition = _places[_currentPlayer] % 4;
        if (categoryPosition == 0) return "Pop";
        if (categoryPosition == 1) return "Science";
        if (categoryPosition == 2) return "Sports";

        return "Rock";
    }

    public bool WasCorrectlyAnswered()
    {
        var winner = false;
        if (_inPenaltyBox[_currentPlayer])
        {
            if (_isGettingOutOfPenaltyBox)
            {
                Console.WriteLine("Answer was correct!!!!");
                _purses[_currentPlayer]++;
                Console.WriteLine(_players[_currentPlayer]
                                  + " now has "
                                  + _purses[_currentPlayer]
                                  + " Gold Coins.");

                winner = DidPlayerWin();
                NextPlayer();

                return winner;
            }

            NextPlayer();
            return true;
        }

        Console.WriteLine("Answer was corrent!!!!");
        _purses[_currentPlayer]++;
        Console.WriteLine(_players[_currentPlayer]
                          + " now has "
                          + _purses[_currentPlayer]
                          + " Gold Coins.");

        winner = DidPlayerWin();
        NextPlayer();

        return winner;
    }

    public void NextPlayer()
    {
        _currentPlayer++;
        if (_currentPlayer == _players.Count) _currentPlayer = 0;
    }

    public bool WrongAnswer()
    {
        Console.WriteLine("Question was incorrectly answered");
        Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
        _inPenaltyBox[_currentPlayer] = true;

        _currentPlayer++;
        if (_currentPlayer == _players.Count) _currentPlayer = 0;
        return true;
    }

    private bool DidPlayerWin()
    {
        return _purses[_currentPlayer] != 6;
    }

    public string GetCurrentPlayer()
    {
        return _players[_currentPlayer];
    }

    public bool IsInPenaltyBox(string playerName)
    {
        var indexOfPlayer = _players.IndexOf(playerName);
        if (indexOfPlayer < 0) return false;

        return _inPenaltyBox[indexOfPlayer];
    }

    public int GetPurses(string playerName)
    {
        var indexOfPlayer = _players.IndexOf(playerName);
        if (indexOfPlayer < 0) Console.WriteLine("Player doesn't exist");

        return _purses[indexOfPlayer];
    }

    public int GetPlace(string playerName)
    {
        var indexOfPlayer = _players.IndexOf(playerName);
        if (indexOfPlayer < 0) Console.WriteLine("Player doesn't exist");

        return _places[indexOfPlayer];
    }
}