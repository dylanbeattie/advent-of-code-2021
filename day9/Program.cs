// See https://aka.ms/new-console-template for more information
var readings = File.ReadAllLines("input.txt")
    .Select(line => line.Select(c => c & 15).ToArray()).ToArray();

var rowMax = readings.Length - 1;
var colMax = readings[0].Length - 1;
var risk = 0;
for (var row = 0; row < readings.Length; row++) {
    for (var col = 0; col < readings[0].Length; col++) {
        var cell = readings[row][col];
        if (row > 0 && readings[row - 1][col] <= cell) continue;
        if (col > 0 && readings[row][col - 1] <= cell) continue;
        if (row < rowMax && readings[row + 1][col] <= cell) continue;
        if (col < colMax && readings[row][col + 1] <= cell) continue;
        risk += cell + 1;
    }
}
Console.WriteLine($"Solution to part 1: {risk}");

var basins = new int[readings.Length, readings[0].Length];
var basin = 0;
var nextBasin = 1;
for (var row = 0; row < readings.Length; row++) {
    for (var col = 0; col < readings[0].Length; col++) {
        if (readings[row][col] == 9) basins[row, col] = -1;
    }
}

int grow(int[][] readings, int[,] basins, int row, int col, int label, List<(int, int)> visited) {
    if (row < 0 || col < 0 || row >= readings.Length || col >= readings[row].Length) return label;
    if (basins[row, col] > 0) return label;
    if (visited.Contains((row, col))) return label;
    visited.Add((row, col));
    if (readings[row][col] == 9) {
        basins[row, col] = -1;
        return label;
    }

    grow(readings, basins, row - 1, col, label, visited);
    grow(readings, basins, row + 1, col, label, visited);
    grow(readings, basins, row, col - 1, label, visited);
    grow(readings, basins, row, col + 1, label, visited);
    basins[row, col] = label;
    return label + 1;
}

var label = 1;
for (var row = 0; row < readings.Length; row++) {
    for (var col = 0; col < readings[0].Length; col++) {
        label = grow(readings, basins, row, col, label, new List<(int, int)>());
    }
}

var counts = new List<int>();
for (var row = 0; row < basins.GetLength(0); row++) {
    for (var col = 0; col < basins.GetLength(1); col++) counts.Add(basins[row, col]);
}

var product = counts
    .GroupBy(f => f)
    .Where(g => g.Key > 0)
    .OrderByDescending(g => g.Count())
    .Take(3)
    .Select(g => g.Count())
    .Aggregate(1, (a, b) => a * b);

Console.WriteLine($"Solution to part 2: {product}");