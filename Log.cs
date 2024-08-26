namespace TriviaKata;

public class Log
{
    public static void AddPlayer(string playerName, int nbPlayers)
    {
        Console.WriteLine(playerName + " was added");
        Console.WriteLine("They are player number " + nbPlayers);
    }

    public static void RollDice(int dice, string currentPlayer)
    {
        Console.WriteLine(currentPlayer + " is the current player");
        Console.WriteLine("They have rolled a " + dice);
    }

    public static void PlayerIsGettingOutPenaltyBox(string playerName)
    {
        Console.WriteLine(playerName + " is getting out of the penalty box");
    }

    public static void PlayerIsNotGettingOutPenaltyBox(string playerName)
    {
        Console.WriteLine(playerName + " is not getting out of the penalty box");
    }

    public static void MovePlayerToNextLocation(string playerName, int newLocation, string currentCategory)
    {
        Console.WriteLine(playerName
                          + "'s new location is "
                          + newLocation);
        Console.WriteLine("The category is " + currentCategory);
    }

    public static void AskQuestion(string question)
    {
        Console.WriteLine(question);
    }

    public static void AnswerIsCorrect(string playerName, int playerPurses)
    {
        Console.WriteLine("Answer was correct!!!!");
        Console.WriteLine(playerName
                          + " now has "
                          + playerPurses
                          + " Gold Coins.");
    }

    public static void AnswerIsWrong(string playerName)
    {
        Console.WriteLine("Question was incorrectly answered");
        Console.WriteLine(playerName + " was sent to the penalty box");
    }
}