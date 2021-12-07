var positions = File.ReadAllText("input.txt").Split(',').Select(Int32.Parse).ToList();

var minimum = positions.Min();
var maximum = positions.Max();
var distances = Enumerable.Range(minimum, maximum - minimum).ToList();

int Solve(Func<int, int, int> calculateFuelCost) 
    => distances.Select(d => positions.Select(p => calculateFuelCost(p, d)).Sum()).Min();

int FuelCostPart1(int s, int p) => Math.Abs(s - p);
int FuelCostPart2(int s, int p) => Triangulate(FuelCostPart1(s, p));
int Triangulate(int x) => x * (x + 1) / 2;

var sw = new System.Diagnostics.Stopwatch();
sw.Start();
Console.WriteLine(Solve(FuelCostPart1));
Console.WriteLine(Solve(FuelCostPart2));
Console.WriteLine($"Total time: {sw.ElapsedMilliseconds}ms");





