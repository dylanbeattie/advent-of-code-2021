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

var o2 = MaxFilter(0, inputs);
var co2 = MinFilter(0, inputs);
Console.WriteLine($"Solution to part 2: {o2 * co2}");

int MaxFilter(int index, IEnumerable<int[]> inputs) {
    Console.WriteLine("==================");
    foreach(var i in inputs) {
        Console.WriteLine(String.Join("", i.Select(s => s.ToString()).ToArray()));
    }
    var mcb = MostCommonBit(inputs, index);
    inputs = inputs.Where(i => i[index] == mcb).ToList();
    return
    inputs.Count() == 1 
        ? Splat(inputs.First())
        :
        MaxFilter(index+1, inputs);
}
 
 
int MinFilter(int index, IEnumerable<int[]> inputs) {
    var lcb = LeastCommonBit(inputs, index);
    inputs = inputs.Where(i => i[index] == lcb).ToList();
    return
    inputs.Count() == 1 
        ? Splat(inputs.First())
        :
        MinFilter(index+1, inputs);
}

int Splat(IEnumerable<int> bits) {
    var s = String.Join("", bits.Select(c => c.ToString()).ToArray());
    return Convert.ToInt32(s, 2);
}

int MostCommonBit(IEnumerable<int[]> inputs, int index) {
    var groups = inputs
        .Select(i => i[index])
        .GroupBy(i => i).ToArray();
        if (groups[0].Count() == groups[1].Count()) return(1);
        return groups.OrderBy(g => g.Count()).Last().Key;
}

int LeastCommonBit(IEnumerable<int[]> inputs, int index) {
        var groups = inputs
        .Select(i => i[index])
        .GroupBy(i => i).ToArray();
        if (groups[0].Count() == groups[1].Count()) return(0);
        return groups.OrderBy(g => g.Count()).First().Key;
}