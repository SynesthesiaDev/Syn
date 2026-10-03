// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace Syn.Lexer.Lexing;

public static class Symbols
{
    public static Token Read(Lexer lexer)
    {
        var c = lexer.Current;
        return c switch
        {
            '{' => lexer.Single(TokenType.LeftBrace),
            '}' => lexer.Single(TokenType.RightBrace),
            '(' => lexer.Single(TokenType.LeftParen),
            ')' => lexer.Single(TokenType.RightParen),
            '[' => lexer.Single(TokenType.LeftBracket),
            ']' => lexer.Single(TokenType.RightBrace),
            ',' => lexer.Single(TokenType.Comma),
            ';' => lexer.Single(TokenType.Semicolon),
            '-' => lexer.MatchIfNext('>', TokenType.Arrow, TokenType.Minus),
            '"' => lexer.ReadString(),
            _ =>  lexer.Single(TokenType.Unknown)

            // '=' => lexer.MatchIfNext('=', TokenType.Equals, TokenType.Assign)
            // ':' => lexer.Single(TokenType.DoubleColon),
            // '.' => lexer.Single(TokenType.DoubleColon),
        };
    }
}
