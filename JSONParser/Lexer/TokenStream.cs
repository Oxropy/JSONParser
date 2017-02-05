using System.Collections.Generic;
using JSONParser.Util;

namespace JSONParser.Parser
{
    public class TokenStream : IndexedStream<Token, TokenType>
    {
        public TokenStream(IEnumerable<Token> input) : base(input, (token, type) => token.Type == type)
        {
        }
    }
}