// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Text;
using Syn.Std;
using Syn.TreeWalker;
using String = Syn.TreeWalker.String;

namespace Syn.Interpreter;

public class SynInterpreter
{
    private readonly Dictionary<string, FuncDeclaration> functions = new();

    public void Run(List<IAstNode> program)
    {
        Console.OutputEncoding = Encoding.UTF8;

        foreach (var node in program)
        {
            if (node is FuncDeclaration func)
            {
                functions[func.Name] = func;
            }

            if (!functions.TryGetValue("Main", out var main))
                throw new Exception("no Main function found");

            execBlock(main.Body);

        }
    }

    private void execBlock(Block block)
    {
        foreach (var stmt in block.Statements) eval(stmt);
    }

    private object? eval(IAstNode node)
    {
        return node switch
        {
            String s => s.Value,
            Call c => evalCall(c),
            _ => throw new NotSupportedException($"can't evaluate {node.GetType().Name}")
        };
    }

    private object? evalCall(Call call)
    {
        if (call.Callee is not Variable v)
            throw new NotSupportedException("can only call named functions");

        var args = call.Args.Select(eval).ToList();

        if (v.Name == "Debug") { Native.Debug(call.SourceLocation.File, call.SourceLocation.Line, call.SourceLocation.Col, args[0]!.ToString()); return null; }

        if (functions.TryGetValue(v.Name, out var f)) { execBlock(f.Body); return null; }

        throw new Exception($"unknown function '{v.Name}'");
    }
}
