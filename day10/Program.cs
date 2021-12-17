
var inputs = File.ReadAllLines("input.txt");

var part1Solution = inputs.Select(FindFirstCorruptCharacter).Where(c => c > 1).Sum(ScorePart1);

Console.WriteLine($"Solution to part 1: {part1Solution}");

var part2Scores = inputs
    .Where(line => FindFirstCorruptCharacter(line) == 1)
    .Select(FindCompletionSequence)
    .Select(sequence => sequence.Aggregate<char,long>(0, (s, c) => (s * 5) + ScorePart2(c)))
    .OrderBy(s => s).ToArray();

var middleScore = part2Scores[part2Scores.Length / 2];

Console.WriteLine($"Solution to part 2: {middleScore}");

int ScorePart2(char c) => " )]}>".IndexOf(c);

int ScorePart1(char c) {
    return c switch {
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137,
        _ => 0
    };
}

const string pattern = "{}<>()[]";

char FindFirstCorruptCharacter(string line) {
    var stack = new Stack<char>();
    foreach (var c in line) {
        var index = pattern.IndexOf(c);
        if ((index & 1) == 1) { 
            var match = pattern.IndexOf(stack.Pop()) + 1;
            if (match != index) return c;
        } else { 
            stack.Push(c);
        }
    }
    if (stack.Count > 0) return (char)1; // line was incomplete
    return (char)0; // line was valid and complete.
}

char[] FindCompletionSequence(string line) {
    var stack = new Stack<char>();
    foreach (var c in line) {
        var index = pattern.IndexOf(c);
        if ((index & 1) == 1) { // closing bracket
            stack.Pop();
        } else { // opening bracket
            stack.Push(c);
        }
    }
    return stack.Select(c => pattern[pattern.IndexOf(c) + 1]).ToArray();
}



