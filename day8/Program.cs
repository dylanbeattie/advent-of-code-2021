var solutions = File.ReadAllLines("input.txt").Select(Solve).ToList();

var totalPart1 = solutions.Aggregate(String.Empty, (s1,s2) => s1 + s2)
    .GroupBy(c => c).Where(group => "1478".Contains(group.Key)).Sum(group => group.Count());

var totalPart2 = solutions.Sum(Int32.Parse);

Console.WriteLine($"solution to part 1: {totalPart1}");
Console.WriteLine($"solution to part 2: {totalPart2}");

string Solve(string line) => new Observation(line).Solve();

class Observation {
    private static readonly List<int> patterns = new() { 119, 36, 93, 109, 46, 107, 123, 37, 127, 111 };
    private readonly Dictionary<char, int> map = new();
    private readonly string[] digits;
    public Observation(string line) {
        var tokens = line.Split('|');
        var inputs = tokens[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        digits = tokens[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var groups = tokens[0].GroupBy(c => c).ToList();
        map.Add(groups.First(g => g.Count() == 4).Key, 4);
        map.Add(groups.First(g => g.Count() == 6).Key, 1);
        map.Add(groups.First(g => g.Count() == 9).Key, 5);
        map.Add(inputs.First(c => c.Length == 2).ToArray().First(c => !map.ContainsKey(c)), 2);
        map.Add(inputs.First(c => c.Length == 3).ToArray().First(c => !map.ContainsKey(c)), 0);
        map.Add(inputs.First(c => c.Length == 4).ToArray().First(c => !map.ContainsKey(c)), 3);
        map.Add("abcdefg".ToArray().First(c => !map.ContainsKey(c)), 6);
    }

    private int Decode(string digit) => patterns.IndexOf(digit.Select(c => 1 << map[c]).Aggregate(0, (x, y) => x | y));
    public string Solve() => digits.Select(Decode).Aggregate(String.Empty, (s1, s2) => s1 + s2);
}