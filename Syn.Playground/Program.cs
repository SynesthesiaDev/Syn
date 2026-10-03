using Syn.TreeWalker;

namespace Syn.Playground;

internal class Program
{
    private static void Main(string[] args)
    {
        var file = File.ReadAllText("../../../../test.syn");
        var tokens = Lexer.Lexing.Lexer.From(file, "test.syn");
        //
        // foreach (var token in tokens)
        // {
        //     Console.WriteLine(token);
        // }

        var func = new FuncDeclaration("Main", [], TypeRef.VOID, new Block([
                new Call(new Variable("Debug"), [new StringLiteral("yahoo")])
            ]
        ));

        Console.WriteLine(func.ToString());
    }
}
