using System.Runtime.InteropServices.ComTypes;
using Shouldly;
using day4;
using Xunit;
namespace day4 {
	public class BingoTests {
		[Fact]
		public void WinningGridWinsOnRow() {
			var bingo = new BingoBoard(" 1 2 3 4 5\n6 7  8 9   10  \n11 12 13 14   15\n16 17  18 19  20\n21  22 23 24 25  ");
			bingo.Winning.ShouldBe(false);
			bingo.Mark(1);
			bingo.Mark(2);
			bingo.Mark(3);
			bingo.Mark(4);
			bingo.Winning.ShouldBe(false);
			bingo.Mark(5);
			bingo.Winning.ShouldBe(true);
		}
		[Fact]
		public void WinningGridWinsOnColumn() {
			var bingo = new BingoBoard(" 1 2 3 4 5\n6 7  8 9   10  \n11 12 13 14   15\n16 17  18 19  20\n21  22 23 24 25  ");
			bingo.Winning.ShouldBe(false);
			bingo.Mark(1);
			bingo.Mark(6);
			bingo.Mark(11);
			bingo.Mark(16);
			bingo.Winning.ShouldBe(false);
			bingo.Mark(21);
			bingo.Winning.ShouldBe(true);
		}

		[Fact]
		public void ScoreWorks() {
			var bingo = new BingoBoard("1 2\n3 4");
			bingo.Score.ShouldBe(10);
			bingo.Mark(1);
			bingo.Score.ShouldBe(9);
			bingo.Mark(2);
			bingo.Score.ShouldBe(7);
			bingo.Mark(4);
			bingo.Score.ShouldBe(3);
		}

	}
}