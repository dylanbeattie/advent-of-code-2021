// See https://aka.ms/new-console-template for more information
var octopods = File.ReadAllLines("example.txt")
    .Select(line => line.ToCharArray().Select(c => Int32.Parse(c.ToString())).ToArray()).ToArray();

int flashes = 0;
for (var i = 0; i < 100; i++) Tick(octopods);

Console.WriteLine(flashes);


void Tick(int[][] grid)
{
    for (var row = 0; row < grid.Length; row++)
    {
        for (var col = 0; col < grid.Length; col++)
        {
            Flash(grid, row, col, false);
        }
    }
    foreach (var row in grid)
    {
        Console.WriteLine(String.Join("", row.Select(e => e.ToString()).ToArray()));
    }
    Console.WriteLine("==========");
}

void Flash(int[][] grid, int row, int col, bool flashing)
{
    // Don't do anything if we're out of bounds.
    if (row < 0 || col < 0) return;
    if (row >= grid.Length || col >= grid[row].Length) return;
    //    if (flashing && grid[row][col] == 0) return; // any octopus who already flashed can't flash again this round.
    if (!flashing) grid[row][col]++;
    if (grid[row][col] > 9)
    {
        flashes++;
        grid[row][col] = 0; // We flashed. Zero.
        for (var r2 = row - 1; r2 < row + 2; r2++)
        {
            for (var c2 = col - 1; c2 < col + 2; c2++)
            {
                if (r2 == row && c2 == col) continue; // don't flash yourself
                Flash(grid, r2, c2, true);
            }
        }
    }
}


public class Octopus
{
    private readonly int row;
    private readonly int col;
    private readonly int energy;

    public Octopus(int row, int col, int energy)
    {
        this.row = row;
        this.col = col;
        this.energy = energy;
    }



    public List<Octopus> Neighbours { get; set; } = new List<Octopus>();
}
