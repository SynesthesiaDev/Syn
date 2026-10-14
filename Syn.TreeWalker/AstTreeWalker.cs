// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Syn.Lexer.Lexing;
using Syn.Tooling;

namespace Syn.TreeWalker;

public class AstTreeWalker(List<Token> tokens)
{
    private int position;

    protected Token Current => tokens[position];
    protected Token Peek(int offset = 1) => tokens[Math.Min(position + offset, tokens.Count - 1)];
    protected bool IsAtEnd => Current.Type == TokenType.EOF;

    public List<IAstNode> MakeTree()
    {
        var nodes = new List<IAstNode>();
        while (!IsAtEnd) nodes.Add(ParseTopLevel());
        return nodes;
    }

    protected IAstNode ParseTopLevel()
    {
        if (CheckCurrent(TokenType.Func)) return ParseFunc();
        throw new InvalidOperationException("expected a declaration");
    }

    protected FuncDeclaration ParseFunc()
    {
        ExpectThenAdvance(TokenType.Func, "func", "Function needs to start with 'func' keyword");
        var name = ExpectThenAdvance(TokenType.Identifier, "function name").Value;

        var parameters = ParseParams();

        ExpectThenAdvance(TokenType.Arrow, "->", "Function requires '->' with either code block or return type after it");
        var hasType = !CheckCurrent(TokenType.LeftBrace);

        TypeRef? typeRef = null;
        if (hasType)
        {
            typeRef = ParseTypeRef();
        }
        var block = ParseBlock();

        return new FuncDeclaration(name, parameters, typeRef ?? TypeRef.VOID, block);
    }

    protected TypeRef ParseTypeRef()
    {

        var isResult = AdvanceIfCurrentIs(TokenType.Not);
        var typeName = ExpectThenAdvance(TokenType.Identifier, "type name");
        var nullable = AdvanceIfCurrentIs(TokenType.QuestionMark);

        //TODO proper generics

        var typeRef = new TypeRef(typeName.Value, nullable);

        if (isResult) typeRef = TypeRef.Result(typeRef);

        return typeRef;
    }

    protected List<Param> ParseParams()
    {
        // starting paren
        ExpectThenAdvance(TokenType.LeftParen, "(");

        var list = new List<Param>();
        while (true)
        {
            // We have hit the closing paren
            if (CheckCurrent(TokenType.RightParen))
                break;

            // (Type name)
            var type = ParseTypeRef();
            var name = ExpectThenAdvance(TokenType.Identifier, "name");

            // If next is closing paren then who cares, if not, then we need a comma!
            if (!CheckCurrent(TokenType.RightParen)) ExpectThenAdvance(TokenType.Comma, ",", "Parameters need to be separated by a comma");

            list.Add(new Param(name.Value, type));
        }

        ExpectThenAdvance(TokenType.RightParen, ")");
        return list;
    }

    protected Block ParseBlock()
    {
        ExpectThenAdvance(TokenType.LeftBrace, "{");

        var statements = ParseStatements();

        ExpectThenAdvance(TokenType.RightBrace, "}");

        return new Block(statements);
    }

    protected List<IAstNode> ParseStatements()
    {
        var statements = new List<IAstNode>();
        while (!CheckCurrent(TokenType.RightBrace) && !IsAtEnd)
            statements.Add(ParseStatement());

        return statements;
    }

    protected IAstNode ParseStatement()
    {
        var expr = ParseExpression();
        ExpectThenAdvance(TokenType.Semicolon, ";", "Statements must end with semicolon");
        return expr;
    }

    protected IAstNode ParseExpression()
    {
        var expr = ParsePrimary();
        while (CheckCurrent(TokenType.LeftParen))
            expr = ParseCallRest(expr);
        return expr;
    }

    protected IAstNode ParsePrimary()
    {
        if (CheckCurrent(TokenType.String)) return new String(Advance().Value);
        if (CheckCurrent(TokenType.Identifier)) return new Variable(Advance().Value);

        Log.ParsingError(Current.File, Current.Line, Current.Col, $"expected expression, but got '{Current.Value}'");
        throw new Exception();
    }

    protected IAstNode ParseCallRest(IAstNode callee)
    {
        ExpectThenAdvance(TokenType.LeftParen, "(");
        var args = new List<IAstNode>();

        if (!AdvanceIfCurrentIs(TokenType.RightParen))
        {
            do args.Add(ParseExpression());
            while (AdvanceIfCurrentIs(TokenType.Comma));
        }

        ExpectThenAdvance(TokenType.RightParen, ")");
        return new Call(callee, args, new SourceLocation(Current.File, Current.Line, Current.Col));
    }



    protected Token Advance()
    {
        var t = Current;
        if (!IsAtEnd) position++;
        return t;
    }

    protected bool CheckCurrent(TokenType type) => Current.Type == type;

    protected  bool AdvanceIfCurrentIs(TokenType type)
    {
        if (!CheckCurrent(type)) return false;
        Advance();
        return true;
    }

    protected Token ExpectThenAdvance(TokenType type, string what, string? extraInfo = null)
    {
        if (CheckCurrent(type)) return Advance();
        Log.ParsingError(Current.File, Current.Line, Current.Col, $"expected '{what}', but got '{Current.Value}'", extraInfo);

        throw new Exception();
    }
}
