using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace JSONParser.Parser
{
    public static class JsonParser
    {
        public static JsonValue Start(string input)
        {
            return Start(JsonLexer.Lex(input));
        }

        public static JsonValue Start(List<Token> input)
        {
            return ParseValue(new TokenStream(input));
        }

        public static JsonValue ParseValue(TokenStream s)
        {
            switch (s.Current().Type)
            {
                case TokenType.BeginObject:
                    return ParseContainer(s, TokenType.BeginObject, TokenType.EndObject, ParsePair, m => new JsonObject(m));
                case TokenType.BeginArray:
                    return ParseContainer(s, TokenType.BeginArray, TokenType.EndArray, ParseValue, m => new JsonArray(m));
                case TokenType.Number:
                    return ParseNumber(s);
                case TokenType.String:
                    return ParseString(s);
                case TokenType.Boolean:
                    return ParseBoolean(s);
                case TokenType.Null:
                    return ParseNull(s);
                default:
                    throw s.NewParseException();
            }
        }

        public static T ParseContainer<T, C>(TokenStream s, TokenType begin, TokenType end, Func<TokenStream, C> parser, Func<List<C>, T> factory)
        {
            s.Consume(begin);
            var container = factory(ParseChildren(s, end, parser));
            s.Consume(end);

            return container;
        }

        public static string ParseLiteralString(TokenStream s)
        {
            return ParseChars(new CharStream(s.Consume(TokenType.String).Value.Substring(1)));
        }

        public static JsonString ParseString(TokenStream s)
        {
            return new JsonString(ParseLiteralString(s));
        }

        public static JsonNumber ParseNumber(TokenStream s)
        {
            return new JsonNumber(Convert.ToDouble(s.Consume(TokenType.Number).Value, CultureInfo.InvariantCulture));
        }

        public static JsonBoolean ParseBoolean(TokenStream s)
        {
            return new JsonBoolean(Convert.ToBoolean(s.Consume(TokenType.Boolean).Value));
        }

        public static JsonNull ParseNull(TokenStream s)
        {
            s.Consume(TokenType.Null);
            return JsonNull.NULL;
        }

        public static List<T> ParseChildren<T>(TokenStream s, TokenType terminator, Func<TokenStream, T> parser)
        {
            var children = new List<T>();
            if (s.Current().Type == terminator)
            {
                return children;
            }

            children.Add(parser(s));
            while (s.Current().Type == TokenType.ElementSeperator)
            {
                s.Consume(TokenType.ElementSeperator);
                children.Add(parser(s));
            }

            return children;
        }

        public static string ParseChars(CharStream s)
        {
            var str = new StringBuilder();
            while (s.Current() != '"')
            {
                str.Append(ParseChar(s));
            }
            return str.ToString();
        }

        public static char ParseChar(CharStream s)
        {
            switch (s.Current())
            {
                case '"':
                    throw s.NewParseException();
                case '\\':
                    s.Consume();
                    var ch = s.Consume();
                    switch (ch)
                    {
                        case 'n':
                            return '\n';
                        case 'r':
                            return '\r';
                        case 't':
                            return '\t';
                        case 'u':
                            return ParseUnicodeSequence(s);
                        case 'b':
                            return '\b';
                        case 'f':
                            return '\f';
                        case '"':
                        case '/':
                        case '\\':
                            return ch;
                        default:
                            throw s.NewParseException();
                    }
                default:
                    return s.Consume();
            }
        }

        public static Tuple<string, JsonValue> ParsePair(TokenStream s)
        {
            var str = ParseLiteralString(s);
            s.Consume(TokenType.PairSeperator);
            return new Tuple<string, JsonValue>(str, ParseValue(s));
        }

        public static char ParseUnicodeSequence(CharStream s)
        {
            var str = "" + s.Consume() + s.Consume() + s.Consume() + s.Consume();
            try
            {
                return Convert.ToChar(Convert.ToInt32(str, 16));
            }
            catch (FormatException)
            {
                throw s.NewParseException();
            }
        }
    }
}