using NFluent;
using NUnit.Framework;

namespace TriviaKata;

public class TriviaShould
{
    [Test]
    public void Players_has_been_added()
    {
        var player1 = "player1";
        var player2 = "player2";
        var game = new Game();
        game.add(player1);
        game.add(player2);

        Check.That(game.GetCurrentPlayer()).IsEqualTo(player1);
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
    public void Current_category_is_science_when_when_arrive_on_place_1(int dice, string categoryName)
    {
        var player = "player1";
        var game = new Game();
        game.add(player);
        game.roll(dice);

        Check.That(game.currentCategory()).IsEqualTo(categoryName);
    }
    
    [Test]
    public void Could_not_play_when_only_on_player_has_been_added()
    {
        var player = "player1";
        var game = new Game();
        game.add(player);

        Check.That(game.isPlayable()).IsEqualTo(false);
    }

    [Test]
    public void Player_arrives_in_penalty_box_when_answer_is_wrong()
    {
        var player1 = "player1";
        var player2 = "player2";
        var game = new Game();
        game.add(player1);
        game.add(player2);
        game.roll(1);
        game.wrongAnswer();

        Check.That(game.IsInPenaltyBox(player1)).IsEqualTo(true);
    }
    
    [Test]
    public void Player_is_still_in_penalty_box_when_dice_result_is_not_even()
    {
        var player1 = "player1";
        var player2 = "player2";
        var game = new Game();
        game.add(player1);
        game.add(player2);
        game.roll(1);
        game.wrongAnswer();
        game.roll(5);
        game.wasCorrectlyAnswered();
        game.roll(3);
        
        Check.That(game.IsInPenaltyBox(player1)).IsEqualTo(true);
    }
    
    [Ignore("buggy functionality")]
    public void Player_get_out_penalty_box_when_dice_result_is_not_odd()
    {
        var player1 = "player1";
        var player2 = "player2";
        var game = new Game();
        game.add(player1);
        game.add(player2);
        game.roll(1);
        game.wrongAnswer();
        game.roll(5);
        game.wasCorrectlyAnswered();
        game.roll(2);
        
        Check.That(game.IsInPenaltyBox(player1)).IsEqualTo(false);
    }
}