using Value;

namespace TriviaKata;

public class Player(string name):ValueType<Player>
{
    public string Name { get; } = name;
    public int Purse { get; set; }

    protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
    {
        return new object[]
        {
            name
        };
    }

    public bool DidPlayerNotWin()
    {
        return Purse != 6;
    }
}