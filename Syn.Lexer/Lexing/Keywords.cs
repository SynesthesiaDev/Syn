// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.


namespace Syn.Lexer.Lexing;

public class Keywords
{
    public static Dictionary<string, TokenType> Map = new()
    {
        ["func"] = TokenType.Func,
        ["let"] = TokenType.Let,
        ["var"] = TokenType.Var,
        ["import"] = TokenType.Import,
        ["package"] = TokenType.Package,
        ["class"] = TokenType.Class,
        ["struct"] = TokenType.Struct,
        ["return"] = TokenType.Return,
        ["if"] = TokenType.If,
        ["else"] = TokenType.Else,
        ["true"] = TokenType.True,
        ["false"] = TokenType.False,
        ["for"] = TokenType.For,
        ["while"] = TokenType.While,
        ["do"] = TokenType.Do,
        ["break"] = TokenType.Break,
        ["continue"] = TokenType.Continue,
        ["to"] = TokenType.To,
        ["as"] = TokenType.As,
        ["new"] = TokenType.New,
        ["is"] = TokenType.Is,
        ["catch"] = TokenType.Catch,
        ["not"] = TokenType.Not,
        ["in"] = TokenType.In,
    };

    public static bool IsKeyword(string text) => Map.ContainsKey(text);
    public static TokenType Lookup(string text) => Map.GetValueOrDefault(text, TokenType.Identifier);
}
