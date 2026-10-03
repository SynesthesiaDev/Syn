namespace Syn.Lexer.Lexing;

public record Token(TokenType Type, string Value, int Line, int Col, string File);
