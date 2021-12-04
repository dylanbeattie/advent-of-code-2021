var inputs = File.ReadAllLines("input.txt").Select(Int32.Parse).ToArray();

var numberIncreases = 0;
for(var i = 1; i < inputs.Length; i++) {
    if (inputs[i] > inputs[i-1]) numberIncreases++;
}

Console.WriteLine($"Solution to part 1: {numberIncreases}");

const int WINDOW_SIZE = 3;
var lastWindow = inputs.Take(WINDOW_SIZE).Sum();
var windowIncreases = 0;

for (var i = 1; i < inputs.Length; i++) {
    var thisWindow = inputs.Skip(i).Take(WINDOW_SIZE).Sum();
    if (thisWindow > lastWindow) windowIncreases++;
    lastWindow = thisWindow;
}

Console.WriteLine($"Solution to part 2: {windowIncreases}");

