namespace Syn.Lexer.Lexing;

public enum TokenType
{
    // Type keywords
    Int,
    Float,
    Short,
    Long,
    Double,
    Bool,
    String,
    Char,
    Byte,
    Any,
    Nothing,

    // Literals
    IntLiteral,
    FloatLiteral,
    StringLiteral,
    CharLiteral,
    DoubleLiteral,
    LongLiteral,
    ShortLiteral,
    NullLiteral,

    // Top-level declarations
    Import,
    Package,

    // Variable declarations
    Let,
    Var,

    // Declarations
    Func,
    Class,
    Struct,
    Return,
    If,
    Else,
    True,
    False,

    // Operators
    Assign,
    Equals,
    Arrow,
    LambdaArrow,
    Dot,
    DoubleColon,
    QuestionMark,
    NullCoalesce,
    NullCoalesceAssign,
    SafeNavigation,
    Range,
    To,
    New,
    Is,
    As,

    Identifier,
    Plus,
    Minus,
    Multiply,
    Divide,
    Modulo,
    PlusEquals,
    MinusEquals,
    MultiplyEquals,
    DivideEquals,
    ModuloEquals,

    LeftBrace,
    RightBrace,
    LeftParen,
    RightParen,
    LeftBracket,
    RightBracket,
    Semicolon,
    Colon,
    Comma,
    NotEquals,
    Greater,
    GreaterEquals,
    Less,
    LessEquals,
    Unwrap,
    NotNullOrErrorAssert,
    AmpAmp,
    PipePipe,
    Catch,
    Not,

    // Loop keywords
    For,
    In,
    Continue,
    Do,
    Break,
    While,

    // Misc
    Discard,

    EOF,
    Unknown,
    Comment
}
