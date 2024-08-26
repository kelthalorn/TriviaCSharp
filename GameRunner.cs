namespace TriviaKata;

public class GameRunner
{
    private static bool notAWinner;

    
    public static void Main(String[] args)
    {
        Game aGame = new Game();

        aGame.Add(new Player("Chet"));
        aGame.Add(new Player("Pat"));
        aGame.Add(new Player("Sue"));

        Random rand = new Random();

        do
        {

            aGame.Roll(rand.Next(5) + 1);

            if (rand.Next(9) == 7)
            {
                notAWinner = aGame.WrongAnswer();
            }
            else
            {
                notAWinner = aGame.CorrectAnswer();
            }



        } while (notAWinner);

    }
}