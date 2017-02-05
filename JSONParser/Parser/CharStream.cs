using System.Collections.Generic;
using JSONParser.Util;

namespace JSONParser.Parser
{
    public class CharStream : IndexedStream<char, char>
    {
        public CharStream(IEnumerable<char> input) : base(input, (c, expected) => (int) c == (int) expected)
        {
        }
    }
}