// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

var movements = File.ReadAllLines("input.txt")
	.Select(line => Movement.Parse(line));
var s = new Submarine();
foreach (var m in movements) s.MovePart2(m);
Console.WriteLine(s);


public class Submarine {
	public override string ToString() {
		return $"{Position}, {Depth} (Position * Depth = {Position * Depth}";
	}

	public int Position { get; set; }
	public int Depth { get; set; }
	public int Aim { get; set; }

	public void MovePart2(Movement movement) {
		
		switch (movement.Direction) {
			case Direction.Up:
				this.Aim -= movement.Magnitude;
				return;
			case Direction.Down:
				this.Aim += movement.Magnitude;
				return;
			case Direction.Forward:
				this.Position += movement.Magnitude;
				this.Depth += Aim * movement.Magnitude;
				return;
			case Direction.Back:
				this.Position -= movement.Magnitude;
				this.Depth -= Aim * movement.Magnitude;
				return;
		}		
	}


	public void MovePart1(Movement movement) {
		switch (movement.Direction) {
			case Direction.Up:
				this.Depth -= movement.Magnitude;
				return;
			case Direction.Down:
				this.Depth += movement.Magnitude;
				return;
			case Direction.Forward:
				this.Position += movement.Magnitude;
				return;
			case Direction.Back:
				this.Position -= movement.Magnitude;
				return;
		}
	}
}

public enum Direction {
	Up,
	Down,
	Forward,
	Back
}

public class Movement {
	public Direction Direction { get; set; }
	public int Magnitude { get; set; }

	public static Movement Parse(string s) {
		var tokens = s.Split(' ');
		var direction = Enum.Parse<Direction>(tokens[0], true);
		var magnitude = Int32.Parse(tokens[1]);
		return new Movement {
			Direction = direction,
			Magnitude = magnitude
		};
	}
}