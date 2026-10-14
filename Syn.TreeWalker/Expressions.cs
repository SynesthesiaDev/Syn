// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Syn.TreeWalker;

public record String(string Value) : Expression;

public record Param(string Name, TypeRef Type) : Expression;

public record Call(IAstNode Callee, List<IAstNode> Args, SourceLocation SourceLocation) : Statement;

public record TypeRef(string Name, bool Nullable, IReadOnlyList<TypeRef>? TypeArgs = null)
{
    public static readonly TypeRef VOID = new TypeRef("Void", false);

    public static TypeRef Result(TypeRef inner) =>
        new("Result", false, [inner]);
}

public record Block(List<IAstNode> Statements) : Expression;

public record Variable(string Name) : Expression;
