namespace TriviaKata;

public record PenaltyBox
{
    private List<Player> _players { get; } = new();

    public void PutIn(Player player)
    {
        _players.Add(player);
    }

    public void PutOut(Player player)
    {
        _players.Remove(player);
    }

    public bool IsIn(Player player)
    {
        return _players.Contains(player);
    }

    public bool ShouldPlayerGetOutPenaltyBox(int dice, Player currentPlayer)
    {
        if (dice.PermitGettingOutPenaltyBox())
        {
            PutOut(currentPlayer);
            Log.PlayerIsGettingOutPenaltyBox(currentPlayer.Name);

            return true;
        }

        Log.PlayerIsNotGettingOutPenaltyBox(currentPlayer.Name);
        return false;
    }
}