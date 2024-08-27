namespace TriviaKata;

public class Questions
{
    private readonly Dictionary<string, Stack<string>> _questions = new()
    {
        ["Pop"] = new Stack<string>(),
        ["Science"] = new Stack<string>(),
        ["Sports"] = new Stack<string>(),
        ["Rock"] = new Stack<string>()
    };

    public Questions()
    {
        for (var i = 0; i < 50; i++)
        {
            _questions["Pop"].Push("Pop Question " + i);
            _questions["Science"].Push("Science Question " + i);
            _questions["Sports"].Push("Sports Question " + i);
            _questions["Rock"].Push("Rock Question " + i);
        }
    }

    public Stack<string> this[string category] => _questions[category];

    public string getCategoryFromIndex(int index)
    {
        return _questions.Keys.ToArray()[index];
    }
}