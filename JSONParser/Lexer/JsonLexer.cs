using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JSONParser.Parser
{
    public static class JsonLexer
    {
        private static Regex reg = new Regex(@"\G(?:(\{)|(\})|(\[)|(\])|(:)|(,)|(-?(?:0|[1-9]\d*)(?:\.\d+)?(?:[Ee][+-]?\d+)?)|(\""(?:[^\\\""]|\\(?:[\\\""\/bfnrt]|u[0-9A-Fa-f]{4}))*\"")|((?:\x20|\t|\r)+)|(true|false)|(null)|(\n))",
            RegexOptions.Compiled);

        public static List<Token> Lex(string input)
        {
            var tokenList = new List<Token>();
            var startat = 0;
            var row = 1;
            var column = 1;
            while (startat < input.Length)
            {
                var match = reg.Match(input, startat);
                if (!match.Success)
                {
                    throw new LexException("Invalid Token", row, column);
                }

                for (int index = 1; index < match.Groups.Count; index++)
                {
                    var value = match.Groups[index].Value;
                    if (!string.IsNullOrEmpty(value))
                    {
                        var type = (TokenType) index;
                        switch (type)
                        {
                            case TokenType.NewLine:
                                row++;
                                column = 1;
                                break;
                            case TokenType.Whitespace:
                                break;
                            default:
                                tokenList.Add(new Token(type, value, row, column));
                                startat += value.Length;
                                column += value.Length;
                                break;
                        }
                    }
                }
            }
            return tokenList;
        }
    }
}