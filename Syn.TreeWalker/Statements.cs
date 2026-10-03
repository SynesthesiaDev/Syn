// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Syn.TreeWalker;

public record FuncDeclaration(string Name, List<Param> Params, TypeRef Type, Expression Body) : Statement;

