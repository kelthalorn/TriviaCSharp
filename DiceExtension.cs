namespace TriviaKata;

public static class DiceExtension
{
    public static bool PermitGettingOutPenaltyBox(this int dice)
    {
        return dice % 2 != 0;
    }
}