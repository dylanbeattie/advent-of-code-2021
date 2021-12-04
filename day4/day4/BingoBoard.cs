using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day4 {
	public class BingoBoard {
		public override string ToString() {
			return String.Join(Environment.NewLine,
				this.rows.Select(row => String.Join(" ", row.Select(cell => cell.Number.ToString().PadLeft(2)))));
		}

		public int Score => this.AllCells.Where(cell => !cell.Marked).Sum(cell => cell.Number);

		public bool Winning {
			get {
				if (rows.Any(row => row.All(cell => cell.Marked))) return (true);
				for (var i = 0; i < rows[0].Length; i++) {
					if (rows.All(row => row[i].Marked)) return (true);
				}

				return (false);
			}
		}

		private Cell[][] rows;

		public BingoBoard(string grid) {
			var lines = grid.Split(Environment.NewLine,
				StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
			List<Cell[]> foo = new List<Cell[]>();
			foreach (var line in lines) {
				var tokens = Regex.Split(line, " +")
					.Select(value => Int32.Parse(value)).ToList();
				var cells = tokens.Select(i => new Cell(i)).ToArray();
				foo.Add(cells);
			}

			rows = foo.ToArray();
		}

		public void Mark(int number) {
			this.AllCells.FirstOrDefault(c => c.Number == number)?.Mark();
		}

		public List<Cell> AllCells => rows.SelectMany(c => c).ToList();
	}

	public class Cell {
		public Cell(int number) => this.Number = number;

		public void Mark() => this.marked = true;
		public int Number { get; private set; }
		private bool marked;
		public bool Marked => marked;
	}
}