using NFluent;
using NUnit.Framework;

namespace TriviaKata;

public class TriviaShould
{
    [Test]
    public void Player_has_been_added()
    {
        var player = "player1";
        var game = new Game();
        game.add(player);

        Check.That(game.GetCurrentPlayer()).IsEqualTo(player);
    }

    [TestCase(0, "Pop")]
    [TestCase(1, "Science")]
    [TestCase(2, "Sports")]
    [TestCase(3,"Rock")]
    [TestCase(4,"Pop")]
    [TestCase(5,"Science")]
    [TestCase(6,"Sports")]
    [TestCase(7,"Rock")]
    [TestCase(8,"Pop")]
    [TestCase(9,"Science")]
    [TestCase(10,"Sports")]
    [TestCase(11,"Rock")]
    public void current_category_is_science_when_when_arrive_on_place_1(int dice, string categoryName)
    {
       
        var player = "player1";
        var game = new Game();
        game.add(player);
        game.roll(dice);

        Check.That(game.currentCategory()).IsEqualTo(categoryName);
    }
}