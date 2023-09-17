// Решение тестового задания #1 для концерна «Энергомера».
// Для определения правильной последовательности скобок использовался Stack.

string[] inputs =
{
    "(((){}[]]]])",
    "(){}[][][]()",
    "({[]})()[]"
};

foreach (string input in inputs)
{
    bool result = CheckBrackets(input);
    Console.WriteLine($"Исходные данные: {input}");
    Console.WriteLine($"Результат: {(result ? "true" : "false")}");
    Console.WriteLine();
}

Console.ReadKey();

static bool CheckBrackets(string input)
{
    Stack<char> stack = new Stack<char>();

    foreach (char c in input)
    {
        if (IsOpeningBracket(c))
        {
            stack.Push(c);
        }
        else if (IsClosingBracket(c))
        {
            if (stack.Count == 0 || !IsMatchingBrackets(stack.Peek(), c))
            {
                return false;
            }
            stack.Pop();
        }
    }

    return stack.Count == 0;
}

static bool IsOpeningBracket(char c) => c is '(' or '{' or '[';
static bool IsClosingBracket(char c) => c is ')' or '}' or ']';
static bool IsMatchingBrackets(char opening, char closing)
{
    return (opening == '(' && closing == ')') || (opening == '{' && closing == '}') || (opening == '[' && closing == ']');
}