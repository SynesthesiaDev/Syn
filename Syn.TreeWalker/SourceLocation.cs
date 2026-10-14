// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Syn.TreeWalker;

public readonly record struct SourceLocation(string File, int Line, int Col)
{
    public override string ToString() => $"{File}:{Line}:{Col}";
}
