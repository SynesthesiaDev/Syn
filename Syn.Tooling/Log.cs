using Spectre.Console;

namespace Syn.Tooling;

public static class Log
{
    public static void ParsingError(string filePath, int line, int col, string message, string? extraInfo = null)
    {

        string location = $"{filePath}:{line}:{col}";

        string fullPath = Path.GetFullPath(filePath).Replace('\\', '/');

        AnsiConsole.MarkupLine($"[red bold]Error:[/] [red]{message}[/]");
        AnsiConsole.MarkupLine($"[grey]  ↪ [Gray58]at [/][blue underline link=file://{fullPath}]{location}[/][/]");
        if (extraInfo != null)
        {
            AnsiConsole.MarkupLine($"[gray]  ↪ [/][Gray58]{extraInfo}[/]");
        }
        Console.WriteLine();
    }




}
