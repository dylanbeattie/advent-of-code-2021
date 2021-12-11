// See https://aka.ms/new-console-template for more information

var inputs = File.ReadAllLines("input.txt");

var part1Solution = inputs
             .Select(FindFirstCorruptCharacter)
             .Where(c => c > 1)
             .Select(scorePart1)
             .Sum();

Console.WriteLine(part1Solution);

//foreach (var input in inputs) {
//    if (FindFirstCorruptCharacter(input) != 1) continue;
//    Console.WriteLine($"{input}    {new String(FindCompletionSequence(input))}");
//}
var foo = "])}>".Aggregate<char, long>(0, (s, c) => (s * 5) + scorePart2(c));
Console.WriteLine($"TEST: {foo}");

var part2Scores = inputs
    .Where(line => FindFirstCorruptCharacter(line) == 1)
    .Select(FindCompletionSequence)
    .Select(sequence => sequence.Aggregate<char,long>(0, (s, c) => (s * 5) + scorePart2(c)));

foreach (var s in part2Scores) Console.WriteLine(s);

var part2ScoresInOrder = part2Scores.OrderBy(s => s).ToArray();
foreach(var s in part2ScoresInOrder) {}
var howMany = part2ScoresInOrder.Length;
var middleIndex = (howMany / 2);
var middleScore = part2ScoresInOrder[middleIndex];

Console.WriteLine($"Solution to part 2: {middleScore}");

const string pattern = "{}<>()[]";

int scorePart2(char c) {
    return c switch {
        ')' => 1,
        ']' => 2,
        '}' => 3,
        '>' => 4,
        _ => 0
    };
}

int scorePart1(char c) {
    return c switch {
        '(' => 1,
        '[' => 2,
        '{' => 3,
        '<' => 4,
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137,
        _ => 0
    };
}

char FindFirstCorruptCharacter(string line) {
    var stack = new Stack<char>();
    foreach (var c in line) {
        var index = pattern.IndexOf(c);
        if ((index & 1) == 1) { // closing bracket
            var match = pattern.IndexOf(stack.Pop()) + 1;
            if (match != index) return c;
        } else { // opening bracket
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
            var match = pattern.IndexOf(stack.Pop()) + 1;
            if (match != index) throw new ArgumentException("This method only works on incomplete lines");
        } else { // opening bracket
            stack.Push(c);
        }
    }
    return stack.Select(c => pattern[pattern.IndexOf(c) + 1]).ToArray();
}



