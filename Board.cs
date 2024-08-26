namespace TriviaKata;

public class Board
{
    private readonly int[] _places = new int[6];
    
    public int GetPlayerPlace(int playerIndex)
    {
        return _places[playerIndex];
    }
    
    public void MovePlayer(int dice, int playerIndex)
    {
        _places[playerIndex] += dice;
        if (_places[playerIndex] > 11) _places[playerIndex] -= 12;
    }
}