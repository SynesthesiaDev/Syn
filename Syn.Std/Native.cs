
using Spectre.Console;

namespace Syn.Std;

public static class Native
{
    public static void Debug(string filePath, int line, int col, string message)
    {
        string location = $"{filePath}:{line}";

        string fullPath = Path.GetFullPath(filePath).Replace('\\', '/');
        AnsiConsole.MarkupLine($"[grey link=file://{fullPath}]{location}[/]");
        Console.WriteLine(message);

    }
}
