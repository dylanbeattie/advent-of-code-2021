using System.Text.RegularExpressions;
using System.Linq;
// See https://aka.ms/new-console-template for more information
var match = Regex.Match(File.ReadAllText("example.txt"), @"target area: x=([-0-9]+)\.\.([-0-9]+), y=([-\d]+)\.\.([-\d]+)");
var extents = match.Groups.Values.Skip(1).Select(m => Int32.Parse(m.Value));

var dyMax = 0;
for (var dx = 0; dx < 100; dx++) {
    for (var dy = 0; dy < 100; dy++) {
        var x = 0, y = 0;
        for (var step = 0; step < 100; step++) {
            if (hit(x, y, extents))
                x += dx;
            y += dy;


        }
    }
}


bool hit(int x, int y, int[] extents) {
    return (extents[0] <= x && x <= extents[1] && extents[2] <= y && y >= extents[3]);
}