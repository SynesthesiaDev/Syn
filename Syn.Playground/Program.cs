using Syn.Interpreter;
using Syn.TreeWalker;

namespace Syn.Playground;

internal class Program
{
    private static void Main(string[] args)
    {
        var path = "../../../../test.syn";
        var file = File.ReadAllText(path);
        var tokens = Lexer.Lexing.Lexer.From(file, path);
        var treeWalker = new AstTreeWalker(tokens).MakeTree();

        // foreach (var token in tokens)
        // {
        //     Console.WriteLine(token);
        // }
        //
        // Console.WriteLine(" ");
        //
        // foreach (var astNode in treeWalker)
        // {
        //     Console.WriteLine(astNode.ToString());
        // }

        var interpreter = new SynInterpreter();
        interpreter.Run(treeWalker);

        // var func = new FuncDeclaration("Main", [], TypeRef.VOID, new Block([
        // new Call(new Variable("Debug"), [new StringLiteral("yahoo")])
        // ]
        // ));

        // Console.WriteLine(func.ToString());
    }
}
