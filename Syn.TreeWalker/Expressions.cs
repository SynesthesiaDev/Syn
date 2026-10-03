// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Syn.TreeWalker;

public record StringLiteral(string Value) : Expression;

public record Param(string Name, TypeRef Type) : Expression;

public record Call(Expression Callee, List<Expression> Args) : Statement;

public record TypeRef(string Name, string FullPath, bool Nullable)
{
    public static readonly TypeRef VOID = new TypeRef("Void", "syn.core.Void", false);
}

public record Block(List<Statement> Statements) : Expression;

public record Variable(string Name) : Expression;
