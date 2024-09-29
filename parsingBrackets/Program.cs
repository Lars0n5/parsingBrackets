
class Program {
    static void Main () {
        Console.Write("Введите выражение для проверки: ");
        string expression = Console.ReadLine();
        if (IsBalancedAndExtractGroups(expression, out List<string> groups, out List<int> unclosedBrackets, out List<int> unmatchedClosingBrackets)) {
            Console.WriteLine("Скобки сбалансированы.");
            Console.WriteLine("Группы внутри скобок:");
            foreach (string group in groups) {
                Console.WriteLine(group);
            }
        } else {
            Console.WriteLine("Скобки не сбалансированы.");
        }
        Console.WriteLine("Исходная строка:");
        Console.WriteLine(expression);
    }

    static bool IsBalancedAndExtractGroups (string expr, out List<string> groups, out List<int> unclosedBrackets, out List<int> unmatchedClosingBrackets) {
        groups = new List<string>();
        unclosedBrackets = new List<int>();
        unmatchedClosingBrackets = new List<int>();
        Stack<int> openBrackets = new Stack<int>();

        for (int i = 0; i < expr.Length; i++) {
            char ch = expr[i];
            if (ch == '(' || ch == '{') {
                openBrackets.Push(i);
            } else if (ch == ')' || ch == '}') {
                if (openBrackets.Count == 0) {
                    unmatchedClosingBrackets.Add(i);
                } else {
                    int startIndex = openBrackets.Pop();
                    char openBracket = expr[startIndex];
                    if ((ch == ')' && openBracket != '(') || (ch == '}' && openBracket != '{')) {
                        return false;
                    }
                    int length = i - startIndex - 1;
                    if (length > 0) {
                        string group = expr.Substring(startIndex + 1, length);
                        groups.Add(group);
                    }
                }
            }
        }
        while (openBrackets.Count > 0) {
            unclosedBrackets.Add(openBrackets.Pop());
        }
        return unclosedBrackets.Count == 0 && unmatchedClosingBrackets.Count == 0;
    }
}
