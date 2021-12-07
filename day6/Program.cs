var input = File.ReadAllText("input.txt").Split(',')
    .GroupBy(Int32.Parse)
    .ToDictionary(group => group.Key, group => group.LongCount());

Dictionary<int, long> Evolve(Dictionary<int, long> fish) {
    var nextGeneration = Enumerable.Range(0, 9).ToDictionary(i => i, _ => 0L);
    foreach (var age in fish.Keys) nextGeneration[age > 0 ? age - 1 : 6] += fish[age];
    if (fish.TryGetValue(0, out var babyFish)) nextGeneration[8] += babyFish;
    return nextGeneration;
}

long Solve(Dictionary<int, long> fish, int days) {
    for (var i = 0; i < days; i++) fish = Evolve(fish);
    return fish.Sum(pair => pair.Value);
}

Console.WriteLine($"Part 1: {Solve(input, 80)}");
Console.WriteLine($"Part 2: {Solve(input, 256)}");

