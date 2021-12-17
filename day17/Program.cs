using System.Text.RegularExpressions;
using System.Linq;
// See https://aka.ms/new-console-template for more information
var match = Regex.Match(File.ReadAllText("input.txt"), @"target area: x=([-0-9]+)\.\.([-0-9]+), y=([-\d]+)\.\.([-\d]+)");
var extents = match.Groups.Values.Skip(1).Select(m => Int32.Parse(m.Value)).ToArray();

List<int> solvePart1(int[] extents) {
    List<int> yMaxes = new List<int>();
    var dyMax = 0;
    for (var dxInitial = -1000; dxInitial < 1000; dxInitial++) {
        Console.Write(".");
        for (var dyInitial = -1000; dyInitial < 1000; dyInitial++) {
            // Console.WriteLine($"Testing ({dxInitial},{dyInitial}):");
            int x = 0, y = 0, dx = dxInitial, dy = dyInitial;
            int yMax = 0;
            for (var step = 0; step < 1000; step++) {
                yMax = Math.Max(y, yMax);
                if (hit(x, y, extents)) {
                    Console.WriteLine($"Scored a hit with ({dxInitial},{dyInitial}) after {step} steps! (yMax: {yMax})");
                    yMaxes.Add(yMax);
                    dyMax = Math.Max(dy, dyMax);
                    break;
                }
                x += dx;
                y += dy;
                if (x > extents[1] || y < extents[2]) break;
                dy -= 1;
                dx += dx switch {
                    > 0 => -1,
                    < 0 => +1,
                    _ => 0
                };
            }
        }
    }
    return yMaxes;
}

var maxima = solvePart1(extents);
var highest = maxima.Max();

Console.WriteLine($"Part 1: {highest}");
Console.WriteLine($"Part 2: {maxima.Count}");

bool hit(int x, int y, int[] extents) {
    return (extents[0] <= x && x <= extents[1] && extents[2] <= y && y <= extents[3]);
}
