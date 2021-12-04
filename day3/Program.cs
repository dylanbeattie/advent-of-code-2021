var inputs = File.ReadAllLines("input.txt")
.Select(l => l.Select(c => Int32.Parse(c.ToString())).ToArray());

var dictionary = new Dictionary<int, Dictionary<int,int>>();
var gammaBits = new List<int>();
var epsilonBits = new List<int>();

for(var i = 0; i < inputs.First().Length; i++) {
    gammaBits.Add(MostCommonBit(inputs, i));
    epsilonBits.Add(LeastCommonBit(inputs, i));
}

var gamma = Splat(gammaBits);
var epsilon = Splat(epsilonBits);

Console.WriteLine($"Solution to part 1: {gamma * epsilon}");

int Filter(int[] pattern, IEnumerable<int[]> inputs) {
    for(var i = 0; i < pattern.Length; i++) {
        var bit = pattern[i];
        inputs = inputs.Where(input => input[i] == pattern[i]).ToList();
        if (inputs.Count() == 1) return(Splat(inputs.First()));
    }
    throw new Exception("BOOM!");
}

int Splat(IEnumerable<int> bits) {
    var s = String.Join("", bits.Select(c => c.ToString()).ToArray());
    return Convert.ToInt32(s, 2);
}

int MostCommonBit(IEnumerable<int[]> inputs, int index) => inputs
        .Select(i => i[index])
        .GroupBy(i => i)
        .OrderBy(g => g.Count()).Last().Key;

int LeastCommonBit(IEnumerable<int[]> inputs, int index) => inputs
        .Select(i => i[index])
        .GroupBy(i => i)
        .OrderBy(g => g.Count()).First().Key;