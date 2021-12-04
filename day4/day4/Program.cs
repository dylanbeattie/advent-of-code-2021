using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using day4;

var inputs = File.ReadAllText("input.txt").Split(Environment.NewLine + Environment.NewLine);
var draws = inputs.First().Split(',').Select(Int32.Parse).ToList();
var grids = inputs.Skip(1).Select(grid => new BingoBoard(grid)).ToList();

int Part1(List<BingoBoard> grids) {
    foreach(var draw in draws) {
        foreach(var grid in grids) grid.Mark(draw);
        var winner = grids.FirstOrDefault(grid => grid.Winning);
        if (winner != default) return draw * winner.Score;
    }
    return -1;
}

int Part2(IEnumerable<BingoBoard> grids) {
    List<(BingoBoard, int)> winners = new List<(BingoBoard, int)>();
    foreach(var draw in draws) {
        var sweep = grids.ToList();
        foreach(var grid in sweep) grid.Mark(draw);
        winners.AddRange(sweep.Where(grid => grid.Winning).Select(grid => (grid, draw)));
        grids = sweep.Where(grid => !grid.Winning);
    }
    var winner = winners.Last();
    return winner.Item1.Score * winner.Item2;
}

Console.WriteLine(Part1(grids));
Console.WriteLine(Part2(grids));