var inputs = File.ReadAllLines("input.txt")
    .Select(line => new TestCase(line));

var solutions = inputs.Select(input => input.Solve());

var allDigits = String.Join("", solutions.ToArray());
var totalPart1 = allDigits.GroupBy(c => c)
    .Where(group => "1478".Contains(group.Key))
    .Sum(group => group.Count());

var totalPart2 = solutions.Sum(Int32.Parse);

Console.WriteLine($"solution to part 1: {totalPart1}");
Console.WriteLine($"solution to part 2: {totalPart2}");


class TestCase {
    private Dictionary<char, int> Map = new();
    public TestCase(string line) {
        var tokens = line.Split('|');
        InputPatterns = tokens[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        DigitPatterns = tokens[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        BuildMap();
    }

    private static Dictionary<int, int> patterns = (new[] {
            "1110111", "0010010", "1011101", "1011011", "0111010",
            "1101011", "1101111", "1010010", "1111111", "1111011"
        }).Select((item, index) => (item: new String(item.Reverse().ToArray()), index))
        .ToDictionary(pair => Convert.ToInt32(pair.item, 2), pair => pair.index);

    private void BuildMap() {
        var groups = String.Join("", InputPatterns).GroupBy(c => c);
        Console.WriteLine("==========");
        foreach (var group in groups) {
            Console.WriteLine($"{group.Key}: {group.Count()}");
        }
        Map.Add(groups.First(g => g.Count() == 4).Key, 4);
        Map.Add(groups.First(g => g.Count() == 6).Key, 1);
        Map.Add(groups.First(g => g.Count() == 9).Key, 5);
        var signal1 = InputPatterns.First(c => c.Length == 2).ToArray().First(c => !Map.ContainsKey(c));
        Map.Add(signal1, 2);
        var signal2 = InputPatterns.First(c => c.Length == 3).ToArray().First(c => !Map.ContainsKey(c));
        Map.Add(signal2, 0);
        var signal3 = InputPatterns.First(c => c.Length == 4).ToArray().First(c => !Map.ContainsKey(c));
        Map.Add(signal3, 3);
        Map.Add("abcdefg".ToArray().First(c => !Map.ContainsKey(c)), 6);
    }

    private int Decode(string digit) {
        var bitField = digit.Select(c => 1 << Map[c]).Aggregate(0, (x, y) => x | y);
        return patterns[bitField];
    }

    public string Solve() {
        return String.Join("", DigitPatterns.Select(Decode).Select(i => i.ToString()).ToArray());
    }

    public string[] InputPatterns { get; set; }
    public string[] DigitPatterns { get; set; }
}