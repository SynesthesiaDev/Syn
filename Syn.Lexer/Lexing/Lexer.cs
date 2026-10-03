// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Text;

namespace Syn.Lexer.Lexing;

public class Lexer(string source, string file)
{
    private int position;
    private int line = 1;
    private int col = 1;

    private bool atEnd => position >= source.Length;
    public char Current => position < source.Length ? source[position] : '\0';


    public static List<Token> From(string source, string file)
    {
        var lexer = new Lexer(source, file);
        return lexer.Tokenize();
    }

    public List<Token> Tokenize()
    {
        var tokens = new List<Token>();

        while (true)
        {

            SkipWhitespaceAndComments();
            int startLine = line, startCol = col;
            char c = Current;

            if (atEnd)
            {
                tokens.Add(new Token(TokenType.EOF, string.Empty, line, col, file));
                break;
            }

            Token token = c switch
            {
                _ when char.IsLetter(c) || c == '_' => ReadIdentifierOrKeyword(),
                _ => Symbols.Read(this)
            };

            tokens.Add(token);
        }

        return tokens;
    }

    protected Token ReadIdentifierOrKeyword()
    {
        int startLine = line;
        int startCol = col;
        int start = position;

        while (!atEnd && (char.IsLetterOrDigit(Current) || Current == '_')) Advance();

        var text = source[start..position];
        var type = Keywords.Lookup(text);

        return new Token(type, text, startLine, startCol, file);
    }

    public Token Single(TokenType type)
    {
        int startCol = col;
        int startLine = line;
        char c = Advance();
        return new Token(type, c.ToString(), startLine, startCol, file);
    }

    public Token MatchIfNext(char ifNext, TokenType yes, TokenType no)
    {
        int startCol = col;
        int startLine = line;

        char current = Current;
        char next = Peek();
        Advance();

        // Console.WriteLine($"current = {current}, next = {next}, ifNext= {ifNext}, ({next == ifNext})");
        if (next == ifNext)
        {
            Advance(); // consume the "next"
            return new Token(yes, current + next.ToString(), startLine, startCol, file);
        }

        return new Token(no, current.ToString(), startLine, startCol, file);
    }

    // UTILS

    protected char Peek(int offset = 1) =>
        position + offset < source.Length
            ? source[position + offset]
            : '\0';

    protected char Advance()
    {
        char c = Current;
        position++;
        if (c == '\n')
        {
            line++;
            col = 1;
        }
        else col++;

        return c;
    }

    protected void SkipWhitespaceAndComments()
    {
        while (!atEnd)
        {
            if (char.IsWhiteSpace(Current))
            {
                Advance();
            }
            else if (Current == '/' && Peek() == '/')
            {
                while (!atEnd && Current != '\n') Advance();
            }
            else
            {
                break;
            }
        }
    }

    public Token ReadString()
    {
        var startLine = line;
        var startColumn = col;

        Advance();

        var sb = new StringBuilder();

        while (true)
        {
            if (atEnd) throw new Exception($"Unterminated literal string starting at line {startLine}, col {startColumn}");
            if (Current == '"')
            {
                Advance();
                break;
            }

            sb.Append(Advance());
        }

        return new Token(TokenType.String, sb.ToString(), startLine, startColumn, file);
    }


}
