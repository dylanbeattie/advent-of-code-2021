// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;

var lines = File.ReadAllLines("input.txt")
	.Where(line => !String.IsNullOrEmpty(line))
	.Select(line => new Line(line));
Console.WriteLine("Solution to part 1: ");
Console.WriteLine(lines
	.Where(l => l.Type == LineType.Horizontal || l.Type == LineType.Vertical)
	.SelectMany(line => line.Points).GroupBy(c => c)
	.Where(group => group.Count() > 1)
	.Count());

Console.WriteLine("Solution to part 2: ");
Console.WriteLine(lines
	.SelectMany(line => line.Points).GroupBy(c => c)
	.Where(group => group.Count() > 1)
	.Count());

public record Point(int X, int Y);

public enum LineType {
	Horizontal,
	Vertical,
	Diagonal
}
public class Line {
	public LineType Type { get; init; }

	readonly string[] separators = { ",", " -> " };
	
	public IEnumerable<Point> Points;

	public Line(string input) {
		var values = input
			.Split(separators, StringSplitOptions.RemoveEmptyEntries)
			.Select(Int32.Parse)
			.ToArray();

		var (x1, y1, x2, y2) = (values[0], values[1], values[2], values[3]);
		if (x1 == x2) {
			Type = LineType.Horizontal;
			Points = Sequence(y1, y2).Select(y => new Point(x1, y));
		} else if (y1 == y2) {
			Type = LineType.Vertical;
			Points = Sequence(x1, x2).Select(x => new Point(x, y1));
		} else {
			Type = LineType.Diagonal;
			Points = Sequence(x1, x2).Zip(Sequence(y1, y2)).Select(tuple => new Point(tuple.First, tuple.Second));
		}
	}

	static IEnumerable<int> Sequence(int a, int b) {
		if (a < b) {
			while (a <= b) yield return a++;
		} else {
			while (a >= b) yield return a--;
		}
	}
}